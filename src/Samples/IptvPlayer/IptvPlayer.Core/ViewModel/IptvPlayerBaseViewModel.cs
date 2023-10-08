using CommunityToolkit.Mvvm.ComponentModel;
using IptvPlayer.Core.Services;
using IptvPlayer.Core.Model;
using LibMpv.MVVM;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;

namespace IptvPlayer.Core.ViewModel;

public abstract partial class IptvPlayerBaseViewModel: BaseMpvContextViewModel
{
    [ObservableProperty] private bool isPlayListVisible = false;
    
    [ObservableProperty] private bool isChannelListVisible = false;
    
    [ObservableProperty] private bool isGroupListVisible = false;
    
    [ObservableProperty] private bool isSettigsVisible = false;

    [ObservableProperty] private string currentChannelGroup = String.Empty;

    [ObservableProperty] private IptvChannel? currentChannel;

    public ObservableCollection<IptvChannel> CurrentChannelList { get; } = new();

    public ObservableCollection<string> ChannelGroups { get; } = new();

    [ObservableProperty] private string playListSource;

    private IptvPlaylist playlist;

    public IptvPlayerBaseViewModel()
    {
        InitializeModel();
    }

    public async Task InitializeModel()
    {
        playListSource = SettingsService.Instance.GetPlayList();

        playlist = await M3UIptvListLoader.Load(playListSource);

        // Cleanup some channels
        if (playListSource.IndexOf("iptv-org.github.io") >= 0)
        {
            var removeChannels = playlist.Channels.Where(it =>
                it.ChannelName.IndexOf("geo-blocked", StringComparison.InvariantCultureIgnoreCase) > 0
                || it.ChannelName.IndexOf("not 24/7", StringComparison.InvariantCultureIgnoreCase) > 0).ToList();

            foreach (var channel in removeChannels)
                playlist.Channels.Remove(channel);
        }

        var groups = playlist.Channels.Select(it => it.GroupTitle).Distinct().OrderBy(it => it);

        InvokeInUIThread(() =>
        {
            CurrentChannelList.Clear();
            ChannelGroups.Clear();
            ChannelGroups.Add(Settings.AllChannels);

            foreach (var group in groups)
                ChannelGroups.Add(group);

            var lastGroup = SettingsService.Instance.GetChannelGroup();
            CurrentChannelGroup = ChannelGroups.Contains(lastGroup) ? lastGroup : Settings.AllChannels;
        });
    }

    partial void OnCurrentChannelGroupChanged(string value)
    {
        IEnumerable<IptvChannel> channels = playlist.Channels;

        if (value != Settings.AllChannels)
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
        if (value != null)
        {
            this.Command("show-text", value.ChannelName, "4000");
            this.LoadFile(value.URL);
            this.Play();
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
}
