using Android.App;
using Android.Content.PM;

using Avalonia;
using Avalonia.Android;
using LibMpv.Client;
using System;

namespace Player.Avalonia.Android;

[Activity(
    Label = "Player.Avalonia.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    public MainActivity()
    {
        Java.Interop.JniEnvironment.References.GetJavaVM(out nint jvmPointer);
        LibMpv.Client.LibMpv.UseLibMpv(0)
            .UseLibraryPath("")
            .InitAndroid(jvmPointer);
    }
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }
}
