using Avalonia.Controls;
using FluentAvalonia.UI.Windowing;

namespace SimplePlayer.Avalonia
{
    public partial class MainWindow : AppWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}