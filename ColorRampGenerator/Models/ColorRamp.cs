using ColorRampGenerator.Prism;

namespace ColorRampGenerator.Models
{
    public class ColorRamp: BindableBase
    {
        public HsbColor BaseColor
        {
            get => _baseColor;
            set
            {
                _baseColor = value;
                RaisePropertyChanged(nameof(BaseColor));
            }
        }
        private HsbColor _baseColor;

        public ColorRamp(HsbColor baseColor)
        {
            BaseColor = baseColor;
            BaseColor.PropertyChanged += (o, e)
                => RaisePropertyChanged(nameof(BaseColor));
        }
    }
}