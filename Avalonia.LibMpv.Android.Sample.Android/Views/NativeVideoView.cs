using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Platform;
using LibMpv.Client;
using System;
using System.Diagnostics;

namespace Avalonia.LibMpv.Android.Sample.Android.Views;

public class NativeVideoView : NativeControlHost
{

    private IPlatformHandle? _platformHandle = null;
    
    private MpvContext? _mpvContext = null;

    public static readonly DirectProperty<NativeVideoView, MpvContext?> MpvContextProperty =
           AvaloniaProperty.RegisterDirect<NativeVideoView, MpvContext?>(
               nameof(MpvContext),
               o => o.MpvContext,
               (o, v) => o.MpvContext = v,
               defaultBindingMode: BindingMode.TwoWay);


    public MpvContext? MpvContext
    {
        get { return _mpvContext; }
        set
        {
            if (ReferenceEquals(value, _mpvContext)) return;
            _mpvContext?.StopRendering();
            _mpvContext = value;
            if (_platformHandle != null)
                _mpvContext?.StartNativeRendering(_platformHandle.Handle);
        }
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property.Name == "Bounds")
        {
            var size = GetPixelSize();
            var param = $"{size.Width}x{size.Height}";
            _mpvContext?.SetPropertyString("android-surface-size", param);
        }
    }

    private PixelSize GetPixelSize()
    {
        var scaling = VisualRoot!.RenderScaling;
        return new PixelSize(Math.Max(1, (int)(Bounds.Width)), Math.Max(1, (int)(Bounds.Height)));
    }


    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        _platformHandle = base.CreateNativeControlCore(parent);
        _mpvContext?.StartNativeRendering(_platformHandle.Handle);
        return _platformHandle;
    }

    protected override void DestroyNativeControlCore(IPlatformHandle control)
    {
        _mpvContext?.StopRendering();
        base.DestroyNativeControlCore(control);
        if (_platformHandle != null)
        {
            _platformHandle = null;
        }
    }
}
