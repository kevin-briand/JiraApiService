# Jira API Service

This project provides a service for interacting with the Jira API, offering methods to retrieve and manipulate data
related to issues, projects, users, and changelogs. The service supports paginated data and asynchronous operations.

## Installation

To use this service in your project, clone the repository.

### Clone Repository

```bash
git clone https://github.com/kevin-briand/JiraApiService.git
cd JiraApiService
```

## Usage

### Create an Instance of JiraApiService

To use the JiraApiService, you will need to create an instance and provide your Jira instance URL, username, and
password/token.

```csharp
// Initialize the JiraApiService
var jiraApiService = new JiraApiService("https://your-jira-instance.atlassian.net", "your-username", "your-token");
```

Alternatively, you can register it as a service for dependency injection:

```csharp
// Initialize the JiraApiService
builder.Services.AddScoped<IJiraApiService, JiraApiService>();
```

and add the following settings un appsetting.json

```json
{
  "JiraApi": {
    "BaseUrl": "https://yourInstance.atlassian.net",
    "Username": "yourEmail",
    "Password": "password/token"
  }
}
```

After that, you can use the methods provided.

```csharp
// Example: Fetch updated issues
var current = 0;
await foreach (var updatedIssue in jiraApiService.GetUpdatedIssues(DateTime.Now.AddDays(-7)))
{
    // Insert missing changelogs/worklogs if needed
    updatedIssue.Values = await jiraApi.InsertMissingChangelogsAndWorklogs(updatedIssue.Values);
    foreach (var issue in updatedIssue.Values)
    {
        current++;
        Console.WriteLine($"{current}/{updatedIssue.Total} {issue.Key}");
    }
}
```

### Serilog Logging Configuration

For logging, this project uses Serilog to track API requests, responses, and other important information.
Setting up Serilog

Here an example of configuration of logs :

```csharp
// Set up Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Sets the minimum log level to capture
    .Enrich.WithProperty("Project", Assembly.GetEntryAssembly()?.GetName().Name ?? "Unknown") // Add project name to logs
    .WriteTo.Console(LogEventLevel.Debug, "[{Timestamp:HH:mm:ss} {Level:u3} {Project}] {Message:lj}{NewLine}{Exception}") // Define the output format
    .CreateLogger(); // Create and initialize the logger

Log.Information("Starting Jira API");
```
