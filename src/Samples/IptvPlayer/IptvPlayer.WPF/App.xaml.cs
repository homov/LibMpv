using LibMpv.Client;
using System;
using System.Windows;

namespace IptvPlayer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitMpv();
        }

        private void InitMpv()
        {
            var platform = IntPtr.Size == 8 ? "x86_64" : "x86";
            var platformId = FunctionResolverFactory.GetPlatformId();
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, platform);
            LibMpv.Client.LibMpv.UseLibMpv(2).UseLibraryPath(path);
        }
    }
}
