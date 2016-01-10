using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
namespace FreestyleOnline___WP.Classes
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <exception cref="ArgumentException">TargetType must be Visibility</exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(!(value is bool))
                throw new ArgumentException("Source must be of type bool");

            if(targetType != typeof(Visibility))
                throw new ArgumentException("TargetType must be Visibility");

            bool v = (bool) value;

            if(parameter is string && parameter.ToString() == "Inverse")
                v = !v;

            if (v)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
