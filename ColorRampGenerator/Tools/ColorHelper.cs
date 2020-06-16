using System;
using System.Windows.Media;
using ColorRampGenerator.Models;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace ColorRampGenerator.Tools
{
    public static class ColorHelper
    {
        public static Color GetRgb(HsbColor hsb)
        {
            HsbToRgb(hsb.Hue, hsb.Saturation / 100.0, hsb.Brightness / 100.0, out var r, out var g, out var b);
            return Color.FromArgb(255, (byte)r, (byte)g, (byte)b);
        }

        public static HsbColor GetHsb(Color rgb)
        {
            RgbToHsb(rgb.R, rgb.G, rgb.B, out var hue, out var saturation, out var brightness);
            return new HsbColor(
                Convert.ToInt32(hue),
                Convert.ToInt32(saturation * 100.0),
                Convert.ToInt32(brightness * 100.0));
        }
        
        private static void RgbToHsb(int red, int green, int blue,
            out double hue, out double saturation, out double brightness)
        {
            var doubleR = red / 255.0;
            var doubleG = green / 255.0;
            var doubleB = blue / 255.0;

            var max = doubleR;
            if (max < doubleG) max = doubleG;
            if (max < doubleB) max = doubleB;

            var min = doubleR;
            if (min > doubleG) min = doubleG;
            if (min > doubleB) min = doubleB;

            var diff = max - min;
            brightness = (max + min) / 2;
            if (Math.Abs(diff) < 0.00001)
            {
                saturation = 0;
                hue = 0; // H is really undefined.
            }
            else
            {
                if (brightness <= 0.5) saturation = diff / (max + min);
                else saturation = diff / (2 - max - min);

                var rDist = (max - doubleR) / diff;
                var gDist = (max - doubleG) / diff;
                var bDist = (max - doubleB) / diff;

                if (doubleR == max) hue = bDist - gDist;
                else if (doubleG == max) hue = 2 + rDist - bDist;
                else hue = 4 + gDist - rDist;

                hue *= 60;
                if (hue < 0) hue += 360;
            }
        }

        private static void HsbToRgb(double hue, double saturation, double brightness,
            out int red, out int green, out int blue)
        {
            double p2;
            if (brightness <= 0.5) p2 = brightness * (1 + saturation);
            else p2 = brightness + saturation - brightness * saturation;

            var p1 = 2 * brightness - p2;
            double doubleR, doubleG, doubleB;
            if (saturation == 0)
            {
                doubleR = brightness;
                doubleG = brightness;
                doubleB = brightness;
            }
            else
            {
                doubleR = QqhToRgb(p1, p2, hue + 120);
                doubleG = QqhToRgb(p1, p2, hue);
                doubleB = QqhToRgb(p1, p2, hue - 120);
            }

            red = (int) (doubleR * 255.0);
            green = (int) (doubleG * 255.0);
            blue = (int) (doubleB * 255.0);
        }

        private static double QqhToRgb(double q1, double q2, double hue)
        {
            if (hue > 360) hue -= 360;
            else if (hue < 0) hue += 360;

            if (hue < 60) return q1 + (q2 - q1) * hue / 60;
            if (hue < 180) return q2;
            if (hue < 240) return q1 + (q2 - q1) * (240 - hue) / 60;
            return q1;
        }
    }
}