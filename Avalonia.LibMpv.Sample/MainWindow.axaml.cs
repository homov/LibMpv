using Avalonia.Controls;

namespace Avalonia.LibMpv.Sample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowModel();
            InitializeComponent();
        }
    }
}