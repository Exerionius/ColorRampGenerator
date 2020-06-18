using System;
using System.Globalization;
using System.Windows.Data;

namespace ColorRampGenerator.Converters
{
    public class IntToSignedStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int integer)
            {
                return integer > 0 ? "+" + integer : integer.ToString();
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && int.TryParse(value.ToString(), out var integer))
            {
                return integer;
            }

            return 0;
        }
    }
}