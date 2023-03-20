output "name" {
  value       = azurerm_application_insights.resource.name
  description = "Specifies the name of the resource."
}

output "id" {
  value       = azurerm_application_insights.resource.id
  description = "Specifies the resource id of the resource."
}

output "instrumentation_key" {
  value       = azurerm_application_insights.resource.instrumentation_key
  description = "Specifies the instrumentation key of the Application Insights."
}

output "app_id" {
  value       = azurerm_application_insights.resource.app_id
  description = "Specifies the resource id of the resource."
}