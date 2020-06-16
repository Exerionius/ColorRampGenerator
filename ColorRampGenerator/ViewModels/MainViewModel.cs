using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using ColorRampGenerator.Models;
using ColorRampGenerator.Prism;
using ColorRampGenerator.Tools;

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
                SelectedColorRampBrush = new SolidColorBrush(ColorHelper.GetRgb(value.BaseColor));
            }
        }
        private ColorRamp _selectedColorRamp;

        public SolidColorBrush SelectedColorRampBrush
        {
            get => _selectedColorRampBrush;
            private set
            {
                _selectedColorRampBrush = value;
                RaisePropertyChanged(nameof(SelectedColorRampBrush));
            }
        }
        private SolidColorBrush _selectedColorRampBrush;
        
        private readonly HsbColor _defaultColor = new HsbColor(120, 60, 50);

        public MainViewModel()
        {
            ColorRamps = new ObservableCollection<ColorRamp>
            {
                new ColorRamp(_defaultColor)
            };
            SelectedColorRamp = ColorRamps[0];
        }

        private void OnBaseColorChange(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case nameof(ColorRamp.BaseColor):
                    SelectedColorRampBrush = new SolidColorBrush(ColorHelper.GetRgb(SelectedColorRamp.BaseColor));
                    break;
            }
        }
    }
}