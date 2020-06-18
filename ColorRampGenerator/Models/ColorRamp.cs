using System.Collections.ObjectModel;
using System.ComponentModel;
using ColorRampGenerator.Prism;

namespace ColorRampGenerator.Models
{
    public class ColorRamp: BindableBase
    {
        public HsbColor BaseColor => Colors[Colors.Count / 2];

        public int Size
        {
            get => Colors.Count;
            set
            {
                if(Colors.Count == value) return;
                
                var baseColor = BaseColor;
                Init(baseColor, value);
                ApplyShifts();
                
                RaisePropertyChanged(nameof(Size));
                RaisePropertyChanged(nameof(Colors));
                RaisePropertyChanged(nameof(HueShifts));
                RaisePropertyChanged(nameof(SaturationShifts));
            }
        }

        public ObservableCollection<HsbColor> Colors { get; }
        public ObservableCollection<Shift> HueShifts { get; }
        public ObservableCollection<Shift> SaturationShifts { get; }

        public ColorRamp(HsbColor baseColor, int size)
        {
            Colors = new ObservableCollection<HsbColor>();
            HueShifts = new ObservableCollection<Shift>();
            SaturationShifts = new ObservableCollection<Shift>();
            
            Init(baseColor, size);
            ApplyShifts();
            
            BaseColor.PropertyChanged += (o, e)
                => ApplyShifts();
        }

        private void Init(HsbColor baseColor, int size)
        {
            for(var i = 0; i < HueShifts.Count; i++)
            {
                HueShifts[i].PropertyChanged -= OnShiftPropertyChange;
                SaturationShifts[i].PropertyChanged -= OnShiftPropertyChange;
            }
            
            Colors.Clear();
            HueShifts.Clear();
            SaturationShifts.Clear();
            
            var middle = size / 2;
            for (var i = 0; i < size; i++)
            {
                if (i < middle)
                {
                    Colors.Add(baseColor.Clone());
                    HueShifts.Add(-15);
                    SaturationShifts.Add(-15);
                }
                else if (i == middle)
                {
                    Colors.Add(baseColor);
                    HueShifts.Add(0);
                    SaturationShifts.Add(0);
                }
                else
                {
                    Colors.Add(baseColor.Clone());
                    HueShifts.Add(15);
                    SaturationShifts.Add(-15);
                }

                HueShifts[i].PropertyChanged += OnShiftPropertyChange;
                SaturationShifts[i].PropertyChanged += OnShiftPropertyChange;
            }
        }

        private void ApplyShifts()
        {
            var middle = Colors.Count / 2;
            var baseColor = BaseColor;
            
            var totalHueShift = 0;
            var totalSaturationShift = 0;
            for (var i = middle - 1; i >= 0; i--)
            {
                totalHueShift += HueShifts[i];
                totalSaturationShift += SaturationShifts[i];
                Colors[i].Hue = baseColor.Hue + totalHueShift;
                Colors[i].Saturation = baseColor.Saturation + totalSaturationShift;
            }

            totalHueShift = 0;
            totalSaturationShift = 0;
            for (var i = middle + 1; i < Colors.Count; i++)
            {
                totalHueShift += HueShifts[i];
                totalSaturationShift += SaturationShifts[i];
                Colors[i].Hue = baseColor.Hue + totalHueShift;
                Colors[i].Saturation = baseColor.Saturation + totalSaturationShift;
            }
        }

        private void OnShiftPropertyChange(object sender, PropertyChangedEventArgs args)
        {
            ApplyShifts();
        }
    }
}