using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.LibMpv.Android.Sample.ViewModels;
using Avalonia.LibMpv.Android.Sample.Views;
using LibMpvClient = LibMpv.Client;

namespace Avalonia.LibMpv.Android.Sample;

public partial class App : Application
{
    public override void Initialize()
    {
        //let the system determine where libmpv is
        LibMpvClient.libmpv.RootPath = "";
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }
        base.OnFrameworkInitializationCompleted();
    }
}