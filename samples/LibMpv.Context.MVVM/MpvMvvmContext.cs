using LibMpv.Client;
using System.ComponentModel;
namespace LibMpv.Context.MVVM;

/*
 * 
MPV has a lot of different properties, command and options.
This example provides an idea on how to use some of them.
  
Read MPV documentation:

- https://mpv.io/manual/master/#properties
- https://mpv.io/manual/master/#options
- https://mpv.io/manual/master/#list-of-input-commands

 */

public class MpvMvvmContext : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    // Override it if you need to receive notifications in a specific thread (UI thread for example)
    public virtual void RaisePropertyChanged(string propertyName)
    {
        if (propertyName != null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public MpvMvvmContext()
    {
        // Register propertyes for observation
        foreach (var observableProperty in observableProperties)
            Context.ObserveProperty(observableProperty.LibMpvName, observableProperty.LibMpvFormat, 0);

        // Register router LibMpv => MVVM
        Context.PropertyChanged += MpvContextPropertyChanged;

    }

    // Route property changed events to MVVM context
    private void MpvContextPropertyChanged(object? sender, MpvPropertyEventArgs e)
    {
        if (!String.IsNullOrEmpty(e.Name))
        {
            // If there will be a lot of properties, it might be better to do a dictionary lookup
            var observableProperty = observableProperties.FirstOrDefault(it=>it.LibMpvName == e.Name);
            if (observableProperty != null)
            {
                RaisePropertyChanged(observableProperty.MvvmName);
            }
        }
    }


    public MpvContext? Context { get; } = new MpvContext();


    // Some commands. Full list: https://mpv.io/manual/master/#list-of-input-commands

    // Load media
    public void Load(string mediaLocation)
    {
        Context?.Command("loadfile", mediaLocation, "replace");
    }

    // Load media and play
    public void Play(string mediaLocation)
    {
        Context?.Command("loadfile", mediaLocation, "replace");
        Paused = false;
    }

    public void Stop()
    {
        Context?.Command("stop");
    }


    // Some properties. Full list: https://mpv.io/manual/master/#properties

    public long? Duration
    {
        get => Context?.GetPropertyLong("duration");
        set 
        {
            if (value == null) return;
            Context?.SetPropertyLong("duration",value.Value);
        }
    }

    public long? TimePos 
    {
        get => Context?.GetPropertyLong("time-pos");
        set
        {
            if (value == null) return;
            Context?.SetPropertyLong("time-pos", value.Value);
        }
    }

    public bool? Paused
    {
        get => Context?.GetPropertyFlag("pause");
        set
        {
            if (value == null) return;
            Context?.SetPropertyFlag("pause", value.Value);
        }
    }

    public double? PlaybackSpeed
    {
        get => Context?.GetPropertyDouble("speed");
        set
        {
            if (value == null) return;
            Context?.SetPropertyDouble("speed", value.Value);
        }
    }


    static PropertyToObserve[] observableProperties = new PropertyToObserve[]
    {
        new() { MvvmName=nameof(Duration), LibMpvName="duration", LibMpvFormat = mpv_format.MPV_FORMAT_INT64 },
        new() { MvvmName=nameof(TimePos), LibMpvName="time-pos", LibMpvFormat = mpv_format.MPV_FORMAT_INT64 },
        new() { MvvmName=nameof(Paused), LibMpvName="pause", LibMpvFormat = mpv_format.MPV_FORMAT_FLAG },
        new() { MvvmName=nameof(PlaybackSpeed), LibMpvName="speed", LibMpvFormat = mpv_format.MPV_FORMAT_DOUBLE }
    };

}