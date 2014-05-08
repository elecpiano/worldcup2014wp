using System;
using System.Globalization;
using System.Windows.Data;

namespace WorldCup2014WP.Converters
{
    public class QuickSelectorItemOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool enabled = (bool)value;

            if (enabled)
            {
                return 1d;
            }
            else
            {
                return 0.3d;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
