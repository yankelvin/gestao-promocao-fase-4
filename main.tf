provider "aws" {
  region = "us-east-2"
}

provider "kubernetes" {
  host  = "https://1FC52356DA6E21E0390E97776718260C.gr7.us-east-2.eks.amazonaws.com"
  cluster_ca_certificate = filebase64("ca_certificate.pem")
}

data "kubernetes_secret" "cluster_ca" {
  metadata {
    name      = "default-token"
    namespace = "kube-system"
  }
}

resource "kubernetes_deployment" "my_api_deployment" {
  metadata {
    name = "minha-api"
  }

  spec {
    replicas = 3

    selector {
      match_labels = {
        app = "minha-api"
      }
    }

    template {
      metadata {
        labels = {
          app = "minha-api"
        }
      }

      spec {
        container {
          image = "547185396737.dkr.ecr.us-east-2.amazonaws.com/minha-api:latest"
          name  = "minha-api"
          port {
            container_port = 80
          }
        }
      }
    }
  }
}
