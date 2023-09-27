using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentAvalonia.UI.Controls;
using LibMpv.Client;
using System;
using System.ComponentModel;
using Avalonia;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePlayer.Avalonia;

public partial class MainWindowViewModel : ObservableObject
{

    [ObservableProperty] private MpvContext context;
    [ObservableProperty] private Symbol playPauseSymbol = Symbol.PlayFilled;
    [ObservableProperty] private string currentMedia;
    [ObservableProperty] private bool volumeEditorVisible = false;
    [ObservableProperty] private bool isMediaLoaded = false;
    [ObservableProperty] private double currentPosition = 0;

    public MainWindowViewModel()
    {
        context = new MpvContext();
        context.Volume = 50;
        context.PropertyChanged += Context_PropertyChanged;
        context.StartFile += Context_StartFile;
        context.EndFile += Context_EndFile;
    }

    private void Context_EndFile(object? sender, MpvEndFileEventArgs e)
    {
        IsMediaLoaded = true;
        PlayPauseSymbol = Symbol.PlayFilled;
        CurrentPosition = 0;
    }

    private void Context_StartFile(object? sender, EventArgs e)
    {
        IsMediaLoaded = true;
        PlayPauseSymbol = Symbol.PauseFilled;
    }


    [RelayCommand]
    async Task OpenFile()
    {
        var storageProvider = GetStorageProvider();
        if (storageProvider != null)
        {
            var result = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Open file",
                FileTypeFilter = new List<FilePickerFileType> { 
                    new FilePickerFileType("All files") { Patterns=new string[]{"*.*"}, MimeTypes=new string[]{"*/*"} }
                },
                AllowMultiple = false
            });

            var file = result.FirstOrDefault();
            if (file != null)
            {
                Play( file.Path.LocalPath );
            }
        }
    }

    private void Play(string media)
    {
        CurrentMedia = media;
        context.Stop();
        context.LoadFile(CurrentMedia);
    }

    [RelayCommand]
    void ToggleVolumeEditor()
    {
        VolumeEditorVisible = !VolumeEditorVisible;
    }


    [RelayCommand] 
    void TogglePlayPause()
    {
        if (String.IsNullOrEmpty(CurrentMedia))
        {
            Play("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4");
        }
        else
        {
            context.IsPaused = !context.IsPaused;
        }
    }

    private void Context_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if ( e.PropertyName == nameof(context.IsPaused) )
        {
            if (context.IsPaused || !IsMediaLoaded )
            {
                PlayPauseSymbol = Symbol.PlayFilled;
            }
            else
            {
                PlayPauseSymbol = Symbol.PauseFilled;
            }
        }
    }

    private static IStorageProvider? GetStorageProvider()
    {
        switch (Application.Current?.ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime { MainWindow: { } window }:
                return window.StorageProvider;
        }
        return null;
    }
}
