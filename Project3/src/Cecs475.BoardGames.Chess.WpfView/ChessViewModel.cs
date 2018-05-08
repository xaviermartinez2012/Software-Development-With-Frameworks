using Cecs475.BoardGames.Chess.Model;
using Cecs475.BoardGames.Model;
using Cecs475.BoardGames.WpfView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.BoardGames.Chess.WpfView
{
    /// <summary>
	/// Represents one square on the Chess board grid.
	/// </summary>
	public class ChessSquare : INotifyPropertyChanged
    {
        /// <summary>
        /// The position of the square.
        /// </summary>
        public BoardPosition Position
        {
            get; set;
        }

        /// <summary>
        /// Whether the square is selected.
        /// </summary>
        private bool mIsSelected;
        public bool IsSelected
        {
            get { return mIsSelected; }
            set
            {
                if (value != mIsSelected)
                {
                    mIsSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }


        /// <summary>
        /// Whether the square should be highlighted because of a user action.
        /// </summary>
        private bool mIsHighlighted;
        public bool IsHighlighted
        {
            get { return mIsHighlighted; }
            set
            {
                if (value != mIsHighlighted)
                {
                    mIsHighlighted = value;
                    OnPropertyChanged(nameof(IsHighlighted));
                }
            }
        }

        /// <summary>
        /// Whether the square should be highlighted because of being a king in check.
        /// </summary>
        private bool mIsInCheck;
        public bool IsInCheck
        {
            get { return mIsInCheck; }
            set
            {
                if (value != mIsInCheck)
                {
                    mIsInCheck = value;
                    OnPropertyChanged(nameof(IsInCheck));
                }
            }
        }

        /// <summary>
        /// The chess piece on the square.
        /// </summary>
        private ChessPiece mChessPiece;
        public ChessPiece ChessPiece
        {
            get { return mChessPiece; }
            set
            {
                mChessPiece = value;
                OnPropertyChanged(nameof(ChessPiece));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    /// <summary>
	/// Represents the game state of a single Chess game.
	/// </summary>
    public class ChessViewModel : INotifyPropertyChanged, IGameViewModel
    {
        public ChessBoard mBoard;
        private ObservableCollection<ChessSquare> mSquares;
        private ChessSquare mCurrentlySelected;
        public event EventHandler GameFinished;

        public ChessViewModel()
        {
            mBoard = new ChessBoard();

            // Initialize the squares objects based on the board's initial state.
            mSquares = new ObservableCollection<ChessSquare>(
                BoardPosition.GetRectangularPositions(8, 8)
                .Select(pos => new ChessSquare()
                {
                    Position = pos,
                    ChessPiece = mBoard.GetPieceAtPosition(pos)
                })
            );

            PossibleMoves = new HashSet<ChessMove>(
                from ChessMove m in mBoard.GetPossibleMoves()
                select m
            );

            mCurrentlySelected = null;

        }

        public bool IsCheck => mBoard.IsCheck;

        public GameAdvantage BoardAdvantage => mBoard.CurrentAdvantage;

        public int CurrentPlayer => mBoard.CurrentPlayer;

        public bool CanUndo => mBoard.MoveHistory.Any();

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
		/// Applies a move for the current player at the given position.
		/// </summary>
		public void ApplyMove(ChessMove move)
        {
            mBoard.ApplyMove(move);

            RebindState();

            if (mBoard.IsFinished)
            {
                GameFinished?.Invoke(this, new EventArgs());
            }
        }

        public ChessSquare FindKingSquareInCheck()
        {
            ChessSquare chessSquare = null;
            foreach (ChessSquare square in mSquares)
            {
                if (square.IsInCheck)
                {
                    chessSquare = square;
                }
            }
            return chessSquare;
        }

        private void RebindState()
        {
            // Rebind the possible moves, now that the board has changed.
            PossibleMoves = new HashSet<ChessMove>(
                from ChessMove m in mBoard.GetPossibleMoves()
                select m
            );

            // Update the collection of squares by examining the new board state.
            var newSquares = BoardPosition.GetRectangularPositions(8, 8);
            int i = 0;
            foreach (var pos in newSquares)
            {
                var piece = mBoard.GetPieceAtPosition(pos);
                mSquares[i].ChessPiece = piece;
                if (piece.PieceType == ChessPieceType.King && mBoard.IsCheck && piece.Player == mBoard.CurrentPlayer)
                {
                    mSquares[i].IsInCheck = true;
                } else
                {
                    mSquares[i].IsInCheck = false;
                }
                mSquares[i].IsSelected = false;
                i++;
            }
            mCurrentlySelected = null;
            OnPropertyChanged(nameof(BoardAdvantage));
            OnPropertyChanged(nameof(CurrentPlayer));
            OnPropertyChanged(nameof(CanUndo));
        }

        /// <summary>
		/// A collection of 64 ChessSquare objects representing the state of the 
		/// game board.
		/// </summary>
		public ObservableCollection<ChessSquare> Squares
        {
            get { return mSquares; }
        }

        /// <summary>
		/// A set of board positions where the current player can move.
		/// </summary>
		public HashSet<ChessMove> PossibleMoves
        {
            get; private set;
        }

        /// <summary>
        /// The chess square currently selected by the user.
        /// </summary>
        public ChessSquare CurrentlySelected
        {
            get { return mCurrentlySelected; }
            set
            {
                mCurrentlySelected = value;
                OnPropertyChanged(nameof(CurrentlySelected));
            }
        }

        public void UndoMove()
        {
            if (CanUndo)
            {
                mBoard.UndoLastMove();
                RebindState();
            }
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
