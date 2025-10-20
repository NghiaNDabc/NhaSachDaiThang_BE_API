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
                powershell '''
                $ftpHost = $env:FTP_HOST
                $ftpUser = $env:FTP_USER
                $ftpPass = $env:FTP_PASS

                $publishPath = Get-Item publish
                $files = Get-ChildItem -Path $publishPath.FullName -Recurse -File

                foreach ($f in $files) {
                    $relativePath = $f.FullName.Substring($publishPath.FullName.Length + 1).Replace("\\","/")
                  
                    $ftpUrl = '$ftpHost/' + $relativePath 

                    Write-Host "Uploading $($f.FullName) to $ftpUrl"

                    curl.exe -T "$($f.FullName)" "$ftpUrl" --user "$ftpUser:$ftpPass"
                }
                '''
            }
        }
    }
}
