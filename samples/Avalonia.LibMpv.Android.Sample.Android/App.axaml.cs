using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.LibMpv.Android.Sample.ViewModels;
using Avalonia.LibMpv.Android.Sample.Views;
using Avalonia.Markup.Xaml;
using LibMpv.Client;
using LibMpv.Client.Native;
using System;
using LibMpvClient = LibMpv.Client;

namespace Avalonia.LibMpv.Android.Sample;

public partial class App : Application
{
    public override void Initialize()
    {
        //let the system determine where libmpv is
        LibMpvClient.libmpv.RootPath = "";

        //Step on Android - setup JVM for MPV
        InitJVM();
        
        AvaloniaXamlLoader.Load(this);
    }

    delegate int av_jni_set_java_vm_delegate(nint jvm, nint logCtx);
    private void InitJVM()
    {
        libmpv.LibraryVersionMap.Add("libavcodec", 0);
        FunctionResolverBase.LibraryDependenciesMap.Add("libavcodec", new string[]{ });
        
        var functionResolver = FunctionResolverFactory.Create();

        var av_jni_set_java_vm = functionResolver.GetFunctionDelegate<av_jni_set_java_vm_delegate>("libavcodec", "av_jni_set_java_vm");
        Java.Interop.JniEnvironment.References.GetJavaVM(out nint jvmPointer);
        av_jni_set_java_vm(jvmPointer, IntPtr.Zero);
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