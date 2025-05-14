#!/usr/bin/env bash
source <(curl -fsSL https://raw.githubusercontent.com/community-scripts/ProxmoxVE/main/misc/build.func)

# Copyright (c) 2021-2025 tteck
# Author: tteck (tteckster)
# License: MIT | https://github.com/community-scripts/ProxmoxVE/raw/main/LICENSE
# Source: https://www.plex.tv/

APP="Plex with NVIDIA GPU Passthrough"
var_tags="${var_tags:-media}"
var_cpu="${var_cpu:-2}"
var_ram="${var_ram:-2048}"
var_disk="${var_disk:-8}"
var_os="${var_os:-ubuntu}"
var_version="${var_version:-22.04}"
var_unprivileged="${var_unprivileged:-0}" # Set to 0 for privileged container needed for GPU passthrough

header_info "$APP"
variables
color
catch_errors

function update_script() {
  header_info
  check_container_storage
  check_container_resources
  if [[ ! -f /etc/apt/sources.list.d/plexmediaserver.list ]]; then
    msg_error "No ${APP} Installation Found!"
    exit
  fi
  UPD=$(whiptail --backtitle "Proxmox VE Helper Scripts" --title "SUPPORT" --radiolist --cancel-button Exit-Script "Spacebar = Select \nplexupdate info >> https://github.com/mrworf/plexupdate" 10 59 2 \
    "1" "Update LXC" ON \
    "2" "Install plexupdate" OFF \
    3>&1 1>&2 2>&3)
  if [ "$UPD" = "1" ]; then
    msg_info "Updating ${APP} LXC"
    $STD apt-get update
    $STD apt-get -y upgrade
    msg_ok "Updated ${APP} LXC"
    exit
  fi
  if [ "$UPD" = "2" ]; then
    set +e
    bash -c "$(curl -fsSL https://raw.githubusercontent.com/mrworf/plexupdate/master/extras/installer.sh)"
    exit
  fi
}

function setup_container() {
  # Install Plex Media Server
  msg_info "Adding Plex Repository Key"
  curl -fsSL https://downloads.plex.tv/plex-keys/PlexSign.key | gpg --dearmor | $STD tee /usr/share/keyrings/plex.gpg > /dev/null
  
  msg_info "Adding Plex Repository"
  echo "deb [signed-by=/usr/share/keyrings/plex.gpg] https://downloads.plex.tv/repo/deb public main" | $STD tee /etc/apt/sources.list.d/plexmediaserver.list > /dev/null
  
  msg_info "Installing Plex Media Server"
  $STD apt-get update
  $STD apt-get -y install plexmediaserver
  
  # Install dependencies for NVIDIA drivers
  msg_info "Installing NVIDIA dependencies"
  $STD apt-get -y install linux-headers-$(uname -r) build-essential gcc make xorg
  
  # Add NVIDIA repository and install drivers
  msg_info "Adding NVIDIA repository"
  $STD apt-get -y install software-properties-common
  $STD add-apt-repository -y ppa:graphics-drivers/ppa
  $STD apt-get update
  
  msg_info "Installing NVIDIA drivers"
  $STD apt-get -y install nvidia-driver-535 nvidia-utils-535
  
  # Install NVIDIA CUDA toolkit
  msg_info "Installing NVIDIA CUDA toolkit"
  $STD apt-get -y install nvidia-cuda-toolkit
  
  # Configure Plex to use NVIDIA GPU for hardware acceleration
  msg_info "Configuring Plex for GPU acceleration"
  # Ensure Plex user has access to the GPU
  $STD usermod -a -G video plex
  
  # Create a script to add necessary device permissions at startup
  cat > /etc/systemd/system/plex-gpu-access.service << 'EOF'
[Unit]
Description=Plex GPU Access
Before=plexmediaserver.service

[Service]
Type=oneshot
ExecStart=/bin/bash -c 'chmod -R 755 /dev/dri /dev/nvidia*'
RemainAfterExit=yes

[Install]
WantedBy=multi-user.target
EOF

  $STD systemctl enable plex-gpu-access.service
  
  # Create a Plex preferences.xml template with hardware acceleration enabled
  mkdir -p /var/lib/plexmediaserver/Library/Application\ Support/Plex\ Media\ Server/
  
  cat > /var/lib/plexmediaserver/Library/Application\ Support/Plex\ Media\ Server/Preferences.xml << 'EOF'
<?xml version="1.0" encoding="utf-8"?>
<Preferences HardwareAcceleratedCodecs="1" HardwareAcceleratedEncoders="nvenc" HardwareAcceleratedDecoders="nvidia" HardwareDevicePath="/dev/dri:/dev/nvidia0" />
EOF
  
  # Set proper ownership
  $STD chown -R plex:plex /var/lib/plexmediaserver/
  
  # Restart Plex Media Server
  msg_info "Restarting Plex Media Server"
  $STD systemctl restart plexmediaserver
}

start build_container
setup_container
description 

msg_ok "Completed Successfully!\n"
echo -e "${CREATING}${GN}${APP} setup has been successfully initialized!${CL}"
echo -e "${INFO}${YW} Access it using the following URL:${CL}"
echo -e "${TAB}${GATEWAY}${BGN}http://${IP}:32400/web${CL}"
echo -e "${INFO}${YW} Important Notes:${CL}"
echo -e "${TAB}${BGN}1. Make sure to pass your NVIDIA GPU to the container in Proxmox${CL}"
echo -e "${TAB}${BGN}2. Add these lines to your LXC config (replace with your GPU IDs):${CL}"
echo -e "${TAB}${BGN}   lxc.cgroup2.devices.allow: c 226:* rwm${CL}"
echo -e "${TAB}${BGN}   lxc.mount.entry: /dev/nvidia0 dev/nvidia0 none bind,optional,create=file${CL}"
echo -e "${TAB}${BGN}   lxc.mount.entry: /dev/nvidiactl dev/nvidiactl none bind,optional,create=file${CL}"
echo -e "${TAB}${BGN}   lxc.mount.entry: /dev/nvidia-uvm dev/nvidia-uvm none bind,optional,create=file${CL}"
echo -e "${TAB}${BGN}   lxc.mount.entry: /dev/nvidia-modeset dev/nvidia-modeset none bind,optional,create=file${CL}"
echo -e "${TAB}${BGN}   lxc.mount.entry: /dev/dri dev/dri none bind,optional,create=dir${CL}"
echo -e "${TAB}${BGN}3. After first login, verify GPU is detected in Settings > Transcoder${CL}"
