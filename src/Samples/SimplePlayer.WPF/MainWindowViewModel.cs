using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibMpv.Client;
using MahApps.Metro.IconPacks;
using Microsoft.Win32;
using System;
using System.ComponentModel;

namespace SimplePlayer.WPF;

public partial class MainWindowViewModel : ObservableObject
{

    [ObservableProperty] private MpvContext context;
    [ObservableProperty] private PackIconMaterialKind playPauseSymbol = PackIconMaterialKind.Play;
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
        PlayPauseSymbol = PackIconMaterialKind.Play;
        CurrentPosition = 0;
    }

    private void Context_StartFile(object? sender, EventArgs e)
    {
        IsMediaLoaded = true;
        PlayPauseSymbol = PackIconMaterialKind.Pause;
    }


    [RelayCommand]
    void OpenFile()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == true)
            Play(openFileDialog.FileName);
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
                PlayPauseSymbol = PackIconMaterialKind.Play;
            }
            else
            {
                PlayPauseSymbol = PackIconMaterialKind.Pause;
            }
        }
    }
}
