using StaticLog = Logger.StaticLogger;
using InstLogger = Logger.Logger;
using Logger;

internal class Program {
    private static void Main(string[] args) {
        Console.WriteLine("This application targets .NET 8");
        var logger1 = new InstLogger(fileEnabled: true, filePath: ".\\Log1.txt", sourceName: "worker 1");
        var logger2 = new InstLogger(fileEnabled: true, filePath: ".\\Log2.txt", sourceName: "worker 2");
        logger1.Info("This is a message from worker 1.");
        logger2.Info("This is a message from worker 2.");
        Console.ReadLine();
    }
}