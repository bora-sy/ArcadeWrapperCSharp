# Hack Club Arcade API Wrapper

[![NuGet](https://img.shields.io/nuget/v/HackClub.Arcade.svg?label=NuGet)](https://www.nuget.org/packages/HackClub.Arcade/1.0.0)

This is a .NET library that provides a wrapper around the Hack Club Arcade API, allowing developers to easily interact with endpoints related to user sessions, statistics, goals, and history. Please note that this library is not officially maintained by Hack Club.

## Installation

You can install the package via NuGet:

```bash
dotnet add package HackClub.Arcade
```

## Usage

To get started, initialize an instance of `ArcadeWrapper` with your Hackclub Arcade API key:

```csharp
using HackClub.Arcade;

// Initialize the wrapper with your API key
var arcadeWrapper = new ArcadeWrapper("your-api-key");

// Example usage: Retrieve user statistics
var userStats = arcadeWrapper.GetUserStats();
Console.WriteLine($"Total sessions: {userStats.TotalSessions}");

// Example usage: Start a new session
var startResult = arcadeWrapper.StartSession("Working on a project");
Console.WriteLine($"Session started at: {startResult.StartTime}");

// Example usage: Pause the current session
var pauseResult = arcadeWrapper.PauseSession();
Console.WriteLine($"Session paused: {pauseResult.Paused}");

// Example usage: Retrieve session history
var sessionHistory = arcadeWrapper.GetSessionHistory();
foreach (var session in sessionHistory)
{
    Console.WriteLine($"Session ID: {session.SessionId}, Duration: {session.Duration}");
}
```
For further guidance and examples, refer to the [Example Project](https://github.com/bora-sy/ArcadeWrapperCSharp/tree/main/Example).

## Methods

### `Ping()` / `PingAsync()`

Checks if the Arcade API server is reachable.

### `GetUserStats()` / `GetUserStatsAsync()`

Retrieves statistics for the current user.

### `GetLatestSession()` / `GetLatestSessionAsync()`

Retrieves information about the latest session for the current user.

### `GetGoals()` / `GetGoalsAsync()`

Retrieves goals associated with the current user.

### `GetSessionHistory()` / `GetSessionHistoryAsync()`

Retrieves session history entries for the current user.

### `StartSession(string work)` / `StartSessionAsync(string work)`

Starts a new session for the user with the specified work description.

### `PauseSession()` / `PauseSessionAsync()`

Pauses or resumes the current session for the user.

### `CancelSession()` / `CancelSessionAsync()`

Cancels the current session for the user.
