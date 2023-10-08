using Avalonia.Data.Converters;
using FluentAvalonia.UI.Controls;
using LibMpv.MVVM;
using System;
using System.Globalization;

namespace Player.Avalonia.Converters;

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
                    return Symbol.PauseFilled;
                case PlayerState.Paused:
                    return Symbol.PlayFilled;
            }
        }
        return Symbol.PlayFilled;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
