using System;
using System.Collections.Generic;
using System.IO;

/* 
 * This is a simple logger that targets .NET Standard 2.0, so it can be used with legacy .NET Framework 4.x 
 * as well as modern .NET 5, 6, 7, and 8. The user can enable/disable console/file logging, and specify timestamp format,
 * console foreground color, and output file path. The logger is implemented as a static class, so the user will generally
 * only need to configure the logger once per project. 
 */

namespace Logger {

    /// <summary>
    /// Defines the severity levels of log messages.
    /// </summary>
    public enum LogLevel {
        /// <summary>
        /// Debug level for detailed and diagnostic information.
        /// Used primarily for development and troubleshooting.
        /// </summary>
        DEBUG = 0,

        /// <summary>
        /// Information level for general operational entries that highlight the progress of the application.
        /// </summary>
        INFO = 1,

        /// <summary>
        /// Success level indicating that a particular operation or process has completed successfully.
        /// </summary>
        SUCCESS = 2,

        /// <summary>
        /// Warning level for situations that are not errors but might require special handling.
        /// </summary>
        WARNING = 3,

        /// <summary>
        /// Error level indicating a problem that has occurred during application execution.
        /// It doesn't halt the application but signifies issues that need attention.
        /// </summary>
        ERROR = 4,

        /// <summary>
        /// Critical level representing serious errors that might require immediate attention.
        /// Often indicates a failure that could cause the application to stop running.
        /// </summary>
        CRITICAL = 5
    }

    /// <summary>
    /// Represents logging destination preferences.
    /// </summary>
    public enum OutputPreference {
        /// <summary>
        /// Log messages will be written to the console only.
        /// </summary>
        ConsoleOnly = 0,

        /// <summary>
        /// Log messages will be written to a file only.
        /// </summary>
        FileOnly = 1,

        /// <summary>
        /// Log messages will be written to both the console and a file.
        /// </summary>
        AllEnabled = 2
    }

    internal class LogMessage {
        // Properties
        internal LogLevel LogLevel { get; set; }
        internal DateTime Timestamp { get; set; }
        internal string Message { get; set; }

        // Constructor
        internal LogMessage(LogLevel level, string message) {
            LogLevel = level;
            Timestamp = DateTime.Now;
            Message = message;
        }
    }

    /// <summary>
    /// Static entry point for typical logging methods. Users can configure this class at project startup and use it throughout the project, and all log messages will go to the same place.
    /// </summary>
    public static class StaticLogger {

        // Fields
        private static readonly object _lockObj = new object();
        private static readonly Dictionary<LogLevel, ConsoleColor> _logLevelColors = new Dictionary<LogLevel, ConsoleColor> {
            { LogLevel.DEBUG, ConsoleColor.Cyan },
            { LogLevel.INFO, DefaultConsoleForegroundColor },
            { LogLevel.SUCCESS, ConsoleColor.Green },
            { LogLevel.WARNING, ConsoleColor.Yellow },
            { LogLevel.ERROR, ConsoleColor.Red },
            { LogLevel.CRITICAL, ConsoleColor.DarkRed }
        };

        // Properties
        public static string TimestampFormat { get; set; }
        public static bool ConsoleOutputEnabled {  get; set; }
        public static ConsoleColor DefaultConsoleForegroundColor { get; set; }
        public static bool FileOutputEnabled { get; set; }
        public static string FilePath { get; set; }
        public static LogLevel LogLevel { get; set; }
        

        // Static constructor
        static StaticLogger() {
            TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff";
            ConsoleOutputEnabled = false;
            DefaultConsoleForegroundColor = ConsoleColor.White;
            FileOutputEnabled = true;
            FilePath = ".\\Log.txt";
            LogLevel = LogLevel.INFO;
        }

        // Methods
        private static void Log(LogMessage logMessage, OutputPreference outputPreference = OutputPreference.AllEnabled) {
            lock (_lockObj) {
                if ( logMessage.LogLevel < LogLevel ) { return; }
                string timestampString = logMessage.Timestamp.ToString(TimestampFormat);
                string logOutput = $"[{timestampString}] {logMessage.LogLevel}: {logMessage.Message}";
                if ( ConsoleOutputEnabled && outputPreference != OutputPreference.FileOnly ) {
                    Console.ForegroundColor = _logLevelColors[logMessage.LogLevel];
                    Console.WriteLine(logOutput);
                    Console.ForegroundColor = DefaultConsoleForegroundColor;
                }
                if ( FileOutputEnabled && outputPreference != OutputPreference.ConsoleOnly ) {
                    File.AppendAllText(FilePath, logOutput + "\r\n");
                }
            }
        }
        public static void Debug(string message, OutputPreference outputPreference = OutputPreference.AllEnabled) {
            Log(new LogMessage(LogLevel.DEBUG, message), outputPreference);
        }
        public static void Info(string message, OutputPreference outputPreference = OutputPreference.AllEnabled) {
            Log(new LogMessage(LogLevel.INFO, message), outputPreference);
        }
        public static void Success(string message, OutputPreference outputPreference = OutputPreference.AllEnabled) {
            Log(new LogMessage(LogLevel.SUCCESS, message), outputPreference);
        }
        public static void Warning(string message, OutputPreference outputPreference = OutputPreference.AllEnabled) {
            Log(new LogMessage(LogLevel.WARNING, message), outputPreference);
        }
        public static void Error(string message, OutputPreference outputPreference = OutputPreference.AllEnabled) {
            Log(new LogMessage(LogLevel.ERROR, message), outputPreference);
        }
        public static void Critical(string message, OutputPreference outputPreference = OutputPreference.AllEnabled) {
            Log(new LogMessage(LogLevel.CRITICAL, message), outputPreference);
        }
 
    }

