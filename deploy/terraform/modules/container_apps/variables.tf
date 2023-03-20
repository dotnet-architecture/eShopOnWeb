
variable "managed_environment_name" {
  description = "(Required) Specifies the name of the managed environment."
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

variable "infrastructure_subnet_id" {
  description = "(Optional) Specifies resource id of the subnet hosting the Azure Container Apps environment."
  type        = string
}

variable "internal_load_balancer_enabled" {
  description = "(Optional) Should the Container Environment operate in Internal Load Balancing Mode? Defaults to false. Changing this forces a new resource to be created."
  type        = bool
  default     = false
}

variable "instrumentation_key" {
  description = "(Optional) Specifies the instrumentation key of the application insights resource."
  type        = string
}

variable "workspace_id" {
  description = "(Optional) Specifies resource id of the log analytics workspace."
  type        = string
}

variable "dapr_components" {
  description = "(Optional) Specifies the dapr components."
  type = list(object({
    name                           = string
    component_type                 = string
    ignore_errors                  = optional(bool)
    version                        = optional(string)
    init_timeout                   = optional(string)
    scopes                         = optional(list(string))
    metadata                       = optional(list(object({
      name                         = string
      secret_name                  = optional(string)
      value                        = optional(string)
    })))
    secret                         = optional(list(object({
      name                         = string
      value                        = string
    })))
  }))
  default = []
}

variable "container_apps" {
  description = "Specifies the container apps in the managed environment."
  type = list(object({
    name                           = string
    revision_mode                  = optional(string)
    ingress                        = optional(object({
      allow_insecure_connections   = optional(bool)
      external_enabled             = optional(bool)
      target_port                  = optional(number)
      transport                    = optional(string)
      traffic_weight               = optional(list(object({
        label                      = optional(string)
        latest_revision            = optional(bool)
        revision_suffix            = optional(string)
        percentage                 = optional(number)
      })))
    }))
    dapr                           = optional(object({
      app_id                       = optional(string)
      app_port                     = optional(number)
      app_protocol                 = optional(string)
    }))
    secrets                        = optional(list(object({
      name                         = string
      value                        = string
    })))
    template                       = object({
      containers                   = list(object({
        name                       = string
        image                      = string
        args                       = optional(list(string))
        command                    = optional(list(string))
        cpu                        = optional(number)
        memory                     = optional(string)
        env                        = optional(list(object({
          name                     = string
          secret_name              = optional(string)
          value                    = optional(string)
        })))
      }))
      min_replicas                 = optional(number)
      max_replicas                 = optional(number)
      revision_suffix              = optional(string)
      volume                       = optional(list(object({
        name                       = string
        storage_name               = optional(string)
        storage_type               = optional(string)
      })))
    })
  }))
}