output "id" {
  description = "Specifies the resource id of the private endpoint."
  value       = azurerm_private_endpoint.private_endpoint.id
}

output "private_dns_zone_group" {
  description = "Specifies the private dns zone group of the private endpoint."
  value = azurerm_private_endpoint.private_endpoint.private_dns_zone_group
}

output "private_dns_zone_configs" {
  description = "Specifies the private dns zone(s) configuration"
  value = azurerm_private_endpoint.private_endpoint.private_dns_zone_configs
}