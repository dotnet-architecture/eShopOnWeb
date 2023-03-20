#!/bin/bash

# Terraform Init
terraform init

# Terraform validate
terraform validate -compact-warnings

# Terraform plan
terraform plan -compact-warnings -out main.tfplan

# Terraform apply
terraform apply -compact-warnings -auto-approve main.tfplan