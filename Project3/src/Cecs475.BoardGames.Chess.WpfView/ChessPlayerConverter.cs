using System;
using System.Globalization;
using System.Windows.Data;

namespace Cecs475.BoardGames.Chess.WpfView
{
    public class ChessPlayerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int player = (int)value;
            return player == 1 ? "White" : "Black";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
