using System;
using System.Globalization;
using System.Windows.Data;

namespace Cecs475.Othello.Application
{

    /// <summary>
    /// Converts the current player into a string represention.
    /// </summary>
    public class OthelloPlayerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int currentPlayer = (int)value;
            string label = "";
            label = currentPlayer == 2 ? "blue" : "yellow";
            return label;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
