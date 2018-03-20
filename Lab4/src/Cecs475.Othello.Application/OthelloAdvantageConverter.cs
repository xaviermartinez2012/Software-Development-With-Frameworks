using System;
using System.Globalization;
using System.Windows.Data;
using Cecs475.Othello.Model;

namespace Cecs475.Othello.Application
{

    /// <summary>
    /// Converts a CurrentAdvantage into a string represention.
    /// </summary>
    public class OthelloAdvantageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GameAdvantage gameAdvantage = (GameAdvantage)value;
            int advantage = gameAdvantage.Advantage;
            int player = gameAdvantage.Player;
            string label = "";
            string formatString = "{0} is winning by {1}";
            if (advantage == 0)
            {
                label = "tie game";
            }
            else if (advantage > 0)
            {
                label = player == 2 ? string.Format(formatString, "blue", advantage) : string.Format(formatString, "yellow", advantage);
            }
            return label;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
