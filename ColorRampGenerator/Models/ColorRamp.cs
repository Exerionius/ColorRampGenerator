using System.Collections.ObjectModel;
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
            }
        }

        public ObservableCollection<HsbColor> Colors { get; }
        public ObservableCollection<Shift> HueShifts { get; }

        public ColorRamp(HsbColor baseColor, int size)
        {
            Colors = new ObservableCollection<HsbColor>();
            HueShifts = new ObservableCollection<Shift>();
            
            Init(baseColor, size);
            ApplyShifts();
            
            BaseColor.PropertyChanged += (o, e)
                => ApplyShifts();
        }

        private void Init(HsbColor baseColor, int size)
        {
            Colors.Clear();
            HueShifts.Clear();
            
            var middle = size / 2;
            for (var i = 0; i < size; i++)
            {
                if (i < middle)
                {
                    Colors.Add(baseColor.Clone());
                    HueShifts.Add(-15);
                }
                else if (i == middle)
                {
                    Colors.Add(baseColor);
                    HueShifts.Add(0);
                }
                else
                {
                    Colors.Add(baseColor.Clone());
                    HueShifts.Add(15);
                }
            }
        }

        private void ApplyShifts()
        {
            var middle = Colors.Count / 2;
            var baseColor = BaseColor;
            
            var totalHueShift = 0;
            for (var i = middle - 1; i >= 0; i--)
            {
                totalHueShift += HueShifts[i];
                Colors[i].Hue = baseColor.Hue + totalHueShift;
            }

            totalHueShift = 0;
            for (var i = middle + 1; i < Colors.Count; i++)
            {
                totalHueShift += HueShifts[i];
                Colors[i].Hue = baseColor.Hue + totalHueShift;
            }
        }
    }
}