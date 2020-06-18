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
                Colors.Clear();
                var middle = value / 2;
                for (var i = 0; i < value; i++)
                {
                    Colors.Add(i == middle ? baseColor : baseColor.Clone());
                }
                
                HueShifts.Clear();
                InitHueShifts(value);
                
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
            for (var i = 0; i < size; i++)
            {
                Colors.Add(baseColor.Clone());
            }
            
            HueShifts = new ObservableCollection<Shift>();
            InitHueShifts(size);
        }

        private void InitHueShifts(int size)
        {
            var middle = size / 2;
            for (var i = 0; i < size; i++)
            {
                if (i < middle)
                {
                    HueShifts.Add(-5);
                }
                else if (i == middle)
                {
                    HueShifts.Add(0);
                }
                else
                {
                    HueShifts.Add(5);
                }
            }
        }
    }
}