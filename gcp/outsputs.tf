output "eshopwebmvc_docker_image" {
  value = local.eshopwebmvc_docker_image
}
output "eshopwebmvc_url" {
  value = google_cloud_run_service.eshopwebmvc.status[0].url
}
