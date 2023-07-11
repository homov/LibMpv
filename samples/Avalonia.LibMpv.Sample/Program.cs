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
            FindLibMpv();

            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        private static void FindLibMpv()
        {
            var libMpvVersions = new[] { 2, 1 };

            // Search libmpv path on Linux
            if (FunctionResolverFactory.GetPlatformId() == PlatformID.Unix)
            {
                var libraryFolders = new[] {
                    "/lib/x86_64-linux-gnu",
                    "/usr/lib"
                };

                foreach (var folder in libraryFolders)
                {
                    foreach (var version in libMpvVersions)
                    {
                        var fullPath = System.IO.Path.Combine(folder, $"libmpv.so.{version}");
                        if (System.IO.File.Exists(fullPath))
                        {
                            //Set path and libmpv version
                            libmpv.RootPath = folder;
                            libmpv.LibraryVersionMap["libmpv"] = version;
                            return;
                        }
                    }
                }
            }
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
    }
}