using System;
using System.Windows;

namespace SimplePlayer.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            this.InitMpv();
        }

        internal void InitMpv()
        {
            var suffix = IntPtr.Size == 8 ? "x86_64" : "x86";
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, suffix);
            LibMpv.Client.LibMpv.UseLibMpv(2).UseLibraryPath(suffix);
        }
    }
}
