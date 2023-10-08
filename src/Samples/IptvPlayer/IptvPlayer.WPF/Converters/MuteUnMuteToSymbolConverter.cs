using MahApps.Metro.IconPacks;
using System;
using System.Globalization;
using System.Windows.Data;

namespace IptvPlayer.Converters;

public class MuteUnMuteToSymbolConverter : IValueConverter
{
    public static readonly MuteUnMuteToSymbolConverter Instance = new();
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool muted)
        {
            return muted ? PackIconMaterialKind.VolumeMute : PackIconMaterialKind.VolumeHigh;
        }
        return PackIconMaterialKind.VolumeHigh;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
