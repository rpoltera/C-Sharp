#!/usr/bin/env bash
source <(curl -fsSL https://raw.githubusercontent.com/community-scripts/ProxmoxVE/main/misc/build.func)

# Copyright (c) 2021-2025 tteck
# Author: tteck (tteckster)
# License: MIT | https://github.com/community-scripts/ProxmoxVE/raw/main/LICENSE
# Source: https://www.plex.tv/

APP="Plex with NVIDIA GPU"
var_tags="${var_tags:-media}"
var_cpu="${var_cpu:-2}"
var_ram="${var_ram:-2048}"
var_disk="${var_disk:-8}"
var_os="${var_os:-ubuntu}"
var_version="${var_version:-22.04}"
var_unprivileged="${var_unprivileged:-0}" # Changed to 0 for privileged container needed for GPU

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

function customize_container() {
  msg_info "Installing Plex Media Server"
  curl -fsSL https://downloads.plex.tv/plex-keys/PlexSign.key | gpg --dearmor | $STD tee /usr/share/keyrings/plex.gpg > /dev/null
  echo "deb [signed-by=/usr/share/keyrings/plex.gpg] https://downloads.plex.tv/repo/deb public main" | $STD tee /etc/apt/sources.list.d/plexmediaserver.list > /dev/null
  $STD apt-get update
  $STD apt-get -y install plexmediaserver
  
  # NVIDIA GPU passthrough setup
  msg_info "Installing NVIDIA dependencies"
  $STD apt-get -y install linux-headers-$(uname -r) build-essential gcc make xorg
  
  msg_info "Adding NVIDIA repository"
  $STD apt-get -y install software-properties-common
  $STD add-apt-repository -y ppa:graphics-drivers/ppa
  $STD apt-get update
  
  msg_info "Installing NVIDIA drivers"
  $STD apt-get -y install nvidia-driver-535 nvidia-utils-535
  
  msg_info "Installing NVIDIA CUDA toolkit"
  $STD apt-get -y install nvidia-cuda-toolkit
  
  msg_info "Configuring Plex for GPU acceleration"
  $STD usermod -a -G video plex
  
  # Create service for GPU permissions
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
  
  #
