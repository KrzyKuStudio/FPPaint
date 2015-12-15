using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace FPPaint.Converters
{
    public class IntegerToStringOnlyNumbersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
               return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(int))
                throw new InvalidOperationException("Target object must be type of integer");
            (value as string).Replace(" ", "");
            value = Regex.Replace(value as string, "[^0-9]", "");
            int number = 1;
            try
            {
                number = System.Convert.ToInt32(value);
            }
            catch (Exception)
            {
                number = 0;
            }

            return number;
        }
    }
}
