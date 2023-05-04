using LibMpv.Client;

namespace Avalonia.LibMpv.Android.Sample.ViewModels;

public class MainViewModel 
{
    public MpvContext Context { get; } = new MpvContext();

    public MainViewModel()
    {
        Context.SetOptionString("force-window", "no");
    }

    public void Play()
    {
        Context.Command("loadfile", "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4", "replace");
        Context.SetOptionString("force-window", "yes");
        Context.SetPropertyFlag("pause", false);
    }

    public void Stop()
    {
        Context.Command("stop");
    }
}
