using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace IptvPlayer.Converters;

public class MultiBoolToVisibilityConverter : IMultiValueConverter
{
    public static MultiBoolToVisibilityConverter Instance = new();
    public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null)
        {
            foreach (object b in value)
                if (b is bool visible && visible) return Visibility.Visible;
        }
        return Visibility.Collapsed;
    }

    public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
