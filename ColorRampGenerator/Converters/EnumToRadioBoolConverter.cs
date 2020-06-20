using System;
using System.Globalization;
using System.Windows.Data;

namespace ColorRampGenerator.Converters
{
    public class EnumToRadioBoolConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;
 
            var checkValue = value.ToString();
            var targetValue = parameter.ToString();
            return checkValue.Equals(targetValue, 
                StringComparison.InvariantCultureIgnoreCase);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return null;
 
            var useValue = (bool)value;
            var targetValue = parameter.ToString();
            return useValue
                ? Enum.Parse(targetType, targetValue)
                : null;
        }
    }
}