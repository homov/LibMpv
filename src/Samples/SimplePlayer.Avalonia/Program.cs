using Avalonia;
using System;

namespace SimplePlayer.Avalonia
{
    internal class Program
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

        internal static void InitMpv()
        {
            var platform = IntPtr.Size == 8 ? "x86_64" : "x86";
            if (OperatingSystem.IsWindows())
            {
                var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, platform);
                LibMpv.Client.LibMpv.UseLibMpv(2).UseLibraryPath(path);
            }
            else
            {
                var path = $"/usr/lib/{platform}-linux-gnu";
                LibMpv.Client.LibMpv.UseLibMpv(0).UseLibraryPath(path);
            }
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();
    }
}