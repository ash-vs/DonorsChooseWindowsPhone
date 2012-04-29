using System;
using System.Windows.Data;
using System.Windows;

namespace DonorsChoose.WindowsPhone.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                bool booleanValue = (bool)value;
                return booleanValue ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // We won't be using this converter for TwoWay binding, so
            // there's no need to implement this method of the interface
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
