# .NET Custom Logger

A simple, versatile logger for .NET applications, compatible with both legacy .NET Framework and modern .NET versions. This logger supports console and file outputs with various customization options, making it a flexible choice for different logging needs.

## Features

- **Broad Compatibility**: Targets .NET Standard 2.0 for compatibility with .NET Framework 4.x and .NET 5, 6, 7, and 8.
- **Multiple Log Levels**: Includes `DEBUG`, `INFO`, `SUCCESS`, `WARNING`, `ERROR`, and `CRITICAL` levels for detailed logging.
- **Output Preferences**: Supports logging to the console, file, or both.
- **Customization**: Allows custom timestamp formats and console foreground colors.
- **Easy Configuration**: Configurable as a static class, requiring minimal setup.

## Getting Started

To use the logger, simply include it in your project and set up the configuration as needed.

### Prerequisites

- .NET Standard 2.0 compatible environment.

### Installation

Include the `Logger` class in your project. No additional packages are required.

### Usage

Configure the logger based on your preferences and use the logging methods to log messages at various levels:

```csharp
using Log = Logger.Logger;

// Configure logger
Log.LogLevel = LogLevel.INFO;
Log.ConsoleOutputEnabled = true;
Log.FileOutputEnabled = true;
Log.FilePath = "path/to/your/logfile.txt";
Log.TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff";

// Log some stuff
Log.Info("This is an informational message.");
Log.Error("This is an error message.");

// Log some stuff to only one destination
Log.Info("This is information that only outputs to the console, even if FileOutputEnabled = true", OutputPreference.ConsoleOnly);
```

## Configuration Options

- `LogLevel`: Sets the minimum level of messages to be logged.
- `ConsoleOutputEnabled`: Enables or disables logging to the console.
- `FileOutputEnabled`: Enables or disables logging to a file.
- `FilePath`: Sets the path for the log file.
- `TimestampFormat`: Sets the format for timestamps in log messages.

## Contributing

Contributions to the logger are welcome. Please feel free to fork the repository, make changes, and submit pull requests.

## License

This logger is open-sourced software licensed under the [MIT license](https://opensource.org/licenses/MIT).
