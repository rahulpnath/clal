using CommandLineApplicationLauncherViewModel;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CommandLineApplicationLauncherUI
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var config = value as CmdApplicationConfigurationViewModel;
            if (config == null)
                return Visibility.Collapsed;

            var val = parameter == null ? config.IsInEditMode : !config.IsInEditMode;
            return val ? Visibility.Visible : Visibility.Collapsed;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
