variable "name" {
  description = "(Required) Specifies the name of the private endpoint. Changing this forces a new resource to be created."
  type        = string
}

variable "resource_group_name" {
  description = "(Required) The name of the resource group. Changing this forces a new resource to be created."
  type        = string
}

variable "private_connection_resource_id" {
  description = "(Required) Specifies the resource id of the private link service"
  type        = string 
}

variable "location" {
  description = "(Required) Specifies the supported Azure location where the resource exists. Changing this forces a new resource to be created."
  type        = string
}

variable "subnet_id" {
  description = "(Required) Specifies the resource id of the subnet"
  type        = string
}

variable "is_manual_connection" {
  description = "(Optional) Specifies whether the private endpoint connection requires manual approval from the remote resource owner."
  type        = string
  default     = false  
}

variable "subresource_name" {
  description = "(Optional) Specifies a subresource name which the Private Endpoint is able to connect to."
  type        = string
  default     = null
}

variable "request_message" {
  description = "(Optional) Specifies a message passed to the owner of the remote resource when the private endpoint attempts to establish the connection to the remote resource."
  type        = string
  default     = null 
}

variable "private_dns_zone_group_name" {
  description = "(Required) Specifies the Name of the Private DNS Zone Group. Changing this forces a new private_dns_zone_group resource to be created."
  type        = string
}

variable "private_dns_zone_group_ids" {
  description = "(Required) Specifies the list of Private DNS Zones to include within the private_dns_zone_group."
  type        = list(string)
}

variable "tags" {
  description = "(Optional) Specifies the tags of the network security group"
  default     = {}
}

variable "private_dns" {
  default = {}
}