using Cecs475.BoardGames.Chess.Model;
using Cecs475.BoardGames.WpfView;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cecs475.BoardGames.Chess.WpfView
{
    /// <summary>
    /// Interaction logic for ChessView.xaml
    /// </summary>
    public partial class ChessView : UserControl, IWpfGameView
    {
        public ChessView()
        {
            InitializeComponent();
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            var square = b.DataContext as ChessSquare;
            var vm = FindResource("vm") as ChessViewModel;
            var currentlySelected = vm.CurrentlySelected;
            if (currentlySelected != null)
            {
                var possibleMoves = from ChessMove m in GetPossibleMovesByStartPosition(currentlySelected)
                                    select m.EndPosition;
                if (possibleMoves.Contains(square.Position))
                {
                    square.IsHighlighted = true;
                }
            } else
            {
                if (GetPossibleMovesByStartPosition(square).Any())
                {
                    square.IsHighlighted = true;
                }
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            var square = b.DataContext as ChessSquare;
            square.IsHighlighted = false;
        }

        private async void Border_MouseUpAsync(object sender, MouseButtonEventArgs e)
        {
            Border b = sender as Border;
            var square = b.DataContext as ChessSquare;
            var currentlySelected = ChessViewModel.CurrentlySelected;

            if (currentlySelected != null)
            {
                if (currentlySelected.Position.Equals(square.Position))
                {
                    square.IsSelected = false;
                    square.IsHighlighted = true;
                    ChessViewModel.CurrentlySelected = null;
                }
                else
                {
                    var possibleMoves = from ChessMove m in GetPossibleMovesByStartPosition(currentlySelected)
                                        where m.EndPosition.Equals(square.Position)
                                        select m;
                    if (possibleMoves.Any())
                    {
                        ChessMove move = possibleMoves.First();
                        if (move.MoveType == ChessMoveType.PawnPromote)
                        {
                            PawnPromotion window = new PawnPromotion(ChessViewModel, currentlySelected.Position, square.Position)
                            {
                                ResizeMode = ResizeMode.NoResize,
                                WindowStyle = WindowStyle.None
                            };
                            window.ShowDialog();
                        } else
                        {
                            if (ChessViewModel.IsCheck)
                            {
                                ChessViewModel.FindKingSquareInCheck().IsInCheck = false;
                            }
                            var window = Window.GetWindow(this);
                            window.IsEnabled = false;
                            await ChessViewModel.ApplyMove(move);
                            window.IsEnabled = true;
                        }
                        square.IsHighlighted = false;
                    }
                    else
                    {
                        ChessViewModel.CurrentlySelected.IsSelected = false;
                        if (IncomingSelectionIsValidChessPiece(square))
                        {
                            square.IsSelected = true;
                            ChessViewModel.CurrentlySelected = square;
                        }
                        else
                        {
                            ChessViewModel.CurrentlySelected = null;
                        }
                    }
                }

            }
            else
            {
                if (IncomingSelectionIsValidChessPiece(square))
                {
                    square.IsHighlighted = false;
                    square.IsSelected = true;
                    ChessViewModel.CurrentlySelected = square;
                }
            }

        }

        private IEnumerable<ChessMove> GetPossibleMovesByStartPosition(ChessSquare square)
        {
            return from ChessMove m in ChessViewModel.PossibleMoves
                   where m.StartPosition.Equals(square.Position)
                   select m;
        }

        private bool IncomingSelectionIsValidChessPiece(ChessSquare square)
        {
            var possibleStartPositions = GetPossibleMovesByStartPosition(square);
            return possibleStartPositions.Any();
        }

        public ChessViewModel ChessViewModel => FindResource("vm") as ChessViewModel;

        public Control ViewControl => this;

        public IGameViewModel ViewModel => ChessViewModel;
    }
}
