using ColorRampGenerator.ViewModels;

namespace ColorRampGenerator.Windows
{
    public partial class MainWindow
    {
        public MainWindow(MainViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
            Chart.AxisX[0].LabelFormatter = _ => string.Empty;
            Chart.AxisY[0].LabelFormatter = _ => string.Empty;
        }
    }
}