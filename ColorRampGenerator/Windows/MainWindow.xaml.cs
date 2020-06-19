using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
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
        
        private void TextBox_KeyEnterUpdate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var tBox = (TextBox)sender;
                var prop = TextBox.TextProperty;

                var binding = BindingOperations.GetBindingExpression(tBox, prop);
                binding?.UpdateSource();
            }
        }
    }
}