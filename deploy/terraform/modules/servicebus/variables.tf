
variable "name" {
  description = "(Required) Specifies the name of the ServiceBus namespace"
  type        = string
}

variable "resource_group_name" {
  description = "(Required) Specifies the resource group name"
  type = string
}

variable "tags" {
  description = "(Optional) Specifies the tags of the log analytics workspace"
  type        = map(any)
  default     = {}
}

variable "location" {
  description = "(Required) Specifies the supported Azure location where the resource exists. Changing this forces a new resource to be created."
  type        = string
}

variable "topics" {
  type = map(object({
    subscriptions = map(object({
      max_delivery_count = number
    }))
  }))
}