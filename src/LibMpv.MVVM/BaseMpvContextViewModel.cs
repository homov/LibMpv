using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibMpv.Client;
using System;
using System.Globalization;

namespace LibMpv.MVVM;

[ObservableObject]
public abstract partial class BaseMpvContextViewModel: MpvContext
{
    PlayerState playerState = PlayerState.DoesNotPlay;
    public PlayerState PlayerState => playerState;

    public event Action<PlayerState,PlayerState> PlayerStateChanging;

    public event Action<PlayerState> PlayerStateChanged;

    public bool IsPaused
    {
        get => this.GetPropertyFlag("pause") ?? true;
    }

    public long Volume
    {
        get => this.GetPropertyLong("volume") ?? 0;
        set => this.SetPropertyLong("volume", value);
    }

    public bool IsMuted
    {
        get => this.GetPropertyFlag("mute") ?? false;
    }

    public bool IsSeekable
    {
        get => this.GetPropertyFlag("seekable") ?? false;
    }

    public bool IsBuffering
    {
        get => this.GetPropertyFlag("paused-for-cache") ?? false;
    }

    public long CacheBufferingState
    {
        get => this.GetPropertyLong("cache-buffering-state") ?? 0;
    }

    public TimeSpan Duration
    {
        get
        {
            var duration = this.GetPropertyDouble("duration");
            if (duration == null)
                return TimeSpan.FromSeconds(0);
            return TimeSpan.FromSeconds(duration.Value);
        }
    }

    public TimeSpan TimeRemaining
    {
        get
        {
            var remain = this.GetPropertyDouble("time-remaining");
            if (remain == null)
                return TimeSpan.FromSeconds(0);
            return TimeSpan.FromSeconds(remain.Value);
        }
    }

    public TimeSpan TimePos
    {
        get
        {
            var position = this.GetPropertyDouble("time-pos");
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

    public long PercentPos
    {
        get => this.GetPropertyLong("percent-pos") ?? 0;
        set
        {
            if (value >=0 && value <= 100)
                this.SetPropertyLong("percent-pos", value);
        }
    }

    public string Path
    {
        get => this.GetPropertyString("path");
        set
        {
            if (value != null)
            {
                this.Stop();
                this.Command("loadfile", value, "replace");
            }
        }
    }

    public BaseMpvContextViewModel()
    {
        InitPropertyObserver();
        this.EndFile += Context_EndFile;
        this.FileLoaded += Context_FileLoaded;
        this.StartFile += Context_StartFile;
    }

    public abstract void InvokeInUIThread(Action action);

    private void RaisePropertyChangedInUIThread(string propertyName)
    {
        InvokeInUIThread(() => OnPropertyChanged(propertyName));
    }

    private void SetPlayerState(PlayerState newState)
    {
        System.Diagnostics.Debug.WriteLine($"Player state: {newState}");
        InvokeInUIThread(() =>
        {
            PlayerStateChanging?.Invoke(playerState, newState);

            playerState = newState;

            OnPropertyChanged(nameof(PlayerState));

            PlayerStateChanged?.Invoke(playerState);

            StopCommand.NotifyCanExecuteChanged();
            PlayCommand.NotifyCanExecuteChanged();
            PauseCommand.NotifyCanExecuteChanged();
            TogglePlayPauseCommand.NotifyCanExecuteChanged();
        });
    }

    [RelayCommand]
    protected void ToggleMute()
    {
        this.Command("cycle", "mute");
    }

    [RelayCommand(CanExecute = nameof(CanMute))]
    public void Mute()
    {
        this.SetPropertyFlag("mute", true);
    }

    bool CanMute()
    {
        return !IsMuted;
    }

    [RelayCommand(CanExecute = nameof(CanUnMute))]
    public void UnMute()
    {
        this.SetPropertyFlag("mute", false);
    }

    bool CanUnMute()
    {
        return IsMuted;
    }

    [RelayCommand(CanExecute = nameof(CanStop))]
    public void Stop()
    {
        this.Command("stop");
    }

    bool CanStop()
    {
        return PlayerState != PlayerState.DoesNotPlay;
    }

    [RelayCommand(CanExecute = nameof(CanTogglePlayPause))]
    public void TogglePlayPause()
    {
        this.Command("cycle", "pause");
    }

    bool CanTogglePlayPause()
    {
        return this.PlayerState == PlayerState.Playing || this.PlayerState == PlayerState.Paused;
    }

    [RelayCommand(CanExecute = nameof(CanPlay))]
    public void Play()
    {
        this.SetPropertyFlag("pause", false);
    }

    bool CanPlay()
    {
        return this.PlayerState == PlayerState.Paused;
    }

    [RelayCommand(CanExecute = nameof(CanPause))]
    public void Pause()
    {
        this.SetPropertyFlag("pause", true);
    }

    bool CanPause()
    {
        return this.PlayerState == PlayerState.Playing;
    }

    private void Context_StartFile(object sender, EventArgs e)
    {
        SetPlayerState(PlayerState.Loading);
    }

    private void Context_FileLoaded(object sender, EventArgs e)
    {
        SetPlayerState(PlayerState.Playing);
    }

    private void Context_EndFile(object sender, MpvEndFileEventArgs e)
    {
        SetPlayerState(PlayerState.DoesNotPlay);
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
        Command("frame-back-step");
    }

    protected virtual void InitPropertyObserver()
    {
        this.ObserveProperty("paused-for-cache", (bool isBuffering) => 
        {
            RaisePropertyChangedInUIThread(nameof(IsBuffering));
            if (isBuffering)
                SetPlayerState(PlayerState.Buffering);
            else if (!String.IsNullOrEmpty(Path))
            {
                if (this.PlayerState != PlayerState.Playing)
                    SetPlayerState(PlayerState.Playing);
            }
            else
            {
                SetPlayerState(PlayerState.DoesNotPlay);
            }
        });

        this.ObserveProperty("pause", (bool isPaused) =>
        {
            RaisePropertyChangedInUIThread(nameof(IsPaused));
            if (isPaused)
                SetPlayerState(PlayerState.Paused);
            else if  (!String.IsNullOrEmpty(Path))
                SetPlayerState(PlayerState.Playing);
            else
            {
                SetPlayerState(PlayerState.DoesNotPlay);
            }
        });


        this.ObserveProperty("cache-buffering-state", () => RaisePropertyChangedInUIThread(nameof(CacheBufferingState)) );
        this.ObserveProperty("duration", () => RaisePropertyChangedInUIThread(nameof(Duration)) );
        this.ObserveProperty("time-pos", () => RaisePropertyChangedInUIThread(nameof(TimePos)) );
        this.ObserveProperty("time-remaining", () => RaisePropertyChangedInUIThread(nameof(TimeRemaining)) );
        this.ObserveProperty("mute", () => RaisePropertyChangedInUIThread(nameof(IsMuted)) );
        this.ObserveProperty("volume", () => RaisePropertyChangedInUIThread(nameof(Volume)) );
        this.ObserveProperty("seekable", () => RaisePropertyChangedInUIThread(nameof(IsSeekable)) );
        this.ObserveProperty("path", () => RaisePropertyChangedInUIThread(nameof(Path)) );
        this.ObserveProperty("percent-pos", () => RaisePropertyChangedInUIThread(nameof(PercentPos)) );
    }
  
}
