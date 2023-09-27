using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Platform;
using LibMpv.Client;

namespace LibMpv.Avalonia;

public class NativeVideoView: NativeControlHost, IMpvVideoView
{
    public static readonly DirectProperty<NativeVideoView, MpvContext?> MpvContextProperty =
        AvaloniaProperty.RegisterDirect<NativeVideoView, MpvContext?>(
               nameof(MpvContext),
               o => o.MpvContext,
               (o, v) => o.MpvContext = v,
               defaultBindingMode: BindingMode.TwoWay);

    public MpvContext? MpvContext
    {
        get { return mpvContext; }
        set
        {
            if (ReferenceEquals(value, mpvContext)) 
                return;
            if (mpvContext != null)
                DetachMpvContext(mpvContext);
            mpvContext = value;
            if (mpvContext!=null)
                AttachMpvContext(mpvContext);
        }
    }

    private void DetachMpvContext(MpvContext context)
    {
        context.StopRendering();
    }

    private void AttachMpvContext(MpvContext context)
    {
        if (platformHandle != null)
        {
            mpvContext?.ConfigureRenderer(new NativeRendererConfiguration() { WindowHandle = platformHandle.Handle });
        }
    }

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        platformHandle = base.CreateNativeControlCore(parent);
        if (mpvContext != null)
            AttachMpvContext(mpvContext);
        return platformHandle;
    }

    protected override void DestroyNativeControlCore(IPlatformHandle control)
    {
        if (mpvContext != null)
        {
            DetachMpvContext (mpvContext);
            mpvContext = null;
        }
        platformHandle = null;
    }

    private IPlatformHandle? platformHandle = null;
    private MpvContext? mpvContext = null;
}
