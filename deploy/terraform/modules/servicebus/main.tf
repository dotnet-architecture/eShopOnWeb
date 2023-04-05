terraform {
  required_version = ">= 1.3"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.43.0"
    }
  }
}

locals {
  subscriptions = [ for topic_key, topic in var.topics : [
      for subscription_key, subscription in topic.subscriptions : {
        topic_id = azurerm_servicebus_topic.topic[topic_key].id
        name  = subscription_key
        max_delivery_count  = subscription.max_delivery_count
      }
    ]
  ]
}

resource "azurerm_servicebus_namespace" "namespace" {
  name                         = var.name
  resource_group_name          = var.resource_group_name
  location                     = var.location
  sku                 = "Standard"
  tags = var.tags
}


resource "azurerm_servicebus_topic" "topic" {
  for_each       = var.topics
  name           = each.key
  namespace_id = azurerm_servicebus_namespace.namespace.id
}

resource "azurerm_servicebus_subscription" "subscription" {
  for_each           = { for k, v in flatten(local.subscriptions) : v.name => v }
  name               = each.key
  topic_id           = each.value.topic_id
  max_delivery_count = each.value.max_delivery_count
}

