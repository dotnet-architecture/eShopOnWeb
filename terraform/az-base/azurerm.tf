#--------------------------------------------------------------
#   Provider to Test Subscription
#--------------------------------------------------------------
provider "azurerm" {
  # Reference: https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs#argument-reference
  # alias       = "testSubscription" # Comment/Remove this alias as it may not be needed
  environment = "public"
  # partner_id  = "adb8eac6-989a-5354-8580-19055546ec74"

  tenant_id       = "72f988bf-86f1-41af-91ab-2d7cd011db47"
  subscription_id = "181b4f09-67a4-4baa-b5d5-f10e01dd5d3b"
  client_id       = "cc9380e8-8ae1-410d-bc41-dde0a8f58599"
  client_secret   = var.spn_secret

  features {}
}
