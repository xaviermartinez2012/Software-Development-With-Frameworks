using Cecs475.BoardGames.Chess.Model;
using Cecs475.BoardGames.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cecs475.BoardGames.Chess.WpfView
{
    /// <summary>
    /// Interaction logic for PawnPromotion.xaml
    /// </summary>
    public partial class PawnPromotion : Window
    {
        private ChessViewModel mChessViewModel;
        private BoardPosition mStart;
        private BoardPosition mEnd;

        public PawnPromotion(ChessViewModel vm, BoardPosition start, BoardPosition end)
        {
            mChessViewModel = vm;
            mStart = start;
            mEnd = end;

            String rookUri;
            String knightUri;
            String bishopUri;
            String queenUri;
            if (mChessViewModel.CurrentPlayer == 1)
            {
                rookUri = "pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Rook_White.png";
                knightUri = "pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Knight_White.png";
                bishopUri = "pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Bishop_White.png";
                queenUri = "pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Queen_White.png";
            } else
            {
                rookUri = "pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Rook_Black.png";
                knightUri = "pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Knight_Black.png";
                bishopUri = "pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Bishop_Black.png";
                queenUri = "pack://application:,,,/Cecs475.BoardGames.Chess.WpfView;component/images/Queen_Black.png";
            }
            Image rookImage = new Image
            {
                Source = new BitmapImage(new Uri(rookUri))
            };
            Image knightImage = new Image
            {
                Source = new BitmapImage(new Uri(knightUri))
            };
            Image bishopImage = new Image
            {
                Source = new BitmapImage(new Uri(bishopUri))
            };
            Image queenImage = new Image
            {
                Source = new BitmapImage(new Uri(queenUri))
            };

            InitializeComponent();

            this.Rook.Content = rookImage;
            this.Knight.Content = knightImage;
            this.Bishop.Content = bishopImage;
            this.Queen.Content = queenImage;
        }

        async private void Button_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            String name = b.Name;
            switch (name)
            {
                case "Rook":
                    await mChessViewModel.ApplyMove(new ChessMove(mStart, mEnd, ChessPieceType.Rook));
                    break;
                case "Knight":
                    await mChessViewModel.ApplyMove(new ChessMove(mStart, mEnd, ChessPieceType.Knight));
                    break;
                case "Bishop":
                   await mChessViewModel.ApplyMove(new ChessMove(mStart, mEnd, ChessPieceType.Bishop));
                    break;
                case "Queen":
                    await mChessViewModel.ApplyMove(new ChessMove(mStart, mEnd, ChessPieceType.Queen));
                    break;
            }
            this.Close();
        }
    }
}
