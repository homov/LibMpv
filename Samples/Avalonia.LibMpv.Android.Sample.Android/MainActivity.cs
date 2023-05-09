using Android.App;
using Android.Content.PM;
using Avalonia.Android;

namespace Avalonia.LibMpv.Android.Sample.Android;

[Activity(Label = "Avalonia.LibMpv.Android.Sample.Android", Theme = "@style/MyTheme.NoActionBar", Icon = "@drawable/icon", LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
public class MainActivity : AvaloniaMainActivity
{
    public MainActivity()
    {
    }
}
