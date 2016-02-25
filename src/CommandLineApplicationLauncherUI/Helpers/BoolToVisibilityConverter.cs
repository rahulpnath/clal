using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CommandLineApplicationLauncherUI
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool valueAsBool = System.Convert.ToBoolean(value);
            valueAsBool = (parameter != null && parameter.ToString() == "!") ? !valueAsBool : valueAsBool;
            return valueAsBool ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
