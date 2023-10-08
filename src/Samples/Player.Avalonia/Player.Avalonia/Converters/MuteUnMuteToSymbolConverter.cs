using Avalonia.Data.Converters;
using FluentAvalonia.UI.Controls;
using System;
using System.Globalization;

namespace Player.Avalonia.Converters;

public class MuteUnMuteToSymbolConverter : IValueConverter
{
    public static readonly MuteUnMuteToSymbolConverter Instance = new();
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool muted)
        {
            return muted ? Symbol.Mute : Symbol.Volume;
        }
        return Symbol.Volume;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
