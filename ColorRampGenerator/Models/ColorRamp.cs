using System.Collections.Generic;
using System.ComponentModel;
using ColorRampGenerator.Prism;

namespace ColorRampGenerator.Models
{
    public class ColorRamp: BindableBase
    {
        public HsbColor BaseColor
        {
            get
            {
                var index = _colors.Count / 2;
                return _colors[index];
            }
        }

        public int Size
        {
            get => _colors.Count;
            set
            {
                var baseColor = BaseColor;
                _colors.Clear();
                var middle = value / 2;
                for (var i = 0; i < value; i++)
                {
                    _colors.Add(i == middle ? baseColor : baseColor.Clone());
                }
                RaisePropertyChanged(nameof(Size));
            }
        }

        private readonly List<HsbColor> _colors;

        public ColorRamp(HsbColor baseColor, int size)
        {
            _colors = new List<HsbColor>();
            for (var i = 0; i < size; i++)
            {
                _colors.Add(baseColor.Clone());
            }

            BaseColor.PropertyChanged += BaseColorOnPropertyChanged;
        }

        private void BaseColorOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            RaisePropertyChanged(nameof(BaseColor));
        }
    }
}