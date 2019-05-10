pipeline {
    agent any

    stages {
        stage('Install Angular') {
            when {
                anyOf{
                    branch 'develop';
                    branch 'preprod';
                    branch 'master';
                    branch 'sonar';
                    tag "Version_1*"
                }
            }
            steps {
                echo 'Building..'
            }
        }
        stage('Build Angular') {
             when {
                anyOf{
                    branch 'develop';
                    branch 'preprod';
                    branch 'master';
                    branch 'sonar';
                    tag "Version_1*"
                }
            }
            steps {
                echo 'Building..'
            }
        }
        
        stage('Build C#') {
             when {
                anyOf{
                    branch 'develop';
                    branch 'preprod';
                    branch 'master';
                    tag "Version_1*"
                }
            }
            steps {
                echo 'Building..'
            }
        }
        
        stage('Code Analysis') {
             when { branch 'sonar' }
            steps {
                def msg = powershell(returnStdout: true, script: 'Write-Output "PowerShell is mighty!"')
                println msg
            }
        }
        
        stage('Angular UnitTest') {
             when {
                anyOf{
                    branch 'develop';
                    branch 'preprod';
                    branch 'master';
                    tag "Version_1*"
                }
            }
            steps {
                echo 'Building..'
            }
        }
        stage('C# UnitTest') {
             when {
                anyOf{
                    branch 'develop';
                    branch 'preprod';
                    branch 'master';
                    tag "Version_1*"
                }
            }
            steps {
                echo 'Testing..'
            }
        }
        stage('ShipToOctopus') {
            when {
                anyOf{
                    branch 'develop';
                    branch 'preprod';
                    branch 'master';
                    tag "Version_1*"
                }
            }
            steps {
                echo 'Deploying....'
            }
        }
        
        stage('HappyDance') {
            steps {
                echo 'Building..'
            }
        }
    }
}
