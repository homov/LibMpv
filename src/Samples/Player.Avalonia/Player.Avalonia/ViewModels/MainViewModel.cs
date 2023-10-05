﻿using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentAvalonia.UI.Controls;
using LibMpv.Client;
using LibMpv.MVVM;
using System;

namespace Player.Avalonia.ViewModels;

public partial class MainViewModel : BaseMpvContextViewModel
{
    [ObservableProperty] private Symbol playPauseSymbol = Symbol.PlayFilled;
    [ObservableProperty] private Symbol muteUnmuteSymbol = Symbol.Volume;

    public bool IsTextDurationsVisible => FunctionResolverFactory.GetPlatformId() != LibMpvPlatformID.Android;
    public MainViewModel()
    {
        this.Volume = 50;
        this.PropertyChanged += MainViewModel_PropertyChanged;
    }

    private void MainViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName ==  nameof(this.IsInPlayMode)) 
        { 
            if (IsInPlayMode)
                PlayPauseSymbol = this.IsPaused ? Symbol.PlayFilled : Symbol.PauseFilled;
            else
                PlayPauseSymbol = Symbol.PlayFilled;
        }
        else if (e.PropertyName == nameof(this.IsPaused))
        {
            PlayPauseSymbol = this.IsPaused || string.IsNullOrEmpty(Source) ? Symbol.PlayFilled : Symbol.PauseFilled;
        }
        else if (e.PropertyName == nameof(this.IsMuted))
        {
            MuteUnmuteSymbol = this.IsMuted ? Symbol.Mute : Symbol.Volume;
        }
    }


    [RelayCommand]
    private void PlayBigBuckBunny()
    {
        this.LoadFile("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4");
        this.Play();
    }

    public override void InvokeInUIThread(Action action)
    {
        Dispatcher.UIThread.Invoke( action );
    }
}
