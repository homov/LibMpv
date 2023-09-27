using System.ComponentModel;
using System.Globalization;

namespace LibMpv.Client;

public unsafe partial class MpvContext
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public bool IsPaused
    {
        get => GetPropertyFlag(MpvProperty.Pause) ?? true;
        set => SetPropertyFlag(MpvProperty.Pause, value);
    }

    public long PercentPos
    {
        get=> GetPropertyLong(MpvProperty.PercentPos) ?? 0;
        set => SetPropertyLong(MpvProperty.PercentPos,value);
    }


    public long? Volume
    {
        get => GetPropertyLong(MpvProperty.Volume);
        set => SetPropertyLong(MpvProperty.Volume,value ?? 0);
    }

    public bool? IsMuted
    {
        get => GetPropertyFlag(MpvProperty.Mute);
        set => SetPropertyFlag(MpvProperty.Mute, value ?? false);
    }

    public bool IsSeekable
    {
        get => GetPropertyFlag(MpvProperty.Seekable) ?? false;
    }


    public bool IsBuffering
    {
        get => GetPropertyFlag(MpvProperty.PausedForCache) ?? false;
    }

    public long BufferingProgress
    {
        get => GetPropertyLong(MpvProperty.CacheBufferingState) ?? 0;
    }

    public TimeSpan Duration
    {
        get
        {
            var duration = GetPropertyDouble(MpvProperty.Duration);
            if (duration == null)
                return TimeSpan.FromSeconds(0);
            return TimeSpan.FromSeconds(duration.Value);
        }
    }

    public TimeSpan TimeRemaining
    {
        get
        {
            var remain = GetPropertyDouble(MpvProperty.TimeRemaining);
            if (remain == null)
                return TimeSpan.FromSeconds(0);
            return TimeSpan.FromSeconds(remain.Value);
        }
    }

    public TimeSpan Position
    {
        get
        {
            var position = GetPropertyDouble(MpvProperty.TimePos);
            if (position == null)
                return TimeSpan.FromSeconds(0);
            return TimeSpan.FromSeconds(position.Value);
        }
        set
        {
            if (value == null) return;
            var position = (long)value.TotalSeconds;
            Command("seek", position.ToString(CultureInfo.InvariantCulture), "absolute");
        }
    }

    public string? Source
    {
        get => GetPropertyString(MpvProperty.Path);
        set
        {
            if ( value!=null)
            {
                Command("loadfile", value, "replace");
            }
        }
    }

    static Dictionary<string, string> PropertyMap = new()
    {
        [MpvProperty.PausedForCache] = nameof(IsBuffering),
        [MpvProperty.CacheBufferingState] = nameof (BufferingProgress),
        [MpvProperty.Duration] = nameof (Duration),
        [MpvProperty.TimePos] = nameof (Position),
        [MpvProperty.Mute] = nameof (IsMuted),
        [MpvProperty.Volume] = nameof (Volume),
        [MpvProperty.Seekable] = nameof (IsSeekable),
        [MpvProperty.Path] = nameof (Source),
        [MpvProperty.Pause] = nameof(IsPaused),
        [MpvProperty.PercentPos] = nameof(PercentPos),
        [MpvProperty.TimeRemaining] = nameof(TimeRemaining),
    };

    const ulong InternalReplyUserData = 9988776644;

    private void InitInternalPropertyObserver()
    {
        foreach( var key in PropertyMap.Keys )
        {
            LibMpv.MpvObserveProperty(handle, InternalReplyUserData, key, MpvFormat.MpvFormatString);
        }
    }

    private void RaisePropertyChange(string propertyName)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
