using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using LibMpv.Client;
using System;

namespace LibMpv.Avalonia;

public class SoftwareVideoView : Control
{
    WriteableBitmap renderTarget;

    private MpvContext? mpvContext = null;

    public static readonly DirectProperty<SoftwareVideoView, MpvContext?> MpvContextProperty =
           AvaloniaProperty.RegisterDirect<SoftwareVideoView, MpvContext?>(
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
            
            mpvContext?.StopRendering();
            mpvContext = value;

            mpvContext?.ConfigureRenderer(new SoftwareRendererConfiguartion() { UpdateCallback = this.UpdateVideoView });
        }
    }

    public SoftwareVideoView()
    {
        ClipToBounds = true;
    }

    public override void Render(DrawingContext context)
    {
        if (VisualRoot == null || mpvContext == null)
            return;

        var bitmapSize = GetPixelSize();

        if (renderTarget == null || renderTarget.PixelSize.Width != bitmapSize.Width || renderTarget.PixelSize.Height != bitmapSize.Height)
            this.renderTarget = new WriteableBitmap(bitmapSize, new Vector(96.0, 96.0), PixelFormat.Rgba8888, AlphaFormat.Premul);

        using (ILockedFramebuffer lockedBitmap = this.renderTarget.Lock())
        {
            mpvContext.RenderBitmap(lockedBitmap.Size.Width, lockedBitmap.Size.Height, lockedBitmap.Address, "rgba");
        }
        context.DrawImage(this.renderTarget, new Rect(0, 0, renderTarget.PixelSize.Width, renderTarget.PixelSize.Height));
    }

    private PixelSize GetPixelSize()
    {
        var scaling = VisualRoot!.RenderScaling;
        return new PixelSize(Math.Max(1,(int)Bounds.Width), Math.Max(1,(int)Bounds.Height));
    }

    private void UpdateVideoView()
    {
        Dispatcher.UIThread.Post(this.InvalidateVisual, DispatcherPriority.Background);
    }
}

