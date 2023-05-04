using LibMpv.Client;
using System.Diagnostics;

namespace Avalonia.LibMpv.Android.Sample.ViewModels;

public class MainViewModel 
{
    public MpvContext Context { get; } = new MpvContext();

    public MainViewModel()
    {
        Context.RequestLogMessages("debug");
        Context.LogMessage += Context_LogMessage;
    }

    private void Context_LogMessage(object? sender, MpvLogMessageEventArgs e)
    {
        Debug.WriteLine($"MpvContext: {e.Level} {e.Text}");
    }

    public void Play()
    {
        Context.Command("loadfile", "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4", "replace");
        Context.SetPropertyFlag("pause", false);
    }

    public void Stop()
    {
        Context.Command("stop");
    }
}
