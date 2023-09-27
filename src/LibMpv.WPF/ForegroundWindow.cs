using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LibMpv.WPF;

internal class ForegroundWindow : Window
{
    Window? _wndhost;
    readonly FrameworkElement _bckgnd;
    readonly Point _zeroPoint = new Point(0, 0);
    private readonly Grid _grid = new Grid();
    private PresentationSource? _presentationSource;

    UIElement? _overlayContent;
    internal UIElement? OverlayContent
    {
        get => _overlayContent;
        set
        {
            _overlayContent = value;
            _grid.Children.Clear();
            if (_overlayContent != null)
            {
                _grid.Children.Add(_overlayContent);
            }
        }
    }

    internal ForegroundWindow(FrameworkElement background, double width, double height)
    {
        Title = "LibMpv.WPF";
        Height = width;
        Width = height;
        WindowStyle = WindowStyle.None;
        Background = Brushes.Transparent;
        ResizeMode = ResizeMode.NoResize;
        BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
        BorderBrush = Brushes.Transparent;

        AllowsTransparency = true;
        ShowInTaskbar = false;
        Content = _grid;

        DataContext = background.DataContext;

        _bckgnd = background;
        _bckgnd.DataContextChanged += Background_DataContextChanged;
        _bckgnd.Loaded += Background_Loaded;
        _bckgnd.Unloaded += Background_Unloaded;
    }

    void Background_DataContextChanged(object? sender, DependencyPropertyChangedEventArgs e)
    {
        DataContext = e.NewValue;
    }

    void Background_Unloaded(object? sender, RoutedEventArgs e)
    {
        _bckgnd.SizeChanged -= Wndhost_SizeChanged;
        _bckgnd.LayoutUpdated -= RefreshOverlayPosition;
        if (_wndhost != null)
        {
            _wndhost.Closing -= Wndhost_Closing;
            _wndhost.LocationChanged -= RefreshOverlayPosition;
        }
        Hide();
    }

    void Background_Loaded(object? sender, RoutedEventArgs e)
    {
        if (_wndhost != null && IsVisible)
            return;

        _wndhost = Window.GetWindow(_bckgnd);
        if ( _wndhost == null)
            return;

        _wndhost.Background = Brushes.Black;
        _wndhost.BorderBrush = Brushes.Transparent;
        _wndhost.BorderThickness = new Thickness(0.0);

        Owner = _wndhost;

        _wndhost.Closing += Wndhost_Closing;
        _wndhost.LocationChanged += RefreshOverlayPosition;
        _bckgnd.LayoutUpdated += RefreshOverlayPosition;
        _bckgnd.SizeChanged += Wndhost_SizeChanged;

        try
        {
            var locationFromScreen = _bckgnd.PointToScreen(_zeroPoint);
            _presentationSource = PresentationSource.FromVisual(_wndhost);
            var targetPoints = _presentationSource.CompositionTarget.TransformFromDevice.Transform(locationFromScreen);
            Left = targetPoints.X;
            Top = targetPoints.Y;
            var size = new Point(_bckgnd.ActualWidth, _bckgnd.ActualHeight);
            Height = size.Y;
            Width = size.X;
            Show();
            _wndhost.Focus();
        }
        catch 
        {
            Hide();
        }
    }

    void RefreshOverlayPosition(object? sender, EventArgs e)
    {
        if (PresentationSource.FromVisual(_bckgnd) == null)
        {
            return;
        }

        try
        {
            var locationFromScreen = _bckgnd.PointToScreen(_zeroPoint);
            if (_presentationSource == null)
            {
                _presentationSource = PresentationSource.FromVisual(_wndhost);
            }
            var targetPoints = _presentationSource.CompositionTarget.TransformFromDevice.Transform(locationFromScreen);
            Left = targetPoints.X;
            Top = targetPoints.Y;
        }
        catch
        {

        }
    }

    void Wndhost_SizeChanged(object? sender, SizeChangedEventArgs e)
    {
        if (_presentationSource == null)
        {
            var _presentationSource = PresentationSource.FromVisual(_wndhost);
            if (_presentationSource == null)
            {
                return;
            }
        }

        var locationFromScreen = _bckgnd.PointToScreen(_zeroPoint);
        var targetPoints = _presentationSource.CompositionTarget.TransformFromDevice.Transform(locationFromScreen);
        Left = targetPoints.X;
        Top = targetPoints.Y;
        var size = new Point(_bckgnd.ActualWidth, _bckgnd.ActualHeight);
        Height = size.Y;
        Width = size.X;
    }

    void Wndhost_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        if (e.Cancel)
        {
            return;
        }

        Close();

        _bckgnd.DataContextChanged -= Background_DataContextChanged;
        _bckgnd.Loaded -= Background_Loaded;
        _bckgnd.Unloaded -= Background_Unloaded;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.Key == Key.System && e.SystemKey == Key.F4)
        {
            _wndhost?.Focus();
        }
    }
}
