using Logger;
using System;
using Log = Logger.Logger;

namespace ConsoleApp1 {
    internal class Program {
        static void Main(string[] args) {
            Log.ConsoleOutputEnabled = true;
            Log.FileOutputEnabled = true;
            Log.FilePath = "C:\\test.txt";
            Log.LogLevel = Logger.LogLevel.DEBUG;
            Console.WriteLine("This application targets .NET Framework 4.8.");
            Log.Debug("Debug message", OutputPreference.FileOnly);
            Log.Info("Info message", OutputPreference.ConsoleOnly);
            Log.Success("Success message", OutputPreference.FileOnly);
            Log.Warning("Warning message", OutputPreference.ConsoleOnly);
            Log.Error("Error message", OutputPreference.FileOnly);
            Log.Critical("Critical message", OutputPreference.ConsoleOnly);
            Console.ReadLine();
        }
    }
}
