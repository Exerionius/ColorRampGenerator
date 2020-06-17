using System.Collections.ObjectModel;
using System.ComponentModel;
using ColorRampGenerator.Models;
using ColorRampGenerator.Prism;

namespace ColorRampGenerator.ViewModels
{
    public class MainViewModel: BindableBase
    {
        public ObservableCollection<ColorRamp> ColorRamps { get; }

        public ColorRamp SelectedColorRamp
        {
            get => _selectedColorRamp;
            set
            {
                if (_selectedColorRamp != null)
                {
                    _selectedColorRamp.PropertyChanged -= OnBaseColorChange;
                }
                _selectedColorRamp = value;
                _selectedColorRamp.PropertyChanged += OnBaseColorChange;
                RaisePropertyChanged(nameof(SelectedColorRamp));
            }
        }
        private ColorRamp _selectedColorRamp;

        private readonly HsbColor _defaultColor = new HsbColor(120, 60, 50);

        public MainViewModel()
        {
            ColorRamps = new ObservableCollection<ColorRamp>
            {
                new ColorRamp(_defaultColor, 5),
                new ColorRamp(_defaultColor, 3)
            };
            SelectedColorRamp = ColorRamps[0];
        }

        private void OnBaseColorChange(object sender, PropertyChangedEventArgs args)
        {
        }
    }
}