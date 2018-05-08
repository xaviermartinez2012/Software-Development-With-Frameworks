using Cecs475.BoardGames.Model;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Cecs475.BoardGames.Chess.WpfView
{
    class ChessSquareBackgroundConverter : IMultiValueConverter {
		private static SolidColorBrush HIGHLIGHT_BRUSH = Brushes.Green;
		private static SolidColorBrush DEFAULT_BRUSH = Brushes.White;
		private static SolidColorBrush CONTRAST_BRUSH = Brushes.BurlyWood;
		private static SolidColorBrush SELECTED_BRUSH = Brushes.Red;
		private static SolidColorBrush IN_CHECK_BRUSH = Brushes.Yellow;

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
			// This converter will receive two properties: the Position of the square, and whether it
			// is being hovered.
			BoardPosition pos = (BoardPosition)values[0];
			bool isHighlighted = (bool)values[1];
            bool isSelected = (bool)values[2];
            bool isInCheck = (bool)values[3];

			// Hovered squares have a specific color.
			if (isHighlighted) {
				return HIGHLIGHT_BRUSH;
			}

            if (isInCheck)
            {
                return IN_CHECK_BRUSH;
            }

            if (isSelected)
            {
                return SELECTED_BRUSH;
            }
            
			// Default colored squares
            if (pos.Row % 2 == 0)
            {
                if (pos.Col % 2 == 0)
                {
                    return DEFAULT_BRUSH;
                } else
                {
                    return CONTRAST_BRUSH;
                }
            } else
            {
                if (pos.Col % 2 != 0)
                {
                    return DEFAULT_BRUSH;
                } else
                {
                    return CONTRAST_BRUSH;
                }
            }
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}