provider "google" {
  project = "sogilis-hectorj-eshop-on-web"
}

provider "docker" {
  registry_auth {
    address             = "eu.gcr.io"
    config_file_content = <<EOT
    { "credHelpers" : { "eu.gcr.io" : "gcloud" } }
    EOT
  }
}

provider "random" {}

terraform {
  required_providers {
    random = {
      source = "hashicorp/random"
    }
    google = {
      source = "hashicorp/google"
    }
    docker = {
      source = "kreuzwerker/docker"
    }
  }
}

data "google_project" "main" {}

data "google_container_registry_repository" "main" {
  region = "eu"
}

locals {
  eshopwebmvc_docker_image = "${data.google_container_registry_repository.main.repository_url}/eshopwebmvc"
  project_root_path        = abspath("${path.module}/..")
}

resource "docker_registry_image" "eshopwebmvc" {
  name = "${local.eshopwebmvc_docker_image}:latest"

  build {
    context    = local.project_root_path
    dockerfile = "src/Web/Dockerfile"
  }
}

resource "random_password" "sql_password" {
  length = 16
}

resource "google_sql_database_instance" "sql_server" {
  name             = "eshop-sqlserver"
  region           = "europe-west1"
  database_version = "SQLSERVER_2017_STANDARD"
  root_password    = random_password.sql_password.result
  settings {
    tier = "db-custom-1-3840"
    ip_configuration {
      ipv4_enabled = true
      authorized_networks {
        name  = "all"
        value = "0.0.0.0/0" # FIXME: this authorizes all IPs to try to connect (with credentials). Restrict to private network for security!!!!!
      }
    }
  }

  deletion_protection = "true"
}

resource "google_cloud_run_service" "eshopwebmvc" {
  autogenerate_revision_name = true
  name                       = "eshopwebmvc"
  location                   = "europe-west1"

  template {
    spec {
      containers {
        image = "${local.eshopwebmvc_docker_image}@${docker_registry_image.eshopwebmvc.sha256_digest}"
        env {
          name  = "ASPNETCORE_ENVIRONMENT"
          value = "Development"
        }
        env {
          name  = "ASPNETCORE_URLS"
          value = "http://+:8080"
        }
        env {
          name  = "ESHOP_ConnectionStrings__CatalogConnection"
          value = "Server=${google_sql_database_instance.sql_server.public_ip_address};Uid=sqlserver;Pwd=${random_password.sql_password.result};Database=Microsoft.eShopOnWeb.CatalogDb"
        }
        env {
          name  = "ESHOP_ConnectionStrings__IdentityConnection"
          value = "Server=${google_sql_database_instance.sql_server.public_ip_address};Uid=sqlserver;Pwd=${random_password.sql_password.result};Database=Microsoft.eShopOnWeb.Identity"
        }
        ports {
          container_port = 8080
          name           = "http1"
        }
      }
    }

    metadata {
      annotations = {
        "autoscaling.knative.dev/maxScale" = "2"
        # "run.googleapis.com/cloudsql-instances" = google_sql_database_instance.sql_server.connection_name
      }
    }
  }

  traffic {
    percent         = 100
    latest_revision = true
  }
}

data "google_iam_policy" "noauth" {
  binding {
    role = "roles/run.invoker"
    members = [
      "allUsers",
    ]
  }
}

resource "google_cloud_run_service_iam_policy" "noauth" {
  location = google_cloud_run_service.eshopwebmvc.location
  project  = google_cloud_run_service.eshopwebmvc.project
  service  = google_cloud_run_service.eshopwebmvc.name

  policy_data = data.google_iam_policy.noauth.policy_data
}
