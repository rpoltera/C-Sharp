#!/bin/bash

# First, download the original working Plex script
wget -qO plex_original.sh https://github.com/community-scripts/ProxmoxVE/raw/main/ct/plex.sh

# Make the necessary modifications to enable GPU passthrough
sed -i 's/var_unprivileged="${var_unprivileged:-1}"/var_unprivileged="${var_unprivileged:-0}"/' plex_original.sh
sed -i 's/APP="Plex"/APP="Plex with NVIDIA GPU"/' plex_original.sh

# Create a file with the NVIDIA GPU setup commands
cat > nvidia_setup.sh << 'EOF'
#!/bin/bash

# To be run inside the container after Plex installation

# Install NVIDIA dependencies
apt-get update
apt-get -y install linux-headers-$(uname -r) build-essential gcc make xorg

# Add NVIDIA repository and install drivers
apt-get -y install software-properties-common
add-apt-repository -y ppa:graphics-drivers/ppa
apt-get update
apt-get -y install nvidia-driver-535 nvidia-utils-535

# Install NVIDIA CUDA toolkit
apt-get -y install nvidia-cuda-toolkit

# Configure Plex to use NVIDIA GPU for hardware acceleration
usermod -a -G video plex

# Create a script to add necessary device permissions at startup
cat > /etc/systemd/system/plex-gpu-access.service << 'EOFS'
[Unit]
Description=Plex GPU Access
Before=plexmediaserver.service

[Service]
Type=oneshot
ExecStart=/bin/bash -c 'chmod -R 755 /dev/dri /dev/nvidia*'
RemainAfterExit=yes

[Install]
WantedBy=multi-user.target
EOFS

systemctl enable plex-gpu-access.service

# Create a Plex preferences.xml template with hardware acceleration enabled
mkdir -p /var/lib/plexmediaserver/Library/Application\ Support/Plex\ Media\ Server/

cat > /var/lib/plexmediaserver/Library/Application\ Support/Plex\ Media\ Server/Preferences.xml << 'EOFS'
<?xml version="1.0" encoding="utf-8"?>
<Preferences HardwareAcceleratedCodecs="1" HardwareAcceleratedEncoders="nvenc" HardwareAcceleratedDecoders="nvidia" HardwareDevicePath="/dev/dri:/dev/nvidia0" />
EOFS

# Set proper ownership
chown -R plex:plex /var/lib/plexmediaserver/

# Restart Plex Media Server
systemctl restart plexmediaserver

echo "NVIDIA GPU setup complete!"
echo "Add the following to your LXC config in Proxmox:"
echo "lxc.cgroup2.devices.allow: c 226:* rwm"
echo "lxc.mount.entry: /dev/nvidia0 dev/nvidia0 none bind,optional,create=file"
echo "lxc.mount.entry: /dev/nvidiactl dev/nvidiactl none bind,optional,create=file"
echo "lxc.mount.entry: /dev/nvidia-uvm dev/nvidia-uvm none bind,optional,create=file"
echo "lxc.mount.entry: /dev/nvidia-modeset dev/nvidia-modeset none bind,optional,create=file"
echo "lxc.mount.entry: /dev/dri dev/dri none bind,optional,create=dir"
EOF

chmod +x nvidia_setup.sh

# Add the NVIDIA script to the end of customize_container function
awk '/function customize_container/{a=1}a&&/^}/{print "  # Setup NVIDIA GPU passthrough\n  mv /root/nvidia_setup.sh /mnt/pve/rootfs/\n  msg_info \"Use the nvidia_setup.sh script inside the container to complete GPU setup\"\n"; a=0} {print}' plex_original.sh > plex_nvidia.sh

# Make the new script executable
chmod +x plex_nvidia.sh

# Execute the modified script
bash plex_nvidia.sh

echo ""
echo "---------------------------------------"
echo "IMPORTANT: After container creation, login to the container and run:"
echo "bash /nvidia_setup.sh"
echo "---------------------------------------"
