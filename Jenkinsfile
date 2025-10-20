pipeline {
    agent any

    environment {
        FTP_USER = 'nghiapt'
        FTP_PASS = 'Nghia2003.'
        FTP_HOST = 'ftp://155.254.244.39/www.nghiahocbai.somee.com'
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'master', url: 'https://github.com/NghiaNDabc/NhaSachDaiThang_BE_API.git'
            }
        }

        stage('Restore') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build --configuration Release'
            }
        }

        stage('Test') {
            steps {
                bat 'dotnet test --no-build --configuration Release'
            }
        }

        stage('Publish') {
            steps {
                bat 'dotnet publish -c Release -o publish'
            }
        }

        stage('Deploy to Somee') {
            steps {
                bat '''
                cd publish
                curl -T "*" ftp://%FTP_HOST%/wwwroot/ --user %FTP_USER%:%FTP_PASS%
                '''
            }
        }
    }
}
