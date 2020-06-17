using System;
using System.Windows.Media;
using ColorRampGenerator.Prism;
using ColorRampGenerator.Tools;

namespace ColorRampGenerator.Models
{
    public class HsbColor: BindableBase
    {
        public int Hue
        {
            get => Convert.ToInt32(_hue);
            set
            {
                _hue = Math.Clamp(value, 0, 360);
                RaisePropertyChanged(nameof(Hue));
                RaisePropertyChanged(nameof(Brush));
            }
        }
        private double _hue;
        
        public int Saturation
        {
            get => Convert.ToInt32(_saturation * 100.0);
            set
            {
                _saturation = Math.Clamp(value / 100.0, 0, 100);
                RaisePropertyChanged(nameof(Saturation));
                RaisePropertyChanged(nameof(Brush));
            }
        }
        private double _saturation;
        
        public int Brightness
        {
            get => Convert.ToInt32(_brightness * 100.0);
            set
            {
                _brightness = Math.Clamp(value / 100.0, 0, 100);
                RaisePropertyChanged(nameof(Brightness));
                RaisePropertyChanged(nameof(Brush));
            }
        }
        private double _brightness;
        
        public SolidColorBrush Brush => new SolidColorBrush(ColorHelper.GetRgb(this));

        public HsbColor(int hue, int saturation, int brightness)
        {
            Hue = hue;
            Saturation = saturation;
            Brightness = brightness;
        }

        public HsbColor Clone()
        {
            return new HsbColor(Hue, Saturation, Brightness);
        }

        public override string ToString()
        {
            return $"({Hue},{Saturation},{Brightness})";
        }
    }
}