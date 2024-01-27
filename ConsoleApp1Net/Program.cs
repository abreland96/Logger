using Log = Logger.Logger;

Log.ConsoleOutputEnabled = true;
Log.FileOutputEnabled = true;
Log.FilePath = "C:\\test.txt";
Log.LogLevel = Logger.LogLevel.DEBUG;
Console.WriteLine("This application targets .NET 8");
Log.Debug("Debug message");
Log.Info("Info message");
Log.Success("Success message");
Log.Warning("Warning message");
Log.Error("Error message");
Log.Critical("Critical message");
Log.Critical("Test commit");
Console.ReadLine();