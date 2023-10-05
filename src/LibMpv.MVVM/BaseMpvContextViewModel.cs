using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibMpv.Client;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace LibMpv.MVVM;

[ObservableObject]
public abstract partial class BaseMpvContextViewModel: MpvContext
{
    public bool IsPaused
    {
        get => this.GetPropertyFlag(MpvProperty.Pause) ?? true;
    }

    public long Volume
    {
        get => this.GetPropertyLong(MpvProperty.Volume) ?? 0;
        set => this.SetPropertyLong(MpvProperty.Volume, value);
    }

    public bool IsMuted
    {
        get => this.GetPropertyFlag(MpvProperty.Mute) ?? false;
    }

    public bool IsSeekable
    {
        get => this.GetPropertyFlag(MpvProperty.Seekable) ?? false;
    }

    public bool IsBuffering
    {
        get => this.GetPropertyFlag(MpvProperty.PausedForCache) ?? false;
    }

    public long BufferingProgress
    {
        get => this.GetPropertyLong(MpvProperty.CacheBufferingState) ?? 0;
    }

    public TimeSpan Duration
    {
        get
        {
            var duration = this.GetPropertyDouble(MpvProperty.Duration);
            if (duration == null)
                return TimeSpan.FromSeconds(0);
            return TimeSpan.FromSeconds(duration.Value);
        }
    }

    public TimeSpan Remaining
    {
        get
        {
            var remain = this.GetPropertyDouble(MpvProperty.TimeRemaining);
            if (remain == null)
                return TimeSpan.FromSeconds(0);
            return TimeSpan.FromSeconds(remain.Value);
        }
    }

    public TimeSpan Position
    {
        get
        {
            var position = this.GetPropertyDouble(MpvProperty.TimePos);
            if (position == null)
                return TimeSpan.FromSeconds(0);
            return TimeSpan.FromSeconds(position.Value);
        }
        set
        {
            if (value == null ) return;
            var position = (long)value.TotalSeconds;
            this.Command("seek", position.ToString(CultureInfo.InvariantCulture), "absolute");
        }
    }

    public long PercentPosition
    {
        get => this.GetPropertyLong(MpvProperty.PercentPos) ?? 0;
        set
        {
            if (value >=0 && value <= 100)
                this.SetPropertyLong(MpvProperty.PercentPos, value);
        }
    }


