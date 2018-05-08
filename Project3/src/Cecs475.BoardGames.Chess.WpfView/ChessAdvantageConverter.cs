﻿using Cecs475.BoardGames.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Cecs475.BoardGames.Chess.WpfView
{
    public class ChessAdvantageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (GameAdvantage)value;
            if (v.Player == 0)
                return "Tie game";
            if (v.Player == 1)
                return $"White has a +{v.Advantage} advantage";
            return $"Black has a +{v.Advantage} advantage";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
