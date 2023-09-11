## Getting Started
Application is structured as below
1. Process.UserData.FunctionApp  - Main function App for Timer Trigger
2. Process.UserData.FunctionApp.Domain - Implements the business logic 
3. Process.UserData.FunctionApp.Infrastructure -  Implements the repositories for external servies

4. UserDb - Project was added to create database structure 
   To Create daytabase structure and stored procedures, in package manager console, set UserDb as startup project, and run below command, Make sure `DatabaseConnectionString` is configured in local.settings.json file
   update-database
   

## Configuration

Process.UserData.FunctionApp - Configuation setting are configured in to local.settings.json file or in Environment variables.

{
    "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "APPINSIGHTS_INSTRUMENTATIONKEY": "5ab0eb2b-7007-40fe-834f-4481ae8d0b3b",
    "ServiceBusConnectionString": "Endpoint=sb://sb-test-rab.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Z5NyFG+xnSt71KuZBhdHDc58ZS7pZWN4H+ASbMAZhEU=",
    "DatabaseConnectionString": "Server=tcp:functionsdbserver.database.windows.net,1433;Initial Catalog=FunctionsDb;Persist Security Info=False;User ID=azuser;Password=user@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
    "AppInsightsDefaultLogLevel": "Debug"
  }
}
