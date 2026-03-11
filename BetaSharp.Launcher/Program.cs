using Avalonia;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using BetaSharp.Launcher;
using Serilog;

if (args[0] == "debug")
{
    string directory = Path.Combine(AppContext.BaseDirectory, "Client");

    var info = new ProcessStartInfo
    {
        CreateNoWindow = true,
        FileName = Path.Combine(directory, "BetaSharp.Client"),
        WorkingDirectory = directory
    };

    using var process = Process.Start(info);

    ArgumentNullException.ThrowIfNull(process);

    await process.WaitForExitAsync();  return;
}

try
{
    Start(args);
}
catch (Exception exception)
{
    Log.Fatal(exception, "An unhandled exception occurred");
    throw;
}
finally
{
    Log.CloseAndFlush();
}

return;

[STAThread]
static void Start(string[] args)
{
    AppBuilder
        .Configure<App>()
        .UsePlatformDetect()
        .WithInterFont()
        .LogToTrace()
        .StartWithClassicDesktopLifetime(args);
}
