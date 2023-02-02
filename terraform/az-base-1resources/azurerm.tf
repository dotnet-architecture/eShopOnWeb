#--------------------------------------------------------------
#   Provider to Test Subscription
#--------------------------------------------------------------
provider "azurerm" {
  # Reference: https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs#argument-reference
  # alias       = "testSubscription" # Comment/Remove this alias as it may not be needed
  environment = "public"

  tenant_id       = var.spn_tenant_id
  subscription_id = var.spn_subscription_id
  client_id       = var.spn_client_id
  client_secret   = var.spn_secret

  features {
    resource_group {
      prevent_deletion_if_contains_resources = false
    }
  }
}
