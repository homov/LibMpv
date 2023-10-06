using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentAvalonia.UI.Controls;
using IptvPlayer.Core;
using IptvPlayer.Model;
using IptvPlayer.Services;
using LibMpv.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace IptvPlayer.ViewModels;

public partial class MainViewModel : BaseMpvContextViewModel
{

    [ObservableProperty] private Symbol playPauseSymbol = Symbol.PlayFilled;
    [ObservableProperty] private Symbol muteUnmuteSymbol = Symbol.Volume;
    [ObservableProperty] private string currentChannelGroup = String.Empty;
    [ObservableProperty] private IptvChannel? currentChannel;
    [ObservableProperty] private bool isPlayListVisible = false;
    [ObservableProperty] private bool isChannelListVisible = true;
    [ObservableProperty] private bool isGroupListVisible = false;
    [ObservableProperty] private bool isSettigsVisible = false;
    [ObservableProperty] private string playListSource;

    public ObservableCollection<IptvChannel> CurrentChannelList { get; } = new();
    public ObservableCollection<string> ChannelGroups { get; } = new();

    private IptvPlaylist playlist;

    public MainViewModel()
    {
        this.Volume = 50;
        this.PropertyChanged += MainViewModel_PropertyChanged;
        InitializeModel();
    }

    partial void OnCurrentChannelGroupChanged(string value)
    {
        IEnumerable<IptvChannel> channels = playlist.Channels;
        
        if (value !=  SettingsModel.AllChannels)
            channels = channels.Where(it => it.GroupTitle == value);
        
        InvokeInUIThread(() =>
        {
            CurrentChannelList.Clear();
            foreach (var channel in channels)
                CurrentChannelList.Add(channel);
            CurrentChannel = null;
            ShowChannelList();
        });
        
        SettingsService.Instance.SetChannelGroup(value);
    }

    partial void OnCurrentChannelChanged(IptvChannel? value)
    {
        this.Stop();
        if (value!=null)
        {
            this.LoadFile(value.URL);
            this.Play();
        }
    }
    private async Task InitializeModel()
    {
        playListSource = SettingsService.Instance.GetPlayList();

        playlist = await M3UIptvListLoader.Load(playListSource);

        // Cleanup some channels
        if (playListSource.IndexOf("iptv-org.github.io") >=0 )
        {
            var removeChannels = playlist.Channels.Where(it => 
                it.ChannelName.IndexOf("geo-blocked", StringComparison.InvariantCultureIgnoreCase) > 0 
                || it.ChannelName.IndexOf("not 24/7", StringComparison.InvariantCultureIgnoreCase) > 0).ToList();

            foreach(var channel in removeChannels)
                playlist.Channels.Remove(channel);
        }

        var groups = playlist.Channels.Select(it => it.GroupTitle).Distinct().OrderBy(it => it);

        InvokeInUIThread(() =>
        {
            CurrentChannelList.Clear();
            ChannelGroups.Clear();
            ChannelGroups.Add(SettingsModel.AllChannels);   

            foreach (var group in groups)
                ChannelGroups.Add(group);

            var lastGroup = SettingsService.Instance.GetChannelGroup();
            CurrentChannelGroup = ChannelGroups.Contains(lastGroup) ? lastGroup : SettingsModel.AllChannels;
        });
    }

    private void MainViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(this.IsInPlayMode))
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
    private void ToggleSettingsVisibility()
    {
        IsSettigsVisible = !IsSettigsVisible;
        
        if (IsSettigsVisible)
            IsPlayListVisible = false;
    }

    [RelayCommand]
    private void SaveSettings()
    {
        if (!String.IsNullOrEmpty(playListSource))
        {
            SettingsService.Instance.SetPlayList(playListSource);
        }
        IsSettigsVisible = false;
        InitializeModel();
    }

    [RelayCommand]
    private void CloseSettings()
    {
        IsSettigsVisible = false;
    }


    [RelayCommand]
    private void TogglePlayListVisibility()
    {
        IsPlayListVisible = !IsPlayListVisible;
        if (IsPlayListVisible)
        {
            IsGroupListVisible = false;
            IsChannelListVisible = true;
        }
    }

    [RelayCommand]
    private void ShowGroupList()
    {
        IsChannelListVisible = false;
        IsGroupListVisible = true;
    }

    [RelayCommand]
    private void ShowChannelList()
    {
        IsGroupListVisible = false;
        IsChannelListVisible = true;
    }

    public override void InvokeInUIThread(Action action)
    {
        Dispatcher.UIThread.Invoke(action);
    }
}
