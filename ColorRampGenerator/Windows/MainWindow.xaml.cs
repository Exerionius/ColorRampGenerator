using System.Windows;
using ColorRampGenerator.ViewModels;

namespace ColorRampGenerator.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}