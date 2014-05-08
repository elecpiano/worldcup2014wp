using System;
using System.Windows.Data;
using System.Threading;
using System.Globalization;

namespace WorldCup2014WP.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                DateTime dt = (DateTime)value;
                string format = string.Empty;
                if (parameter!=null)
                {
                    format = (string)parameter;
                    //if (format == "TODAY")
                    //{
                    //    if (dt==DateTime.Today)
                    //    {
                    //        return "Today";
                    //    }
                    //    else
                    //    {
                    //        format = "MMMd";
                    //    }
                    //}
                }
                return dt.ToString(format);
            }
            else
            {
                return string.Empty;
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
