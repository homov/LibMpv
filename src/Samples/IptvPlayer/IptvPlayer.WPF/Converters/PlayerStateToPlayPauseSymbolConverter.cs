using LibMpv.MVVM;
using MahApps.Metro.IconPacks;
using System;
using System.Globalization;
using System.Windows.Data;

namespace IptvPlayer.Converters;

public class PlayerStateToPlayPauseSymbolConverter : IValueConverter
{
    public static readonly PlayerStateToPlayPauseSymbolConverter Instance = new();
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is PlayerState state)
        {
            switch (state)
            {
                case PlayerState.Playing:
                    return PackIconMaterialKind.Pause;
                case PlayerState.Paused:
                    return PackIconMaterialKind.Play;
            }
        }
        return PackIconMaterialKind.Play;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
