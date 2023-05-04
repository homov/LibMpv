using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Avalonia.Android;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Platform;
using LibMpv.Client;
using System;
using System.Runtime.InteropServices;

namespace Avalonia.LibMpv.Android.Sample.Android.Views;

public class NativeVideoView : NativeControlHost
{
    public class MpvSurfaceView : SurfaceView, ISurfaceHolderCallback
    {
        MpvContext? _mpvContext = null;

        private IntPtr _nativeHandle = IntPtr.Zero;

        public IntPtr NativeHandle { get { return _nativeHandle; } }

        public MpvSurfaceView(Context context):base(context)
        {
            Holder.AddCallback(this);
        }

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
            if (_mpvContext!=null)
            {
                _mpvContext.SetPropertyString("android-surface-size",$"{width}x{height}");
            }
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            _nativeHandle = holder.Surface.Handle;
            if (_mpvContext != null)
                Attach(_mpvContext);
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            Detach();
            if (_nativeHandle != IntPtr.Zero)
            {
                _nativeHandle = IntPtr.Zero;
            }
        }

        public void Attach(MpvContext mpvContext)
        {
            _mpvContext = mpvContext;
            if (NativeHandle != IntPtr.Zero )
                _mpvContext.StartNativeRendering(NativeHandle);
        }

        public void Detach()
        {
            if (_mpvContext != null)
                _mpvContext.StopRendering();
            _mpvContext = null;
        }

        [DllImport("android")]
        private static extern IntPtr ANativeWindow_fromSurface(IntPtr jni, IntPtr surface);

        [DllImport("android")]
        private static extern void ANativeWindow_release(IntPtr surface);
    }

    private MpvSurfaceView? _mpvSurfaceView = null;
    
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
            if (_mpvSurfaceView != null)
                _mpvSurfaceView?.Attach(_mpvContext);
        }
    }

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        if (parent is AndroidViewControlHandle handle)
        {
            _mpvSurfaceView = new MpvSurfaceView(handle.View.Context);
            if (_mpvContext != null) _mpvSurfaceView.Attach(_mpvContext);
            _mpvSurfaceView.SetZOrderOnTop(true);
        }
        return new AndroidViewControlHandle(_mpvSurfaceView);
    }

    protected override void DestroyNativeControlCore(IPlatformHandle control)
    {
        _mpvSurfaceView?.Detach();
        _mpvSurfaceView = null;
        base.DestroyNativeControlCore(control);
    }
}
