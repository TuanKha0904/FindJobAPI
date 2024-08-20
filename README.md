# FindJobAPI

FindJobAPI is a Web API application built using .NET 7. This project provides services related to job searching.

## System Requirements

- .NET SDK 7.0: [Download and install .NET SDK 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)
- Docker: [Download and install Docker](https://www.docker.com/products/docker-desktop)
- Database: SQL Server

## Installation and Configuration

### 1. Clone the Repository

Clone the project from the repository to your local machine:
```bash
git clone https://github.com/username/FindJobAPI.git
```

### 2. Configuration
Create an appsettings.json file from the appsettings.Development.json file.
Configure the database connection and other settings in the appsettings.json file.

### 3. Run the Application Locally
- Restore NuGet packages:
```
dotnet restore
```
- Build the application:
```
dotnet build
```
- Run the application:
```
dotnet run
```
The application will start running on http://localhost:5000 or https://localhost:5001.

### 4. Run the Application with Docker
- Build the image:
```
docker build -t findjobapi .
```
- Run the container:
```
docker run -d -p 8080:80 findjobapi
```
The application will be running on http://localhost:8080.

### 5. View Swagger
Access: your-url/swagger/index.html
