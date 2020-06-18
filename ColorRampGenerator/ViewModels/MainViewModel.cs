using System.Collections.Generic;
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

        private bool CanRemoveAnyColorRamps => ColorRamps.Count > 1;
        
        public DelegateCommand AddColorRampCommand { get; }
        public DelegateCommand<ColorRamp> RemoveColorRampCommand { get; }

        public MainViewModel()
        {
            var huePresets = new List<ShiftsPreset>
            {
                new ShiftsPreset(0, 0, "No Shift"),
                new ShiftsPreset(-5, 5, "Small"),
                new ShiftsPreset(-10, 10, "Medium", true),
                new ShiftsPreset(-20, 20, "Large"),
                new ShiftsPreset(0, 0, "Custom", custom: true)
            };
            var saturationPresets = new List<ShiftsPreset>
            {
                new ShiftsPreset(0, 0, "No Shift"),
                new ShiftsPreset(-10, 10, "Desaturated Shadows"),
                new ShiftsPreset(10, -10, "Desaturated Highlights"),
                new ShiftsPreset(-10, -10, "Desaturated Both", true),
                new ShiftsPreset(0, 0, "Custom", custom: true)
            };
            var brightnessPresets = new List<ShiftsPreset>
            {
                new ShiftsPreset(0, 0, "No Shift"),
                new ShiftsPreset(-5, 5, "Small"),
                new ShiftsPreset(-10, 10, "Medium", true),
                new ShiftsPreset(-20, 20, "Large"),
                new ShiftsPreset(0, 0, "Custom", custom: true)
            };
            ColorRamps = new ObservableCollection<ColorRamp>
            {
                new ColorRamp(_defaultColor.Clone(), 5, huePresets, saturationPresets, brightnessPresets)
            };
            SelectedColorRamp = ColorRamps[0];
            
            AddColorRampCommand = new DelegateCommand(AddColorRamp);
            RemoveColorRampCommand = new DelegateCommand<ColorRamp>(RemoveColorRamp, CanRemoveRamp)
                .ObservesProperty(() => CanRemoveAnyColorRamps);
        }

        private void AddColorRamp()
        {
            ColorRamps.Add(SelectedColorRamp.Clone());
            SelectedColorRamp = ColorRamps[^1];
            RaisePropertyChanged(nameof(CanRemoveAnyColorRamps));
        }

        private void RemoveColorRamp(ColorRamp ramp)
        {
            if (ramp == SelectedColorRamp)
            {
                var index = ColorRamps.IndexOf(ramp);
                var indexToSelect = index == 0 ? index + 1 : index - 1;
                SelectedColorRamp = ColorRamps[indexToSelect];
            }
            ColorRamps.Remove(ramp);
            RaisePropertyChanged(nameof(CanRemoveAnyColorRamps));
        }

        private bool CanRemoveRamp(ColorRamp ramp)
        {
            return CanRemoveAnyColorRamps;
        }
    }
}