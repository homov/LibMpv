using Avalonia.Threading;
using DynamicData.Binding;
using LibMpv.Client;
using ReactiveUI;
using System;

namespace Avalonia.LibMpv.Sample;

public class MainWindowModel: ReactiveObject
{
    public MpvContext OpenGLMpvContext { get; }   = new MpvContext();
    public MpvContext SoftwareMpvContext { get; } = new MpvContext();
    
    private MpvContext[] contexts;

    int _selectedContext =-1;
    public int SelectedContext
    {
        get => _selectedContext;
        set 
        {
            this.RaiseAndSetIfChanged(ref _selectedContext, value);
        }
    }

    string _mediaUrl = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";
    public string MediaUrl
    {
        get => _mediaUrl;
        set
        {
            this.RaiseAndSetIfChanged(ref _mediaUrl, value);
        }
    }

    public MainWindowModel()
    {
        contexts = new MpvContext[2]
        {
            this.OpenGLMpvContext,
            this.SoftwareMpvContext
        };
        this.WhenValueChanged(t => t.SelectedContext).Subscribe((_) => this.PauseAll());
    }

    public void Play()
    {
        PauseAll();
        Dispatcher.UIThread.Post(() =>
        {
            if (SelectedContext >= 0 && SelectedContext < contexts.Length)
            {
                contexts[SelectedContext].Command("loadfile", MediaUrl, "replace");
                contexts[SelectedContext].SetPropertyFlag("pause", false);
            }
        });
    }

    public void PauseAll()
    {
        Dispatcher.UIThread.Post(() =>
        {
            foreach (var context in contexts)
            {
                context.SetPropertyFlag("pause", true);
            }
        });

    }

    public void Pause()
    {
        Dispatcher.UIThread.Post(() =>
        {
            if (SelectedContext >= 0 && SelectedContext < contexts.Length)
            {
                contexts[SelectedContext].Command("cycle", "pause");
            }
        });

    }

    public void Stop()
    {
        Dispatcher.UIThread.Post(() =>
        {
            if (SelectedContext >= 0 && SelectedContext < contexts.Length)
            {
                contexts[SelectedContext].Command("stop");
            }
        });
    }
}
