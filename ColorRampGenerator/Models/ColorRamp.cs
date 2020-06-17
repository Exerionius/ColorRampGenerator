using System.ComponentModel;
using ColorRampGenerator.Prism;
using ColorRampGenerator.Tools;

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
                var baseColor = BaseColor;
                Colors.Clear();
                var middle = value / 2;
                for (var i = 0; i < value; i++)
                {
                    Colors.Add(i == middle ? baseColor : baseColor.Clone());
                }
                RaisePropertyChanged(nameof(Size));
                RaisePropertyChanged(nameof(Colors));
            }
        }

        public TrulyObservableCollection<HsbColor> Colors { get; }

        public ColorRamp(HsbColor baseColor, int size)
        {
            Colors = new TrulyObservableCollection<HsbColor>();
            for (var i = 0; i < size; i++)
            {
                Colors.Add(baseColor.Clone());
            }

            Colors.CollectionChanged += (sender, args) =>
            {
                RaisePropertyChanged(nameof(Size));
                RaisePropertyChanged(nameof(Colors));
            };

            BaseColor.PropertyChanged += BaseColorOnPropertyChanged;
        }

        private void BaseColorOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            RaisePropertyChanged(nameof(BaseColor));
        }
    }
}