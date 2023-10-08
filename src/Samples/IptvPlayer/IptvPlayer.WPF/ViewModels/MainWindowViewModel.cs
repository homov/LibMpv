using IptvPlayer.Core.ViewModel;
using System;
using System.Windows;

namespace IptvPlayer.ViewModels;

public partial class MainWindowViewModel : IptvPlayerBaseViewModel
{
    public MainWindowViewModel()
    {
        this.Volume = 50;
    }

    public override void InvokeInUIThread(Action action)
    {
        Application.Current?.Dispatcher.Invoke(action);
    }
}
