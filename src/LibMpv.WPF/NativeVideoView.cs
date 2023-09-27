using LibMpv.Client;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace LibMpv.WPF;

[TemplatePart(Name = PART_PlayerHost, Type = typeof(VideoHwndHost))]
public class NativeVideoView : ContentControl, IMpvVideoView, IDisposable
{
    private const string PART_PlayerHost = "PART_PlayerHost";
    private VideoHwndHost? _videoHwndHost = null;

    public NativeVideoView()
    {
        DefaultStyleKey = typeof(NativeVideoView);
    }

    public static readonly DependencyProperty MpvContextProperty = DependencyProperty.Register(nameof(MpvContext),
            typeof(MpvContext),
            typeof(NativeVideoView),
            new PropertyMetadata(null, OnMpvContextChanged));

    public MpvContext? MpvContext
    {
        get => GetValue(MpvContextProperty) as MpvContext;
        set => SetValue(MpvContextProperty, value);
    }

    private static void OnMpvContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.OldValue is MpvContext oldMediaPlayer)
        {
            ((NativeVideoView)d).DetachMpvContext(oldMediaPlayer);
        }
        if (e.NewValue is MpvContext newMediaPlayer)
        {
            ((NativeVideoView)d).AttachMpvContext(newMediaPlayer);
        }
    }

    private bool IsDesignMode => (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;
    private ForegroundWindow? ForegroundWindow { get; set; }
    private bool IsUpdatingContent { get; set; }
    private UIElement? ViewContent { get; set; }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (IsDesignMode)
        {
            return;
        }

        if (Template.FindName(PART_PlayerHost, this) is not VideoHwndHost controlHost)
        {
            Trace.WriteLine($"Couldn't find {PART_PlayerHost} of type {nameof(VideoHwndHost)}");
            return;
        }

        _videoHwndHost = controlHost;

        IsUpdatingContent = true;
        try
        {
            Content = null;
        }
        finally
        {
            IsUpdatingContent = false;
        }


        if (this._videoHwndHost != null)
        {
            ForegroundWindow = new ForegroundWindow(_videoHwndHost, ActualWidth, ActualHeight)
            {
                OverlayContent = ViewContent
            };
        }

        if (MpvContext != null)
        {
            AttachMpvContext(MpvContext);
        }
    }

    private void AttachMpvContext(MpvContext mpvContext)
    {
        if (mpvContext != null  && _videoHwndHost != null)
        {
            mpvContext.StopRendering();
            mpvContext.ConfigureRenderer(new NativeRendererConfiguration() { WindowHandle = _videoHwndHost.Handle });
        }
    }

    private void DetachMpvContext(MpvContext mpvContext)
    {
        if (mpvContext != null )
        {
            mpvContext.StopRendering();
        }
    }

    protected override void OnContentChanged(object oldContent, object newContent)
    {
        base.OnContentChanged(oldContent, newContent);

        if (IsDesignMode || IsUpdatingContent)
        {
            return;
        }

        IsUpdatingContent = true;
        try
        {
            Content = null;
        }
        finally
        {
            IsUpdatingContent = false;
        }

        ViewContent = newContent as UIElement;
        if (ForegroundWindow != null)
        {
            ForegroundWindow.OverlayContent = ViewContent;
        }
    }

    #region IDisposable Support

    bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                if (MpvContext != null)
                {
                    DetachMpvContext(MpvContext);
                }

                _videoHwndHost?.Dispose();
                _videoHwndHost = null;
                ForegroundWindow?.Close();
            }

            ViewContent = null;
            ForegroundWindow = null;
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
    }

    #endregion
}
