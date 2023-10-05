using Avalonia.Data;
using Avalonia.OpenGL.Controls;
using Avalonia.OpenGL;
using Avalonia.Threading;
using Avalonia;
using LibMpv.Client;
using System;

namespace LibMpv.Avalonia;

public class VideoView : OpenGlControlBase, IMpvVideoView
{
    public static readonly DirectProperty<VideoView, MpvContext?> MpvContextProperty =
      AvaloniaProperty.RegisterDirect<VideoView, MpvContext?>(
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
            if (mpvContext != null)
                AttachMpvContext(mpvContext);
        }
    }

    protected override void OnOpenGlRender(GlInterface gl, int fbo)
    {
        if (mpvContext != null && getProcAddres != null)
        {
            var scaling = VisualRoot!.RenderScaling;
            var width = Math.Max(1, (int)(Bounds.Width * scaling));
            var height = Math.Max(1, (int)(Bounds.Height * scaling));
            mpvContext.RenderOpenGl(width, height, fbo, 1);
        }
    }

    private void DetachMpvContext(MpvContext context)
    {
        context.StopRendering();
    }

    private void AttachMpvContext(MpvContext context)
    {
        if (getProcAddres != null)
        {
            mpvContext?.ConfigureRenderer(new OpenGlRendererConfiguration() { OpnGlGetProcAddress = getProcAddres, UpdateCallback = this.UpdateVideoView });
        }
    }

    protected override void OnOpenGlDeinit(GlInterface gl)
    {
        if (mpvContext != null)
            DetachMpvContext(mpvContext);
    }

    protected override void OnOpenGlInit(GlInterface gl)
    {
        if (getProcAddres != null) return;

        getProcAddres = gl.GetProcAddress;
        if (mpvContext != null)
        {
            AttachMpvContext(mpvContext);
        }
    }

    private void UpdateVideoView()
    {
        Dispatcher.UIThread.InvokeAsync(this.RequestNextFrameRendering, DispatcherPriority.Background);
    }

    private MpvContext? mpvContext = null;
    private OpenGlGetProcAddressDelegate getProcAddres;
}
