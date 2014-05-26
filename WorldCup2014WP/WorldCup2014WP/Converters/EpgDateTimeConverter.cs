using System;
using System.Windows.Data;
using System.Threading;
using System.Globalization;

namespace WorldCup2014WP.Converters
{
    public class EpgDateTimeConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                /* worldcup specific */
                if (value is string)
                {
                    string[] dtArr = value.ToString().Split(' ');
                    string[] dateArr = dtArr[0].Split('-');
                    int year = int.Parse(dateArr[0]);
                    int month = int.Parse(dateArr[1]);
                    int day = int.Parse(dateArr[2]);

                    string[] timeArr = dtArr[1].Split(':');
                    int hour = int.Parse(timeArr[0])%24;
                    int minute = int.Parse(timeArr[1]);
                    int second = int.Parse(timeArr[2]);
                    
                    value = new DateTime(year,month,day,hour,minute,second);
                }

                DateTime dt = (DateTime)value;
                string format = string.Empty;
                if (parameter != null)
                {
                    format = (string)parameter;
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
