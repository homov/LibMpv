using Avalonia;
using Avalonia.OpenGL;
using LibMpv.Client;
using System;

namespace Avalonia.LibMpv.Sample
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            // Linux libmpv path
            if (FunctionResolverFactory.GetPlatformId() == PlatformID.Unix)
            {
                libmpv.RootPath = "/lib/x86_64-linux-gnu";

                // If there is version 1 of libmpv, then load it instead of verison 2
                if (System.IO.File.Exists(System.IO.Path.Combine(libmpv.RootPath, "libmpv.so.1")))
                {
                    
                    libmpv.LibraryVersionMap["libmpv"] = 1;
                }
            }

            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
    }
}