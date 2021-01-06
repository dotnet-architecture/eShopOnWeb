terraform {
  backend "gcs" {
    bucket = "sogilis-hectorj-eshop-on-web-terraform"
    prefix = "terraform/state"
  }
}
