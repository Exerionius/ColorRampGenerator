using System;
using ColorRampGenerator.Prism;

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
            }
        }
        private double _brightness;

        public HsbColor(int hue, int saturation, int brightness)
        {
            Hue = hue;
            Saturation = saturation;
            Brightness = brightness;
        }
    }
}