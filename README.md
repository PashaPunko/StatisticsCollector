# StatisticsCollectorApp

## Overview
StatisticsCollectorApp is a C# application designed to collect and publish statistics from GitHub repositories. It uses Autofac for dependency injection and integrates with various services to perform its tasks.

## Features
- Collects statistics from GitHub repositories.
- Publishes collected statistics.
- Configurable through `appsettings.json` or terminal .

## Prerequisites
- .NET 6.0 or later

## Setup

1. **Clone the repository:**
    ```sh
    git clone https://github.com/PashaPunko/StatisticsCollectorApp.git
    cd StatisticsCollectorApp
    ```

2. **Restore dependencies:**
    ```sh
    dotnet restore
    ```

3. **Build the project:**
    ```sh
    dotnet build
    ```

4. **Run the application:**
    ```sh
    dotnet run --project StatisticsCollectorApp
    ```

## Configuration
1. You may configure application using `appsettings.json` for configuration:

```json
{
  "RepositoryParameters": {
    "Owner": "github-username",
    "Name": "repository-name",
    "Reference": "reference-branch-or-commit"
  }
},
"GitHubOptions": {
    "Token": "your-github-token"
}
```
2. Or using console arguments:
   ```sh
    dotnet run --project StatisticsCollectorApp --RepositoryParameters:Owner=github-username --RepositoryParameters:Name=repository-name --RepositoryParameters:Reference=reference-branch-or-commit --GitHubOptions:Token=your-github-token
    ```
