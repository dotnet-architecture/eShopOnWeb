output "name" {
  value       = azurerm_container_app_environment.managed_environment.name
  description = "Specifies the name of the managed environment."
}

output "id" {
  value       = azurerm_container_app_environment.managed_environment.id
  description = "Specifies the resource id of the managed environment."
}