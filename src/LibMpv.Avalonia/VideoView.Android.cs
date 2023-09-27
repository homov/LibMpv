using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Avalonia;
using Avalonia.Android;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Platform;
using LibMpv.Client;
using System;

namespace LibMpv.Avalonia;

public class VideoView : NativeControlHost
{
    internal class MpvSurfaceView : SurfaceView, ISurfaceHolderCallback
    {
        MpvContext? mpvContext = null;

        private IntPtr nativeHandle = IntPtr.Zero;

        public MpvSurfaceView(Context context) : base(context)
        {
            this.SetZOrderOnTop(true);
            Holder.AddCallback(this);
        }

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
            if (mpvContext != null)
            {
                mpvContext.SetPropertyString("android-surface-size", $"{width}x{height}");
            }
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            nativeHandle = holder.Surface.Handle;
            if (mpvContext != null)
                Attach(mpvContext);
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            Detach();
            if (nativeHandle != IntPtr.Zero)
            {
                nativeHandle = IntPtr.Zero;
            }
        }

        public void Attach(MpvContext mpvContext)
        {
            this.mpvContext = mpvContext;
            if (nativeHandle != IntPtr.Zero && mpvContext!=null)
                mpvContext?.ConfigureRenderer(new NativeRendererConfiguration() { WindowHandle = nativeHandle });
        }

        public void Detach()
        {
            if (mpvContext != null)
                mpvContext.StopRendering();
            mpvContext = null;
        }
    }


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
            if (mpvContext != null)
                AttachMpvContext(mpvContext);
        }
    }

    private void DetachMpvContext(MpvContext context)
    {
        if (mpvSurfaceView != null)
        {
            mpvSurfaceView.Detach();
        }
    }

    private void AttachMpvContext(MpvContext context)
    {
        if (platformHandle != null && mpvSurfaceView!=null)
        {
            mpvSurfaceView.Attach(context);
        }
    }

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        var parentContext = (parent as AndroidViewControlHandle)?.View.Context  ?? global::Android.App.Application.Context;
        mpvSurfaceView = new MpvSurfaceView(parentContext);
        if (mpvContext != null) AttachMpvContext(mpvContext);
        return new AndroidViewControlHandle(mpvSurfaceView);

    }

    protected override void DestroyNativeControlCore(IPlatformHandle control)
    {
        if (mpvContext != null)
        {
            DetachMpvContext(mpvContext);
            mpvContext = null;
        }
        platformHandle = null;
    }

    private IPlatformHandle? platformHandle = null;
    private MpvContext? mpvContext = null;
    private MpvSurfaceView? mpvSurfaceView = null;

}