    public string Source
    {
        get => this.GetPropertyString(MpvProperty.Path);
        set
        {
            if (value != null)
            {
                if (IsInPlayMode) this.Command("stop");
                this.Command("loadfile", value, "replace");
            }
        }
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StopCommand))]
    [NotifyCanExecuteChangedFor(nameof(PlayCommand))]
    [NotifyCanExecuteChangedFor(nameof(PauseCommand))]
    [NotifyCanExecuteChangedFor(nameof(TogglePlayPauseCommand))]
    bool isInPlayMode = false;

    public BaseMpvContextViewModel()
    {
        InitPropertyObserver();
        this.EndFile += Context_EndFile;
        this.FileLoaded += Context_FileLoaded;
        this.StartFile += Context_StartFile;
    }

    public abstract void InvokeInUIThread(Action action);


    [RelayCommand]
    protected void ToggleMute()
    {
        this.Command("cycle", MpvProperty.Mute);
    }

    [RelayCommand(CanExecute = nameof(CanMute))]
    protected void Mute()
    {
        this.SetPropertyFlag(MpvProperty.Mute, true);
    }

    bool CanMute()
    {
        return !IsMuted;
    }

    [RelayCommand(CanExecute = nameof(CanUnMute))]
    protected void UnMute()
    {
        this.SetPropertyFlag(MpvProperty.Mute, false);
    }

    bool CanUnMute()
    {
        return IsMuted;
    }

    [RelayCommand(CanExecute = nameof(CanStop))]
    protected void Stop()
    {
        this.Command("stop");
    }

    bool CanStop()
    {
        return IsInPlayMode;
    }

    [RelayCommand(CanExecute = nameof(CanTogglePlayPause))]
    protected void TogglePlayPause()
    {
        this.Command("cycle", MpvProperty.Pause);
    }

    bool CanTogglePlayPause()
    {
        return IsInPlayMode;
    }

    [RelayCommand(CanExecute = nameof(CanPlay))]
    protected void Play()
    {
        this.SetPropertyFlag(MpvProperty.Pause, false);
    }

    bool CanPlay()
    {
        return IsInPlayMode && IsPaused;
    }

    [RelayCommand(CanExecute = nameof(CanPause))]
    protected void Pause()
    {
        this.SetPropertyFlag(MpvProperty.Pause, true);
    }

    bool CanPause()
    {
        return IsInPlayMode && !IsPaused;
    }

    private void Context_StartFile(object sender, EventArgs e)
    {
        OnPlayStopped(null);
    }

    private void Context_FileLoaded(object sender, EventArgs e)
    {
        OnPlayStarted();
    }

    private void Context_EndFile(object sender, MpvEndFileEventArgs e)
    {
        OnPlayStopped(e.EventData);
    }

    public virtual void OnPlayStarted()
    {
        InvokeInUIThread( ()=> IsInPlayMode = true);
    }

    public virtual void OnPlayStopped(MpvEventEndFile? mpvEventEndFile)
    {
        InvokeInUIThread(() => IsInPlayMode = false);
    }

    public virtual void MpvPropertyChangedHandler(string propertyName)
    {
        InvokeInUIThread(() =>
        {
            OnPropertyChanged(propertyName);
            if (propertyName == nameof(IsPaused))
            {
                PauseCommand.NotifyCanExecuteChanged();
                PlayCommand.NotifyCanExecuteChanged();
            }
            else if (propertyName == nameof(IsMuted))
            {
                MuteCommand.NotifyCanExecuteChanged();
                UnMuteCommand.NotifyCanExecuteChanged();
            }
        });
    }
    public void LoadFile(string fileName, string mode = "replace")
    {
        Command("loadfile", fileName, mode);
    }

    public void Seek(long amount, string reference = "relative", string precision = "keyframes")
    {
        Command("seek", amount.ToString(), reference, precision);
    }

    public void FrameStep()
    {
        Command("frame-step");
    }

    public void FrameBackStep()
    {
        Command("frame_back_step");
    }

    void Context_MpvPropertyChanged(object sender, MpvGetPropertyReplyEventArgs e)
    {
        if (e.ReplyUserData == InternalReplyUserData)
        {
            string propertyName;
            if (PropertyMap.TryGetValue(e.EventData.Name, out propertyName))
            {
                MpvPropertyChangedHandler( propertyName );
            }
        }
    }

    protected const ulong InternalReplyUserData = 9988776644;

    protected virtual void InitPropertyObserver()
    {
        this.MpvPropertyChanged += Context_MpvPropertyChanged;
        foreach (var key in PropertyMap.Keys)
        {
            this.ObserveProperty(InternalReplyUserData, key);
        }
    }

    protected Dictionary<string, string> PropertyMap = new()
    {
        [MpvProperty.PausedForCache] = nameof(IsBuffering),
        [MpvProperty.CacheBufferingState] = nameof(BufferingProgress),
        [MpvProperty.Duration] = nameof(Duration),
        [MpvProperty.TimePos] = nameof(Position),
        [MpvProperty.TimeRemaining] = nameof(Remaining),
        [MpvProperty.Mute] = nameof(IsMuted),
        [MpvProperty.Volume] = nameof(Volume),
        [MpvProperty.Seekable] = nameof(IsSeekable),
        [MpvProperty.Path] = nameof(Source),
        [MpvProperty.Pause] = nameof(IsPaused),
        [MpvProperty.PercentPos] = nameof(PercentPosition),
    };
    
}
