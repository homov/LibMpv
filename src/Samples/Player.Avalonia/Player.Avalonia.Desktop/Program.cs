using System;

using Avalonia;
using LibMpv.Client;

namespace Player.Avalonia.Desktop;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        InitMpv();
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();

    public static void InitMpv()
    {
        var platform = IntPtr.Size == 8 ? "x86_64" : "x86";
        var platformId = FunctionResolverFactory.GetPlatformId();
        if (platformId == LibMpvPlatformID.Win32NT)
        {
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, platform);
            LibMpv.Client.LibMpv.UseLibMpv(2).UseLibraryPath(path);
        }
        else if (platformId == LibMpvPlatformID.Unix)
        {
            var path = $"/usr/lib/{platform}-linux-gnu";
            LibMpv.Client.LibMpv.UseLibMpv(0).UseLibraryPath(path);
        }
    }

}
