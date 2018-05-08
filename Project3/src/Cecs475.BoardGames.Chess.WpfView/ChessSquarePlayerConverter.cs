using Cecs475.BoardGames.Chess.Model;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Cecs475.BoardGames.Chess.WpfView
{

    public class ChessSquarePlayerConverter : IValueConverter {

        public static Uri PAWN_WHITE_URI = new Uri("pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Pawn_White.png");
        public static Uri PAWN_BLACK_URI = new Uri("pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Pawn_Black.png");
        public static Uri ROOK_WHITE_URI = new Uri("pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Rook_White.png");
        public static Uri ROOK_BLACK_URI = new Uri("pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Rook_Black.png");
        public static Uri KNIGHT_WHITE_URI = new Uri("pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Knight_White.png");
        public static Uri KNIGHT_BLACK_URI = new Uri("pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Knight_Black.png");
        public static Uri BISHOP_WHITE_URI = new Uri("pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Bishop_White.png");
        public static Uri BISHOP_BLACK_URI = new Uri("pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Bishop_Black.png");
        public static Uri QUEEN_WHITE_URI = new Uri("pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Queen_White.png");
        public static Uri QUEEN_BLACK_URI = new Uri("pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Queen_Black.png");
        public static Uri KING_WHITE_URI = new Uri("pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/King_White.png");
        public static Uri KING_BLACK_URI = new Uri("pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/King_Black.png");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			ChessPiece chessPiece = (ChessPiece)value;
			if (chessPiece.PieceType == ChessPieceType.Empty) {
				return null;
			}

            Image finalImage = new Image();

            switch (chessPiece.PieceType)
            {
                case ChessPieceType.Pawn:
                    finalImage.Source = chessPiece.Player == 1 ? new BitmapImage(PAWN_WHITE_URI) : new BitmapImage(PAWN_BLACK_URI);
                    break;
                case ChessPieceType.Rook:
                    finalImage.Source = chessPiece.Player == 1 ? new BitmapImage(ROOK_WHITE_URI) : new BitmapImage(ROOK_BLACK_URI);
                    break;
                case ChessPieceType.Bishop:
                    finalImage.Source = chessPiece.Player == 1 ? new BitmapImage(BISHOP_WHITE_URI) : new BitmapImage(BISHOP_BLACK_URI);
                    break;
                case ChessPieceType.Queen:
                    finalImage.Source = chessPiece.Player == 1 ? new BitmapImage(QUEEN_WHITE_URI) : new BitmapImage(QUEEN_BLACK_URI);
                    break;
                case ChessPieceType.Knight:
                    finalImage.Source = chessPiece.Player == 1 ? new BitmapImage(KNIGHT_WHITE_URI) : new BitmapImage(KNIGHT_BLACK_URI);
                    break;
                default:
                    finalImage.Source = chessPiece.Player == 1 ? new BitmapImage(KING_WHITE_URI) : new BitmapImage(KING_BLACK_URI);
                    break;
            }

            return finalImage;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
