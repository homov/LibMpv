﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.Metadata;
using Avalonia.Platform;
using Avalonia.VisualTree;
using LibMpv.Client;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Avalonia.LibMpv.Sample;

public class NativeVideoView : NativeControlHost
{
    public static readonly StyledProperty<object> ContentProperty =
            ContentControl.ContentProperty.AddOwner<NativeVideoView>();

    private IPlatformHandle? _platformHandle = null;
    
    private bool _attached;
    private Window _floatingContent;
    private IDisposable _disposables;
    private IDisposable _isEffectivellyVisibleSub;


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

    static NativeVideoView()
    {
        ContentProperty.Changed.AddClassHandler<NativeVideoView>((s, e) => s.InitializeNativeOverlay());
        IsVisibleProperty.Changed.AddClassHandler<NativeVideoView>((s, e) => s.ShowNativeOverlay(s.IsVisible));
    }


    [Content]
    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
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

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        _attached = true;

        InitializeNativeOverlay();

        _isEffectivellyVisibleSub = this.GetVisualAncestors().OfType<Control>()
                .Select(v => v.GetObservable(IsVisibleProperty))
                .CombineLatest(v => !v.Any(o => !o))
                .DistinctUntilChanged()
                .Subscribe(v => IsVisible = v);
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);

        _isEffectivellyVisibleSub?.Dispose();

        ShowNativeOverlay(false);

        _attached = false;
    }

    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromLogicalTree(e);

        _disposables?.Dispose();
        _disposables = null;
        _floatingContent?.Close();
        _floatingContent = null;
    }

    private void InitializeNativeOverlay()
    {
        if (!this.IsAttachedToVisualTree()) return;
        //if (!((IVisual)this).IsAttachedToVisualTree) return;

        if (_floatingContent == null && Content != null)
        {
            _floatingContent = new Window()
            {
                SystemDecorations = SystemDecorations.None,
                TransparencyLevelHint = WindowTransparencyLevel.Transparent,
                Background = Brushes.Transparent,
                SizeToContent = SizeToContent.WidthAndHeight,
                ShowInTaskbar = false,
            };

            _disposables = new CompositeDisposable()
            {
                _floatingContent.Bind(Window.ContentProperty, this.GetObservable(ContentProperty)),
                this.GetObservable(ContentProperty).Skip(1).Subscribe(_=> UpdateOverlayPosition()),
                this.GetObservable(BoundsProperty).Skip(1).Subscribe(_ => UpdateOverlayPosition()),
                Observable.FromEventPattern(VisualRoot, nameof(Window.PositionChanged))
                .Subscribe(_ => UpdateOverlayPosition())
            };
        }

        ShowNativeOverlay(IsEffectivelyVisible);
    }

    private void ShowNativeOverlay(bool show)
    {
        if (_floatingContent == null || _floatingContent.IsVisible == show)
            return;

        if (show && _attached)
            _floatingContent.Show(VisualRoot as Window);
        else
            _floatingContent.Hide();
    }
    private void UpdateOverlayPosition()
    {
        if (_floatingContent == null) return;

        bool forceSetWidth = false, forceSetHeight = false;

        var topLeft = new Point();

        var child = _floatingContent.Presenter?.Child;

        if (child?.IsArrangeValid == true)
        {
            switch (child.HorizontalAlignment)
            {
                case HorizontalAlignment.Right:
                    topLeft = topLeft.WithX(Bounds.Width - _floatingContent.Bounds.Width);
                    break;

                case HorizontalAlignment.Center:
                    topLeft = topLeft.WithX((Bounds.Width - _floatingContent.Bounds.Width) / 2);
                    break;

                case HorizontalAlignment.Stretch:
                    forceSetWidth = true;
                    break;
            }

            switch (child.VerticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    topLeft = topLeft.WithY(Bounds.Height - _floatingContent.Bounds.Height);
                    break;

                case VerticalAlignment.Center:
                    topLeft = topLeft.WithY((Bounds.Height - _floatingContent.Bounds.Height) / 2);
                    break;

                case VerticalAlignment.Stretch:
                    forceSetHeight = true;
                    break;
            }
        }

        if (forceSetWidth && forceSetHeight)
            _floatingContent.SizeToContent = SizeToContent.Manual;
        else if (forceSetHeight)
            _floatingContent.SizeToContent = SizeToContent.Width;
        else if (forceSetWidth)
            _floatingContent.SizeToContent = SizeToContent.Height;
        else
            _floatingContent.SizeToContent = SizeToContent.Manual;

        _floatingContent.Width = forceSetWidth ? Bounds.Width : double.NaN;
        _floatingContent.Height = forceSetHeight ? Bounds.Height : double.NaN;

        _floatingContent.MaxWidth = Bounds.Width;
        _floatingContent.MaxHeight = Bounds.Height;

        var newPosition = this.PointToScreen(topLeft);

        if (newPosition != _floatingContent.Position)
        {
            _floatingContent.Position = newPosition;
        }
    }
}
