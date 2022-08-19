pipeline {
  agent any
  stages {
    stage('Build') {
      agent any
      steps {
        sh 'dotnet build eShopOnWeb.sln'
      }
    }

    stage('Tests') {
      parallel {
        stage('Tests') {
          agent any
          steps {
            sh 'dotnet test tests/UnitTests'
          }
        }

        stage('Integration') {
          agent any
          steps {
            sh 'dotnet test tests/IntegrationTests'
          }
        }

        stage('Functional') {
          agent any
          steps {
            sh 'dotnet test tests/FunctionalTests'
          }
        }

      }
    }

    stage('Deployment') {
      agent any
      steps {
        sh 'dotnet publish eShopOnWeb.sln -o C:/Users/medin/Desktop/Dev'
      }
    }

  }
}