    /// <summary>
    /// Instanced entry point for typical logging methods. Users can create multiple loggers if the want to create separate logs for various parts of a program.
    /// </summary>
    public class Logger {

        // Fields
        private readonly object _lockObj = new object();
        private Dictionary<LogLevel, ConsoleColor> _logLevelColors;

        // Properties
        public string SourceName { get; set; }
        public string TimestampFormat { get; set; }
        public bool ConsoleOutputEnabled { get; set; }
        public ConsoleColor DefaultConsoleForegroundColor { get; set; }
        public bool FileOutputEnabled { get; set; }
        public string FilePath { get; set; }
        public LogLevel LogLevel { get; set; }


        // Constructor
        public Logger(
            string timestampFormat = "yyyy-MM-dd HH:mm:ss.fff",
            LogLevel logLevel = LogLevel.INFO,
            bool consoleEnabled = true, ConsoleColor defaultConsoleColor = ConsoleColor.White,
            bool fileEnabled = false, string filePath = ".\\Log.txt",
            string sourceName = null
        ) {
            TimestampFormat = timestampFormat;
            ConsoleOutputEnabled = consoleEnabled;
            DefaultConsoleForegroundColor = defaultConsoleColor;
            LogLevel = logLevel;
            FileOutputEnabled = fileEnabled;
            FilePath = filePath;
            SourceName = sourceName;
            _logLevelColors = new Dictionary<LogLevel, ConsoleColor> {
                { LogLevel.DEBUG, ConsoleColor.Cyan },
                { LogLevel.INFO, DefaultConsoleForegroundColor },
                { LogLevel.SUCCESS, ConsoleColor.Green },
                { LogLevel.WARNING, ConsoleColor.Yellow },
                { LogLevel.ERROR, ConsoleColor.Red },
                { LogLevel.CRITICAL, ConsoleColor.DarkRed }
            };
        }

        // Methods
        private void Log(LogMessage logMessage, OutputPreference outputPreference = OutputPreference.AllEnabled) {
            lock (_lockObj) {
                if (logMessage.LogLevel < LogLevel) { return; }
                string timestampString = logMessage.Timestamp.ToString(TimestampFormat);
                string logOutput;
                if ( SourceName != null ) {
                    logOutput = $"[{timestampString}][{SourceName}] {logMessage.LogLevel}: {logMessage.Message}";
                }
                else {
                    logOutput = $"[{timestampString}] {logMessage.LogLevel}: {logMessage.Message}";
                }
                if (ConsoleOutputEnabled && outputPreference != OutputPreference.FileOnly) {
                    Console.ForegroundColor = _logLevelColors[logMessage.LogLevel];
                    Console.WriteLine(logOutput);
                    Console.ForegroundColor = DefaultConsoleForegroundColor;
                }
                if (FileOutputEnabled && outputPreference != OutputPreference.ConsoleOnly) {
                    File.AppendAllText(FilePath, logOutput + "\r\n");
                }
            }
        }
        public void Debug(string message, OutputPreference outputPreference = OutputPreference.AllEnabled) {
            Log(new LogMessage(LogLevel.DEBUG, message), outputPreference);
        }
        public void Info(string message, OutputPreference outputPreference = OutputPreference.AllEnabled) {
            Log(new LogMessage(LogLevel.INFO, message), outputPreference);
        }
        public void Success(string message, OutputPreference outputPreference = OutputPreference.AllEnabled) {
            Log(new LogMessage(LogLevel.SUCCESS, message), outputPreference);
        }
        public void Warning(string message, OutputPreference outputPreference = OutputPreference.AllEnabled) {
            Log(new LogMessage(LogLevel.WARNING, message), outputPreference);
        }
        public void Error(string message, OutputPreference outputPreference = OutputPreference.AllEnabled) {
            Log(new LogMessage(LogLevel.ERROR, message), outputPreference);
        }
        public void Critical(string message, OutputPreference outputPreference = OutputPreference.AllEnabled) {
            Log(new LogMessage(LogLevel.CRITICAL, message), outputPreference);
        }

    }

}
