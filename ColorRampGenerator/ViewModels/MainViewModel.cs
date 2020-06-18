using System.Collections.ObjectModel;
using ColorRampGenerator.Models;
using ColorRampGenerator.Prism;

namespace ColorRampGenerator.ViewModels
{
    public class MainViewModel: BindableBase
    {
        private readonly HsbColor _defaultColor = new HsbColor(120, 60, 50);
        
        public ObservableCollection<ColorRamp> ColorRamps { get; }

        public ColorRamp SelectedColorRamp
        {
            get => _selectedColorRamp;
            set
            {
                _selectedColorRamp = value;
                RaisePropertyChanged(nameof(SelectedColorRamp));
            }
        }
        private ColorRamp _selectedColorRamp;

        public MainViewModel()
        {
            ColorRamps = new ObservableCollection<ColorRamp>
            {
                new ColorRamp(_defaultColor, 5),
                new ColorRamp(_defaultColor, 3)
            };
            SelectedColorRamp = ColorRamps[0];
        }
    }
}