using Avalonia.Threading;
using IptvPlayer.Core.ViewModel;
using System;

namespace IptvPlayer.ViewModels;

public partial class MainViewModel : IptvPlayerBaseViewModel
{
    public MainViewModel()
    {
        this.Volume = 50;
    }

    public override void InvokeInUIThread(Action action)
    {
        Dispatcher.UIThread.Invoke(action);
    }

}
