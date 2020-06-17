using System;
using System.Globalization;
using System.Windows.Data;

namespace ColorRampGenerator.Converters
{
    public class IntToRadioBoolConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int integer)
            {
                if (parameter != null && int.TryParse(parameter.ToString(), out var paramInteger))
                {
                    return integer == paramInteger;
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }
}