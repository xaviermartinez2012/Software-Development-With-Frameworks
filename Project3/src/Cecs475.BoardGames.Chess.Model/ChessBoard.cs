using System;
using System.Collections.Generic;
using System.Text;
using Cecs475.BoardGames.Model;
using System.Linq;

namespace Cecs475.BoardGames.Chess.Model {
	/// <summary>
	/// Represents the board state of a game of chess. Tracks which squares of the 8x8 board are occupied
	/// by which player's pieces.
	/// </summary>
	public class ChessBoard : IGameBoard {
		#region Member fields.
		// The history of moves applied to the board.
		private List<ChessMove> mMoveHistory = new List<ChessMove>();

		public const int BoardSize = 8;

		// Bitboards for each player's pieces, using the ulong type.
		private ulong WhitePawns { get; set; }

		private ulong BlackPawns { get; set; }

		private ulong WhiteKnights { get; set; }

		private ulong BlackKnights { get; set; }

		private ulong WhiteBishops { get; set; }

		private ulong BlackBishops { get; set; }

		private ulong WhiteRooks { get; set; }

		private ulong BlackRooks { get; set; }

		private ulong WhiteQueen { get; set; }

		private ulong BlackQueen { get; set; }

		private ulong WhiteKing { get; set; }

		private ulong BlackKing { get; set; }

		private GameAdvantage Advantage { get; set; }
		private int Player { get; set; }
		private bool mIsCheck { get; set; }
		private bool mIsCheckMate { get; set; }
		private bool mIsStalemate { get; set; }
		private bool mIsFinished { get; set; }
		private List<int> mDrawCounterValues { get; set; }
		#endregion

		#region Properties.
		public bool IsFinished { get { return mIsFinished; } }

		public int CurrentPlayer { get { return Player; } }

		public GameAdvantage CurrentAdvantage { get { return Advantage; } }

		public IReadOnlyList<ChessMove> MoveHistory => mMoveHistory;

		public bool IsCheck {
			get {
					if (IsCheckmate)
					{
						return false;
					} else
					{
						return mIsCheck;
					}
				}
		}

		public bool IsCheckmate {
			get {
					if (mIsCheckMate)
					{
						return mIsCheckMate;
					} else if (mIsCheck)
					{
						return !GetPossibleMoves().Any();
					} else
					{
						return false;
					}
				}
		}

		public bool IsStalemate {
			get {
					if (mIsStalemate)
					{
						return mIsStalemate;
					} else {
						GetPossibleMoves();
						return mIsStalemate;
					}
				}
		}

		public bool IsDraw {
			get { return mDrawCounterValues.Last() >= 50; }
		}
		
		/// <summary>
		/// Tracks the current draw counter, which goes up by 1 for each non-capturing, non-pawn move, and resets to 0
		/// for other moves. If the counter reaches 100 (50 full turns), the game is a draw.
		/// </summary>
		public int DrawCounter {
			get { return mDrawCounterValues.Last(); }
		}
		#endregion


		#region Public methods.
		public IEnumerable<ChessMove> GetPossibleMoves() {

			var possibleMoves = new List<ChessMove>();
			bool wasInCheck = mIsCheck;
			int player = Player;
			var validPawnAttackMoves = GetAllValidPawnAttackMoves(player);
			var validPawnMoves = GetAllValidPawnMoves(player);
			var validRookAttackMoves = GetAllValidRookAttackMoves(player);
			var validKnightAttackMoves = GetAllValidKnightAttackMoves(player);
			var validBishopAttackMoves = GetAllValidBishopAttackMoves(player);
			var validQueenAttackMoves = GetAllValidQueenAttackMoves(player);
			var validKingAttackMoves = GetAllValidKingAttackMoves(player);
			var kingPosition = GetPositionsOfPiece(ChessPieceType.King, player);
			var validKingCastleMoves = GetValidKingCastleMoves(kingPosition.First(), player);
			foreach (var move in validPawnAttackMoves)
			{
				ApplyMove(move);
				if (!wasInCheck && PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					continue;
				}
				if (wasInCheck && PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					continue;
				}
				if (wasInCheck && !PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					possibleMoves.Add(move);
					continue;
				}
				if (!wasInCheck && !PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					possibleMoves.Add(move);
					continue;
				}
			}
			foreach (var move in validPawnMoves)
			{
				ApplyMove(move);
				if (!wasInCheck && PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					continue;
				}
				if (wasInCheck && PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					continue;
				}
				if (wasInCheck && !PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					possibleMoves.Add(move);
					continue;
				}
				if (!wasInCheck && !PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					possibleMoves.Add(move);
					continue;
				}
			}
			foreach (var move in validRookAttackMoves)
			{
				ApplyMove(move);
				if (!wasInCheck && PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					continue;
				}
				if (wasInCheck && PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					continue;
				}
				if (wasInCheck && !PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					possibleMoves.Add(move);
					continue;
				}
				if (!wasInCheck && !PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					possibleMoves.Add(move);
					continue;
				}
			}
			foreach (var move in validKnightAttackMoves)
			{
				ApplyMove(move);
				if (!wasInCheck && PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					continue;
				}
				if (wasInCheck && PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					continue;
				}
				if (wasInCheck && !PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					possibleMoves.Add(move);
					continue;
				}
				if (!wasInCheck && !PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					possibleMoves.Add(move);
					continue;
				}
			}
			foreach (var move in validBishopAttackMoves)
			{
				ApplyMove(move);
				if (!wasInCheck && PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					continue;
				}
				if (wasInCheck && PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					continue;
				}
				if (wasInCheck && !PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					possibleMoves.Add(move);
					continue;
				}
				if (!wasInCheck && !PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					possibleMoves.Add(move);
					continue;
				}
			}
			foreach (var move in validQueenAttackMoves)
			{
				ApplyMove(move);
				if (!wasInCheck && PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					continue;
				}
				if (wasInCheck && PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					continue;
				}
				if (wasInCheck && !PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					possibleMoves.Add(move);
					continue;
				}
				if (!wasInCheck && !PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					possibleMoves.Add(move);
					continue;
				}
			}
			foreach (var move in validKingCastleMoves)
			{
				ApplyMove(move);
				kingPosition = GetPositionsOfPiece(ChessPieceType.King, player);
				if (!wasInCheck && PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					continue;
				}
				if (wasInCheck && PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					continue;
				}
				if (wasInCheck && !PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					possibleMoves.Add(move);
					continue;
				}
				if (!wasInCheck && !PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					possibleMoves.Add(move);
					continue;
				}
			}
			foreach (var move in validKingAttackMoves)
			{
				ApplyMove(move);
				kingPosition = GetPositionsOfPiece(ChessPieceType.King, player);
				if (!wasInCheck && PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					continue;
				}
				if (wasInCheck && PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					continue;
				}
				if (wasInCheck && !PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					possibleMoves.Add(move);
					continue;
				}
				if (!wasInCheck && !PositionIsThreatened(kingPosition.First(), (player % 2)+1))
				{
					UndoLastMove();
					possibleMoves.Add(move);
					continue;
				}
			}
			if (wasInCheck && possibleMoves.Count() == 0)
			{
				mIsCheck = false;
				mIsCheckMate = true;
				mIsFinished = true;
			} else if (!wasInCheck && possibleMoves.Count() == 0)
			{
				mIsStalemate = true;
				mIsFinished = true;
			}
			return possibleMoves;
		}

		public void ApplyMove(ChessMove m) {
			BoardPosition start = m.StartPosition;
			BoardPosition end = m.EndPosition;
			ChessPiece piece = GetPieceAtPosition(start);
			m.PieceType = piece.PieceType;
			m.Player = piece.Player;
			ChessPiece toReplace = GetPieceAtPosition(end);
			ChessMoveType moveType = m.MoveType;
			int player = m.Player;
			int nextPlayer = (player % 2) + 1;
			var enemyKing = GetPositionsOfPiece(ChessPieceType.King, nextPlayer);
			if (toReplace.PieceType != ChessPieceType.Empty && moveType != ChessMoveType.CastleKingSide && moveType != ChessMoveType.CastleQueenSide)
			{
				m.IsCapture = true;
				m.CapturedPiece = toReplace;
			} else
			{
				if (moveType == ChessMoveType.EnPassant)
				{
					m.IsCapture = true;
					m.CapturedPiece = new ChessPiece(ChessPieceType.Pawn, nextPlayer);
				}
				m.IsCapture = false;
			}
			if (moveType == ChessMoveType.Normal)
			{
				SetPieceAtPosition(start, new ChessPiece(ChessPieceType.Empty, 0));
				SetPieceAtPosition(end, piece);
				if (piece.PieceType == ChessPieceType.Pawn || m.IsCapture)
				{
					mDrawCounterValues.Add(0);
				} else
				{
					mDrawCounterValues.Add(mDrawCounterValues.Last() + 1);
					if (IsDraw)
					{
						mIsFinished = true;
					}
				}
			} else if (moveType == ChessMoveType.EnPassant)
			{
				SetPieceAtPosition(start, new ChessPiece(ChessPieceType.Empty, 0));
				SetPieceAtPosition(end, piece);
				if (player == 1)
				{
					SetPieceAtPosition(end.Translate(1,0), new ChessPiece(ChessPieceType.Empty, 0));
				} else
				{
					SetPieceAtPosition(end.Translate(-1,0), new ChessPiece(ChessPieceType.Empty, 0));
				}
				mDrawCounterValues.Add(0);
			} else if (moveType == ChessMoveType.PawnPromote)
			{
				SetPieceAtPosition(start, new ChessPiece(ChessPieceType.Empty, 0));
				SetPieceAtPosition(end, new ChessPiece(m.Promotion, player));
				mDrawCounterValues.Add(0);
			} else if (moveType == ChessMoveType.CastleKingSide)
			{
				SetPieceAtPosition(start.Translate(0, 1), new ChessPiece(ChessPieceType.Rook, player));
				SetPieceAtPosition(start, new ChessPiece(ChessPieceType.Empty, 0));
				SetPieceAtPosition(end, piece);
				SetPieceAtPosition(end.Translate(0, 1), new ChessPiece(ChessPieceType.Empty, 0));
				mDrawCounterValues.Add(mDrawCounterValues.Last() + 1);
				if (IsDraw)
				{
					mIsFinished = true;
				}
			} else if (moveType == ChessMoveType.CastleQueenSide)
			{
				SetPieceAtPosition(start.Translate(0, -1), new ChessPiece(ChessPieceType.Rook, player));
				SetPieceAtPosition(start, new ChessPiece(ChessPieceType.Empty, 0));
				SetPieceAtPosition(end, piece);
				SetPieceAtPosition(end.Translate(0, -2), new ChessPiece(ChessPieceType.Empty, 0));
				mDrawCounterValues.Add(mDrawCounterValues.Last() + 1);
				if (IsDraw)
				{
					mIsFinished = true;
				}
			}
			if (PositionIsThreatened(enemyKing.First(), player))
			{
				mIsCheck = true;
			} else
			{
				mIsCheck = false;
			}
			Advantage = CalculateCurrentAdvantage();
			mMoveHistory.Add(m);
			Player = nextPlayer;
		}

		public void UndoLastMove() {
			ChessMove lastMove = mMoveHistory.Last();
			BoardPosition end = lastMove.EndPosition;
			BoardPosition start = lastMove.StartPosition;
			int player = lastMove.Player;
			ChessMoveType moveType = lastMove.MoveType;
			ChessPieceType pieceType = lastMove.PieceType;
			mIsFinished = false;
			mIsCheckMate = false;
			mIsStalemate = false;
			if (moveType == ChessMoveType.Normal)
			{
				if (lastMove.IsCapture)
				{
					SetPieceAtPosition(end, lastMove.CapturedPiece);
					SetPieceAtPosition(start, new ChessPiece(pieceType, player));
				} else {
					SetPieceAtPosition(end, new ChessPiece(ChessPieceType.Empty, 0));
					SetPieceAtPosition(start, new ChessPiece(pieceType, player));
				}
			} else if (moveType == ChessMoveType.EnPassant)
			{
				SetPieceAtPosition(end, new ChessPiece(ChessPieceType.Empty, 0));
				if (player == 1)
				{
					SetPieceAtPosition(end.Translate(1,0), new ChessPiece(ChessPieceType.Pawn, (player % 2) + 1));
				} else
				{
					SetPieceAtPosition(end.Translate(-1,0), new ChessPiece(ChessPieceType.Pawn, (player % 2) + 1));
				}
				SetPieceAtPosition(start, new ChessPiece(pieceType, player));
			} else if (moveType == ChessMoveType.PawnPromote)
			{
				if (lastMove.IsCapture)
				{
					SetPieceAtPosition(end, lastMove.CapturedPiece);
					SetPieceAtPosition(start, new ChessPiece(pieceType, player));
				} else {
					SetPieceAtPosition(end, new ChessPiece(ChessPieceType.Empty, 0));
					SetPieceAtPosition(start, new ChessPiece(pieceType, player));
				}
			} else if (moveType == ChessMoveType.CastleKingSide)
			{
				SetPieceAtPosition(end.Translate(0, 1), new ChessPiece(ChessPieceType.Rook, player));
				SetPieceAtPosition(end, new ChessPiece(ChessPieceType.Empty, 0));
				SetPieceAtPosition(end.Translate(0, -1), new ChessPiece(ChessPieceType.Empty, 0));
				SetPieceAtPosition(start, new ChessPiece(pieceType, player));
			} else if (moveType == ChessMoveType.CastleQueenSide)
			{
				SetPieceAtPosition(end.Translate(0, -2), new ChessPiece(ChessPieceType.Rook, player));
				SetPieceAtPosition(end, new ChessPiece(ChessPieceType.Empty, 0));
				SetPieceAtPosition(end.Translate(0, 1), new ChessPiece(ChessPieceType.Empty, 0));
				SetPieceAtPosition(start, new ChessPiece(pieceType, player));
			}
			Advantage = CalculateCurrentAdvantage();
			BoardPosition kingPosition = GetPositionsOfPiece(ChessPieceType.King, player).First();
			if (PositionIsThreatened(kingPosition, (player % 2) + 1))
			{
				mIsCheck = true;
			} else
			{
				mIsCheck = false;
			}
			mDrawCounterValues.RemoveAt(mDrawCounterValues.Count() - 1);
			mMoveHistory.RemoveAt(mMoveHistory.Count() - 1);
			Player = (Player % 2) + 1;
		}
		
		/// <summary>
		/// Returns whatever chess piece is occupying the given position.
		/// </summary>
		public ChessPiece GetPieceAtPosition(BoardPosition position) {
			int row = position.Row;
			int col = position.Col;
			ulong targetPosition = TransformRowColToBitboard(row, col);
			if ((targetPosition & WhitePawns) == targetPosition)
			{
				return new ChessPiece(ChessPieceType.Pawn, 1);
			} else if ((targetPosition & WhiteRooks) == targetPosition)
			{
				return new ChessPiece(ChessPieceType.Rook, 1);
			} else if ((targetPosition & WhiteBishops) == targetPosition)
			{
				return new ChessPiece(ChessPieceType.Bishop, 1);
			} else if ((targetPosition & WhiteKnights) == targetPosition)
			{
				return new ChessPiece(ChessPieceType.Knight, 1);
			} else if ((targetPosition & WhiteQueen) == targetPosition)
			{
				return new ChessPiece(ChessPieceType.Queen, 1);
			} else if ((targetPosition & WhiteKing) == targetPosition)
			{
				return new ChessPiece(ChessPieceType.King, 1);
			} else if ((targetPosition & BlackPawns) == targetPosition)
			{
				return new ChessPiece(ChessPieceType.Pawn, 2);
			} else if ((targetPosition & BlackRooks) == targetPosition)
			{
				return new ChessPiece(ChessPieceType.Rook, 2);
			} else if ((targetPosition & BlackBishops) == targetPosition)
			{
				return new ChessPiece(ChessPieceType.Bishop, 2);
			} else if ((targetPosition & BlackKnights) == targetPosition)
			{
				return new ChessPiece(ChessPieceType.Knight, 2);
			} else if ((targetPosition & BlackQueen) == targetPosition)
			{
				return new ChessPiece(ChessPieceType.Queen, 2);
			} else if ((targetPosition & BlackKing) == targetPosition)
			{
				return new ChessPiece(ChessPieceType.King, 2);
			} else {
				return new ChessPiece(ChessPieceType.Empty, 0);
			}
		}

		/// <summary>
		/// Returns whatever player is occupying the given position.
		/// </summary>
		public int GetPlayerAtPosition(BoardPosition pos) {
			ChessPiece piece = GetPieceAtPosition(pos);
			return piece.Player;
		}

		/// <summary>
		/// Returns true if the given position on the board is empty.
		/// </summary>
		/// <remarks>returns false if the position is not in bounds</remarks>
		public bool PositionIsEmpty(BoardPosition pos) {
			if (PositionInBounds(pos))
			{
				ChessPiece piece = GetPieceAtPosition(pos);
				return piece.PieceType == ChessPieceType.Empty ? true : false;
			} else
			{
				return false;
			}
		}

		/// <summary>
		/// Returns true if the given position contains a piece that is the enemy of the given player.
		/// </summary>
		/// <remarks>returns false if the position is not in bounds</remarks>
		public bool PositionIsEnemy(BoardPosition pos, int player) {
			if (PositionInBounds(pos))
			{
				if (PositionIsEmpty(pos))
				{
					return false;
				} else {
					ChessPiece piece = GetPieceAtPosition(pos);
					return piece.Player == player ? false : true;
				}
			} else
			{
				return false;
			}
		}

		/// <summary>
		/// Returns true if the given position is in the bounds of the board.
		/// </summary>
		public static bool PositionInBounds(BoardPosition pos) {
			int row = pos.Row;
			int col = pos.Col;
			if (row < 0 || row > 7)
			{
				return false;
			}
			if (col < 0 || col > 7)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Returns all board positions where the given piece can be found.
		/// </summary>
		public IEnumerable<BoardPosition> GetPositionsOfPiece(ChessPieceType piece, int player) {
			if (piece == ChessPieceType.Pawn)
			{
				if (player == 1)
				{
					ulong bitBoard = WhitePawns;
					return CreatePositionsListFromBitboard(bitBoard);
				} else {
					ulong bitBoard = BlackPawns;
					return CreatePositionsListFromBitboard(bitBoard);
				}
			} else if (piece == ChessPieceType.Rook)
			{
				if (player == 1)
				{
					ulong bitBoard = WhiteRooks;
					return CreatePositionsListFromBitboard(bitBoard);
				} else {
					ulong bitBoard = BlackRooks;
					return CreatePositionsListFromBitboard(bitBoard);
				}
			} else if (piece == ChessPieceType.Knight)
			{
				if (player == 1)
				{
					ulong bitBoard = WhiteKnights;
					return CreatePositionsListFromBitboard(bitBoard);
				} else {
					ulong bitBoard = BlackKnights;
					return CreatePositionsListFromBitboard(bitBoard);
				}
			} else if (piece == ChessPieceType.Bishop)
			{
				if (player == 1)
				{
					ulong bitBoard = WhiteBishops;
					return CreatePositionsListFromBitboard(bitBoard);
				} else {
					ulong bitBoard = BlackBishops;
					return CreatePositionsListFromBitboard(bitBoard);
				}
			} else if (piece == ChessPieceType.Queen)
			{
				if (player == 1)
				{
					ulong bitBoard = WhiteQueen;
					return CreatePositionsListFromBitboard(bitBoard);
				} else {
					ulong bitBoard = BlackQueen;
					return CreatePositionsListFromBitboard(bitBoard);
				}
			} else if (piece == ChessPieceType.King)
			{
				if (player == 1)
				{
					ulong bitBoard = WhiteKing;
					return CreatePositionsListFromBitboard(bitBoard);
				} else {
					ulong bitBoard = BlackKing;
					return CreatePositionsListFromBitboard(bitBoard);
				}
			} else
			{
				return new List<BoardPosition>();
			}
		}

		/// <summary>
		/// Returns true if the given player's pieces are attacking the given position.
		/// </summary>
		public bool PositionIsThreatened(BoardPosition position, int byPlayer) {
			var attackedPositions = GetAttackedPositions(byPlayer);
			return attackedPositions.Contains(position);
		}

		/// <summary>
		/// Returns a set of all BoardPositions that are attacked by the given player.
		/// </summary>
		public ISet<BoardPosition> GetAttackedPositions(int byPlayer) {
			var attackedPositions = new HashSet<BoardPosition>();
			var validPawnAttackMoves = GetAllValidPawnAttackMoves(byPlayer);
			foreach (var move in validPawnAttackMoves)
			{
				attackedPositions.Add(move.EndPosition);
			}
			var validRookAttackMoves = GetAllValidRookAttackMoves(byPlayer);
			foreach (var move in validRookAttackMoves)
			{
				attackedPositions.Add(move.EndPosition);
			}
			var validKnightAttackMoves = GetAllValidKnightAttackMoves(byPlayer);
			foreach (var move in validKnightAttackMoves)
			{
				attackedPositions.Add(move.EndPosition);
			}
			var validBishopAttackMoves = GetAllValidBishopAttackMoves(byPlayer);
			foreach (var move in validBishopAttackMoves)
			{
				attackedPositions.Add(move.EndPosition);
			}
			var validQueenAttackMoves = GetAllValidQueenAttackMoves(byPlayer);
			foreach (var move in validQueenAttackMoves)
			{
				attackedPositions.Add(move.EndPosition);
			}
			var validKingAttackMoves = GetAllValidKingAttackMoves(byPlayer);
			foreach (var move in validKingAttackMoves)
			{
				attackedPositions.Add(move.EndPosition);
			}
			return attackedPositions;
		}

		#endregion

		#region Private methods.
		private List<ChessMove> GetAllValidPawnMoves(int player) {
			var validPawnMoves = new List<ChessMove>();
			var pawnPositions = GetPositionsOfPiece(ChessPieceType.Pawn, player);
			foreach (BoardPosition pawnPosition in pawnPositions)
			{
				var moves = GetValidPawnMoves(pawnPosition, player);
				validPawnMoves.AddRange(moves);
			}
			return validPawnMoves;
		}

		private List<ChessMove> GetAllValidPawnAttackMoves(int player) {
			var validPawnAttackMoves = new List<ChessMove>();
			var pawnPositions = GetPositionsOfPiece(ChessPieceType.Pawn, player);
			foreach (BoardPosition pawnPosition in pawnPositions)
			{
				var moves = GetValidPawnAttackMoves(pawnPosition, player);
				validPawnAttackMoves.AddRange(moves);
			}
			return validPawnAttackMoves;
		}

		private List<ChessMove> GetAllValidRookAttackMoves(int player) {
			var validRookAttackMoves = new List<ChessMove>();
			var rookPositions = GetPositionsOfPiece(ChessPieceType.Rook, player);
			foreach (BoardPosition rookPosition in rookPositions)
			{
				var moves = GetValidHorizontalVerticalAttackMoves(rookPosition, player, ChessPieceType.Rook);
				validRookAttackMoves.AddRange(moves);
			}
			return validRookAttackMoves;
		}

		private List<ChessMove> GetAllValidKnightAttackMoves(int player) {
			var validKnightAttackMoves = new List<ChessMove>();
			var knightPositions = GetPositionsOfPiece(ChessPieceType.Knight, player);
			foreach (BoardPosition knightPosition in knightPositions)
			{
				var moves = GetValidKnightMoves(knightPosition, player);
				foreach (var move in moves)
				{
					validKnightAttackMoves.Add(move);
				}
			}
			return validKnightAttackMoves;
		}

		private List<ChessMove> GetAllValidBishopAttackMoves(int player) {
			var validBishopAttackMoves = new List<ChessMove>();
			var bishopPositions = GetPositionsOfPiece(ChessPieceType.Bishop, player);
			foreach (BoardPosition bishopPosition in bishopPositions)
			{
				var moves = GetValidDiagonalAttackMoves(bishopPosition, player, ChessPieceType.Bishop);
				validBishopAttackMoves.AddRange(moves);
			}
			return validBishopAttackMoves;
		}

		private List<ChessMove> GetAllValidQueenAttackMoves(int player) {
			var validQueenAttackMoves = new List<ChessMove>();
			var queenPositions = GetPositionsOfPiece(ChessPieceType.Queen, player);
			foreach (BoardPosition queenPosition in queenPositions)
			{
				var horizontalVerticalMoves = GetValidHorizontalVerticalAttackMoves(queenPosition, player, ChessPieceType.Queen);
				var diagonalMoves = GetValidDiagonalAttackMoves(queenPosition, player, ChessPieceType.Queen);
				validQueenAttackMoves.AddRange(horizontalVerticalMoves);
				validQueenAttackMoves.AddRange(diagonalMoves);
			}
			return validQueenAttackMoves;
		}

		private List<ChessMove> GetAllValidKingAttackMoves(int player) {
			var validKingAttackMoves = new List<ChessMove>();
			var kingPositions = GetPositionsOfPiece(ChessPieceType.King, player);
			foreach (BoardPosition kingPosition in kingPositions)
			{
				var moves = GetValidKingAttackMoves(kingPosition, player);
				validKingAttackMoves.AddRange(moves);
			}
			return validKingAttackMoves;
		}

		private List<ChessMove> GetValidKingCastleMoves(BoardPosition kingPosition, int player) {
			var validCastleMoves = new List<ChessMove>();
			int row = kingPosition.Row;
			int col = kingPosition.Col;
			if (player == 1)
			{
				if (row == 7)
				{
					if (col == 4)
					{
						BoardPosition rookLeftPos = new BoardPosition(7, 0);
						ChessPiece rookLeft = GetPieceAtPosition(rookLeftPos);
						BoardPosition rookRightPos = new BoardPosition(7, 7);
						ChessPiece rookRight = GetPieceAtPosition(rookRightPos);
						if (mMoveHistory.Any())
						{
							IEnumerable<ChessMove> kingMoves = mMoveHistory.Where(m => m.StartPosition.Equals(kingPosition) && m.PieceType == ChessPieceType.King && m.Player == player);
							if (!kingMoves.Any())
							{
								if (rookLeft.PieceType == ChessPieceType.Rook && rookLeft.Player == player)
								{
									IEnumerable<ChessMove> rookMoves = mMoveHistory.Where(m => m.StartPosition.Equals(rookLeftPos) && m.PieceType == ChessPieceType.Rook && m.Player == player);
									if (!rookMoves.Any())
									{
										BoardPosition two = new BoardPosition(7, 2);
										BoardPosition three = new BoardPosition(7, 3);
										if (PositionIsEmpty(two) && PositionIsEmpty(three))
										{
											if (!PositionIsThreatened(two, (player % 2) + 1) && !PositionIsThreatened(three, (player % 2) + 1))
											{
												if (!PositionIsThreatened(kingPosition, (player % 2) + 1))
												{
													ChessMove move = new ChessMove(kingPosition, two, ChessMoveType.CastleQueenSide);
													move.PieceType = ChessPieceType.King;
													move.Player = player;
													validCastleMoves.Add(move);
												}
											}
										}
									}
								}
								if (rookRight.PieceType == ChessPieceType.Rook && rookRight.Player == player)
								{
									IEnumerable<ChessMove> rookMoves = mMoveHistory.Where(m => m.StartPosition.Equals(rookRightPos) && m.PieceType == ChessPieceType.Rook && m.Player == player);
									if (!rookMoves.Any())
									{
										BoardPosition one = new BoardPosition(7, 5);
										BoardPosition two = new BoardPosition(7, 6);
										if (PositionIsEmpty(one) && PositionIsEmpty(two))
										{
											if (!PositionIsThreatened(one, (player % 2) + 1) && !PositionIsThreatened(two, (player % 2) + 1))
											{
												if (!PositionIsThreatened(kingPosition, (player % 2) + 1))
												{
													ChessMove move = new ChessMove(kingPosition, two, ChessMoveType.CastleKingSide);
													move.PieceType = ChessPieceType.King;
													move.Player = player;
													validCastleMoves.Add(move);
												}
											}
										}
									}
								}
							}
						} else
						{
							if (rookLeft.PieceType == ChessPieceType.Rook && rookLeft.Player == player)
								{
									BoardPosition two = new BoardPosition(7, 2);
									BoardPosition three = new BoardPosition(7, 3);
									if (PositionIsEmpty(two) && PositionIsEmpty(three))
									{
										if (!PositionIsThreatened(two, (player % 2) + 1) && !PositionIsThreatened(three, (player % 2) + 1))
										{
											if (!PositionIsThreatened(kingPosition, (player % 2) + 1))
											{
												ChessMove move = new ChessMove(kingPosition, two, ChessMoveType.CastleQueenSide);
												move.PieceType = ChessPieceType.King;
												move.Player = player;
												validCastleMoves.Add(move);
											}
										}
									}
								}
							if (rookRight.PieceType == ChessPieceType.Rook && rookRight.Player == player)
							{
								BoardPosition one = new BoardPosition(7, 5);
								BoardPosition two = new BoardPosition(7, 6);
								if (PositionIsEmpty(one) && PositionIsEmpty(two))
								{
									if (!PositionIsThreatened(one, (player % 2) + 1) && !PositionIsThreatened(two, (player % 2) + 1))
									{
										if (!PositionIsThreatened(kingPosition, (player % 2) + 1))
										{
											ChessMove move = new ChessMove(kingPosition, two, ChessMoveType.CastleKingSide);
											move.PieceType = ChessPieceType.King;
											move.Player = player;
											validCastleMoves.Add(move);
										}
									}
								}
							}
						}
					}
				}
			} else
			{
				if (row == 0)
				{
					if (col == 4)
					{
						BoardPosition rookLeftPos = new BoardPosition(0, 0);
						ChessPiece rookLeft = GetPieceAtPosition(rookLeftPos);
						BoardPosition rookRightPos = new BoardPosition(0, 7);
						ChessPiece rookRight = GetPieceAtPosition(rookRightPos);
						if (mMoveHistory.Any())
						{
							IEnumerable<ChessMove> kingMoves = mMoveHistory.Where(m => m.StartPosition.Equals(kingPosition) && m.PieceType == ChessPieceType.King && m.Player == player);
							if (!kingMoves.Any())
							{
								if (rookLeft.PieceType == ChessPieceType.Rook && rookLeft.Player == player)
								{
									IEnumerable<ChessMove> rookMoves = mMoveHistory.Where(m => m.StartPosition.Equals(rookLeftPos) && m.PieceType == ChessPieceType.Rook && m.Player == player);
									if (!rookMoves.Any())
									{
										BoardPosition two = new BoardPosition(0, 2);
										BoardPosition three = new BoardPosition(0, 3);
										if (PositionIsEmpty(two) && PositionIsEmpty(three))
										{
											if (!PositionIsThreatened(two, (player % 2) + 1) && !PositionIsThreatened(three, (player % 2) + 1))
											{
												if (!PositionIsThreatened(kingPosition, (player % 2) + 1))
												{
													ChessMove move = new ChessMove(kingPosition, two, ChessMoveType.CastleQueenSide);
													move.PieceType = ChessPieceType.King;
													move.Player = player;
													validCastleMoves.Add(move);
												}
											}
										}
									}
								}
								if (rookRight.PieceType == ChessPieceType.Rook && rookRight.Player == player)
								{
									IEnumerable<ChessMove> rookMoves = mMoveHistory.Where(m => m.StartPosition.Equals(rookRightPos) && m.PieceType == ChessPieceType.Rook && m.Player == player);
									if (!rookMoves.Any())
									{
										BoardPosition one = new BoardPosition(0, 5);
										BoardPosition two = new BoardPosition(0, 6);
										if (PositionIsEmpty(one) && PositionIsEmpty(two))
										{
											if (!PositionIsThreatened(one, (player % 2) + 1) && !PositionIsThreatened(two, (player % 2) + 1))
											{
												if (!PositionIsThreatened(kingPosition, (player % 2) + 1))
												{
													ChessMove move = new ChessMove(kingPosition, two, ChessMoveType.CastleKingSide);
													move.PieceType = ChessPieceType.King;
													move.Player = player;
													validCastleMoves.Add(move);
												}
											}
										}
									}
								}
							}
						} else
						{
							if (rookLeft.PieceType == ChessPieceType.Rook && rookLeft.Player == player)
								{
									BoardPosition two = new BoardPosition(0, 2);
									BoardPosition three = new BoardPosition(0, 3);
									if (PositionIsEmpty(two) && PositionIsEmpty(three))
									{
										if (!PositionIsThreatened(two, (player % 2) + 1) && !PositionIsThreatened(three, (player % 2) + 1))
										{
											if (!PositionIsThreatened(kingPosition, (player % 2) + 1))
											{
												ChessMove move = new ChessMove(kingPosition, two, ChessMoveType.CastleQueenSide);
												move.PieceType = ChessPieceType.King;
												move.Player = player;
												validCastleMoves.Add(move);
											}
										}
									}
								}
							if (rookRight.PieceType == ChessPieceType.Rook && rookRight.Player == player)
							{
								BoardPosition one = new BoardPosition(0, 5);
								BoardPosition two = new BoardPosition(0, 6);
								if (PositionIsEmpty(one) && PositionIsEmpty(two))
								{
									if (!PositionIsThreatened(one, (player % 2) + 1) && !PositionIsThreatened(two, (player % 2) + 1))
									{
										if (!PositionIsThreatened(kingPosition, (player % 2) + 1))
										{
											ChessMove move = new ChessMove(kingPosition, two, ChessMoveType.CastleKingSide);
											move.PieceType = ChessPieceType.King;
											move.Player = player;
											validCastleMoves.Add(move);
										}
									}
								}
							}
						}
					}
				}
			}
			return validCastleMoves;
		}
		
		/// <summary>
		/// Helper function
		/// </summary>
		private GameAdvantage CalculateCurrentAdvantage() {
			var whitePawns = GetPositionsOfPiece(ChessPieceType.Pawn, 1);
			var whiteRooks = GetPositionsOfPiece(ChessPieceType.Rook, 1);
			var whiteKnights = GetPositionsOfPiece(ChessPieceType.Knight, 1);
			var whiteBishops = GetPositionsOfPiece(ChessPieceType.Bishop, 1);
			var whiteQueens = GetPositionsOfPiece(ChessPieceType.Queen, 1);
			var blackPawns = GetPositionsOfPiece(ChessPieceType.Pawn, 2);
			var blackRooks = GetPositionsOfPiece(ChessPieceType.Rook, 2);
			var blackKnights = GetPositionsOfPiece(ChessPieceType.Knight, 2);
			var blackBishops = GetPositionsOfPiece(ChessPieceType.Bishop, 2);
			var blackQueens = GetPositionsOfPiece(ChessPieceType.Queen, 2);
			int whiteAdvantage = whitePawns.Count() + (whiteRooks.Count() * 5) + (whiteKnights.Count() * 3) + (whiteBishops.Count() * 3) + (whiteQueens.Count() * 9);
			int blackAdvantage = blackPawns.Count() + (blackRooks.Count() * 5) + (blackKnights.Count() * 3) + (blackBishops.Count() * 3) + (blackQueens.Count() * 9);
			if (whiteAdvantage > blackAdvantage)
			{
				return new GameAdvantage(1, whiteAdvantage - blackAdvantage);
			} else if(whiteAdvantage < blackAdvantage)
			{
				return new GameAdvantage(2, blackAdvantage - whiteAdvantage);
			} else
			{
				return new GameAdvantage(0, 0);
			}
		}

		private List<ChessMove> GetPawnAttackMoves(BoardPosition pawnPosition, int player) {
			var attackMoves = new List<ChessMove>();
			int row = pawnPosition.Row;
			int col = pawnPosition.Col;
			if (player == 1) {
				BoardPosition diagonalLeft = pawnPosition.Translate(-1, -1);
				BoardPosition diagonalRight = pawnPosition.Translate(-1, 1);
				ChessMove dL = new ChessMove(pawnPosition, diagonalLeft);
				dL.Player = player;
				dL.PieceType = ChessPieceType.Pawn;
				attackMoves.Add(dL);
				ChessMove dR = new ChessMove(pawnPosition, diagonalRight);
				dR.Player = player;
				dR.PieceType = ChessPieceType.Pawn;
				attackMoves.Add(dR);
				if (row == 3)
				{
					if (col == 0)
					{
						ChessMove dRE = new ChessMove(pawnPosition, diagonalRight, ChessMoveType.EnPassant);
						dRE.Player = player;
						dRE.PieceType = ChessPieceType.Pawn;
						attackMoves.Add(dRE);
					} else if (col == 7)
					{
						ChessMove dLE = new ChessMove(pawnPosition, diagonalLeft, ChessMoveType.EnPassant);
						dLE.Player = player;
						dLE.PieceType = ChessPieceType.Pawn;
						attackMoves.Add(dLE);
					} else
					{
						ChessMove dLE = new ChessMove(pawnPosition, diagonalLeft, ChessMoveType.EnPassant);
						dLE.Player = player;
						dLE.PieceType = ChessPieceType.Pawn;
						ChessMove dRE = new ChessMove(pawnPosition, diagonalRight, ChessMoveType.EnPassant);
						dRE.Player = player;
						dRE.PieceType = ChessPieceType.Pawn;
						attackMoves.Add(dLE);
						attackMoves.Add(dRE);
					}
				}
			} else
			{
				BoardPosition diagonalLeft = pawnPosition.Translate(1, -1);
				BoardPosition diagonalRight = pawnPosition.Translate(1, 1);
				ChessMove dL = new ChessMove(pawnPosition, diagonalLeft);
				dL.Player = player;
				dL.PieceType = ChessPieceType.Pawn;
				attackMoves.Add(dL);
				ChessMove dR = new ChessMove(pawnPosition, diagonalRight);
				dR.Player = player;
				dR.PieceType = ChessPieceType.Pawn;
				attackMoves.Add(dR);
				if (row == 4)
				{
					if (col == 0)
					{
						ChessMove dRE = new ChessMove(pawnPosition, diagonalRight, ChessMoveType.EnPassant);
						dRE.Player = player;
						dRE.PieceType = ChessPieceType.Pawn;
						attackMoves.Add(dRE);
					} else if (col == 7)
					{
						ChessMove dLE = new ChessMove(pawnPosition, diagonalLeft, ChessMoveType.EnPassant);
						dLE.Player = player;
						dLE.PieceType = ChessPieceType.Pawn;
						attackMoves.Add(dLE);
					} else
					{
						ChessMove dLE = new ChessMove(pawnPosition, diagonalLeft, ChessMoveType.EnPassant);
						dLE.Player = player;
						dLE.PieceType = ChessPieceType.Pawn;
						ChessMove dRE = new ChessMove(pawnPosition, diagonalRight, ChessMoveType.EnPassant);
						dRE.Player = player;
						dRE.PieceType = ChessPieceType.Pawn;
						attackMoves.Add(dLE);
						attackMoves.Add(dRE);
					}
				}
			}
			return attackMoves;
		}

		private List<ChessMove> GetValidPawnAttackMoves(BoardPosition pawnPosition, int player) {
			var attackMoves = GetPawnAttackMoves(pawnPosition, player);
			var validAttackMoves = new List<ChessMove>();
			foreach (ChessMove attackMove in attackMoves)
			{
				BoardPosition endPosition = attackMove.EndPosition;
				if (attackMove.MoveType == ChessMoveType.Normal)
				{
					if (PositionIsEnemy(endPosition, player))
					{
						if (player == 1)
						{
							if (endPosition.Row == 0)
							{
								validAttackMoves.Add(new ChessMove(attackMove.StartPosition, endPosition, ChessPieceType.Queen));
								validAttackMoves.Add(new ChessMove(attackMove.StartPosition, endPosition, ChessPieceType.Knight));
								validAttackMoves.Add(new ChessMove(attackMove.StartPosition, endPosition, ChessPieceType.Bishop));
								validAttackMoves.Add(new ChessMove(attackMove.StartPosition, endPosition, ChessPieceType.Rook));
							} else
							{
								validAttackMoves.Add(attackMove);
							}
						} else
						{
							if (endPosition.Row == 7)
							{
								validAttackMoves.Add(new ChessMove(attackMove.StartPosition, endPosition, ChessPieceType.Queen));
								validAttackMoves.Add(new ChessMove(attackMove.StartPosition, endPosition, ChessPieceType.Knight));
								validAttackMoves.Add(new ChessMove(attackMove.StartPosition, endPosition, ChessPieceType.Bishop));
								validAttackMoves.Add(new ChessMove(attackMove.StartPosition, endPosition, ChessPieceType.Rook));
							} else
							{
								validAttackMoves.Add(attackMove);
							}
						}
					}
				} else
				{
					if (mMoveHistory.Any())
					{
						if (player == 1)
						{
							BoardPosition enemyPawnPosition = endPosition.Translate(1, 0);
							ChessPiece enemyPawn = GetPieceAtPosition(enemyPawnPosition);
							if (enemyPawn.PieceType == ChessPieceType.Pawn)
							{
								BoardPosition enemyPawnPrevPosition = enemyPawnPosition.Translate(-2, 0);
								ChessMove lastMove = mMoveHistory.Last();
								if (lastMove.StartPosition.Equals(enemyPawnPrevPosition) && lastMove.EndPosition.Equals(enemyPawnPosition))
								{
									validAttackMoves.Add(attackMove);
								}
							}
						} else {
							BoardPosition enemyPawnPosition = endPosition.Translate(-1, 0);
							ChessPiece enemyPawn = GetPieceAtPosition(enemyPawnPosition);
							if (enemyPawn.PieceType == ChessPieceType.Pawn)
							{
								BoardPosition enemyPawnPrevPosition = enemyPawnPosition.Translate(2, 0);
								ChessMove lastMove = mMoveHistory.Last();
								if (lastMove.StartPosition.Equals(enemyPawnPrevPosition) && lastMove.EndPosition.Equals(enemyPawnPosition))
								{
									validAttackMoves.Add(attackMove);
								}
							}
						}
					}
				}
			}
			return validAttackMoves;
		}

		private List<ChessMove> GetValidPawnMoves(BoardPosition pawnPosition, int player) {
			var validPawnMoves = new List<ChessMove>();
			int row = pawnPosition.Row;
			if (player == 1)
			{
				ChessMove moveForwardTwo = new ChessMove(pawnPosition, pawnPosition.Translate(-2, 0));
				moveForwardTwo.Player = player;
				moveForwardTwo.PieceType = ChessPieceType.Pawn;
				ChessMove moveForwardOne = new ChessMove(pawnPosition, pawnPosition.Translate(-1, 0));
				moveForwardOne.Player = player;
				moveForwardOne.PieceType = ChessPieceType.Pawn;
				if (row == 6)
				{
					if (PositionIsEmpty(moveForwardOne.EndPosition))
					{
						validPawnMoves.Add(moveForwardOne);
						if (PositionIsEmpty(moveForwardTwo.EndPosition))
						{
							validPawnMoves.Add(moveForwardTwo);
						}
					}
				} else
				{
					if (PositionIsEmpty(moveForwardOne.EndPosition))
					{
						if (moveForwardOne.EndPosition.Row == 0)
						{
							ChessMove promoteQueen = new ChessMove(pawnPosition, moveForwardOne.EndPosition, ChessPieceType.Queen);
							promoteQueen.Player = player;
							ChessMove promoteBishop = new ChessMove(pawnPosition, moveForwardOne.EndPosition, ChessPieceType.Bishop);
							promoteBishop.Player = player;
							ChessMove promoteKnight = new ChessMove(pawnPosition, moveForwardOne.EndPosition, ChessPieceType.Knight);
							promoteKnight.Player = player;
							ChessMove promoteRook = new ChessMove(pawnPosition, moveForwardOne.EndPosition, ChessPieceType.Rook);
							promoteRook.Player = player;
							validPawnMoves.Add(promoteQueen);
							validPawnMoves.Add(promoteBishop);
							validPawnMoves.Add(promoteKnight);
							validPawnMoves.Add(promoteRook);
						} else {
							validPawnMoves.Add(moveForwardOne);
						}
					}
				}
			} else {
				ChessMove moveForwardTwo = new ChessMove(pawnPosition, pawnPosition.Translate(2, 0));
				moveForwardTwo.Player = player;
				moveForwardTwo.PieceType = ChessPieceType.Pawn;
				ChessMove moveForwardOne = new ChessMove(pawnPosition, pawnPosition.Translate(1, 0));
				moveForwardOne.Player = player;
				moveForwardOne.PieceType = ChessPieceType.Pawn;
				if (row == 1)
				{
					if (PositionIsEmpty(moveForwardOne.EndPosition))
					{
						validPawnMoves.Add(moveForwardOne);
						if (PositionIsEmpty(moveForwardTwo.EndPosition))
						{
							validPawnMoves.Add(moveForwardTwo);
						}
					}
				} else
				{
					if (PositionIsEmpty(moveForwardOne.EndPosition))
					{
						if (moveForwardOne.EndPosition.Row == 7)
						{
							ChessMove promoteQueen = new ChessMove(pawnPosition, moveForwardOne.EndPosition, ChessPieceType.Queen);
							promoteQueen.Player = player;
							promoteQueen.PieceType = ChessPieceType.Pawn;
							ChessMove promoteBishop = new ChessMove(pawnPosition, moveForwardOne.EndPosition, ChessPieceType.Bishop);
							promoteBishop.Player = player;
							promoteBishop.PieceType = ChessPieceType.Pawn;
							ChessMove promoteKnight = new ChessMove(pawnPosition, moveForwardOne.EndPosition, ChessPieceType.Knight);
							promoteKnight.Player = player;
							promoteKnight.PieceType = ChessPieceType.Pawn;
							ChessMove promoteRook = new ChessMove(pawnPosition, moveForwardOne.EndPosition, ChessPieceType.Rook);
							promoteRook.Player = player;
							promoteRook.PieceType = ChessPieceType.Pawn;
							validPawnMoves.Add(promoteQueen);
							validPawnMoves.Add(promoteBishop);
							validPawnMoves.Add(promoteKnight);
							validPawnMoves.Add(promoteRook);
						} else {
							validPawnMoves.Add(moveForwardOne);
						}
					}
				}
			}
			return validPawnMoves;
		}
		
		private List<ChessMove> GetValidHorizontalVerticalAttackMoves(BoardPosition pos, int player, ChessPieceType pieceType) {
			var attackMoves = new List<ChessMove>();
			int row = pos.Row;
			int col = pos.Col;
			int virtualForwardMovement = row;
			int virtualBackwardMovement = 7 - row;
			int virtualLeftwardMovement = col;
			int virtualRightwardMovement = 7 - col;
			for (int i = 1; i < virtualForwardMovement + 1; i++)
			{
				BoardPosition check = pos.Translate(-i, 0);
				if (PositionIsEmpty(check))
				{
					ChessMove move = new ChessMove(pos, check);
					move.Player = player;
					move.PieceType = pieceType;
					attackMoves.Add(move);
					continue;
				} else if (PositionIsEnemy(check, player))
				{
					ChessMove move = new ChessMove(pos, check);
					move.Player = player;
					move.PieceType = pieceType;
					attackMoves.Add(move);
					break;
				} else
				{
					break;
				}
			}
			for (int i = 1; i < virtualBackwardMovement + 1; i++)
			{
				BoardPosition check = pos.Translate(i, 0);
				if (PositionIsEmpty(check))
				{
					ChessMove move = new ChessMove(pos, check);
					move.Player = player;
					move.PieceType = pieceType;
					attackMoves.Add(move);
					continue;
				} else if (PositionIsEnemy(check, player))
				{
					ChessMove move = new ChessMove(pos, check);
					move.Player = player;
					move.PieceType = pieceType;
					attackMoves.Add(move);
					break;
				} else
				{
					break;
				}
			}
			for (int i = 1; i < virtualLeftwardMovement + 1; i++)
			{
				BoardPosition check = pos.Translate(0, -i);
				if (PositionIsEmpty(check))
				{
					ChessMove move = new ChessMove(pos, check);
					move.Player = player;
					move.PieceType = pieceType;
					attackMoves.Add(move);
					continue;
				} else if (PositionIsEnemy(check, player))
				{
					ChessMove move = new ChessMove(pos, check);
					move.Player = player;
					move.PieceType = pieceType;
					attackMoves.Add(move);
					break;
				} else
				{
					break;
				}
			}
			for (int i = 1; i < virtualRightwardMovement + 1; i++)
			{
				BoardPosition check = pos.Translate(0, i);
				if (PositionIsEmpty(check))
				{
					ChessMove move = new ChessMove(pos, check);
					move.Player = player;
					move.PieceType = pieceType;
					attackMoves.Add(move);
					continue;
				} else if (PositionIsEnemy(check, player))
				{
					ChessMove move = new ChessMove(pos, check);
					move.Player = player;
					move.PieceType = pieceType;
					attackMoves.Add(move);
					break;
				} else
				{
					break;
				}
			}
			return attackMoves;
		}

		private List<ChessMove> GetKnightAttackMoves(BoardPosition knightPosition, int player) {
			var attackMoves = new List<ChessMove>();
			int row = knightPosition.Row;
			int col = knightPosition.Col;
			BoardPosition virtualForwardLeft = knightPosition.Translate(-2, -1);
			BoardPosition virtualsmallForwardLeft = knightPosition.Translate(-1, -2);
			BoardPosition virtualForwardRight = knightPosition.Translate(-2, 1);
			BoardPosition virtualsmallForwardRight = knightPosition.Translate(-1, 2);
			BoardPosition virtualBackwardLeft = knightPosition.Translate(2, -1);
			BoardPosition virtualsmallBackwardLeft = knightPosition.Translate(1, -2);
			BoardPosition virtualBackwardRight = knightPosition.Translate(2, 1);
			BoardPosition virtualsmallBackwardRight = knightPosition.Translate(1, 2);
			ChessMove move = new ChessMove(knightPosition, virtualForwardLeft);
			move.Player = player;
			move.PieceType = ChessPieceType.Knight;
			attackMoves.Add(move);
			ChessMove move9 = new ChessMove(knightPosition, virtualsmallForwardLeft);
			move9.Player = player;
			move9.PieceType = ChessPieceType.Knight;
			attackMoves.Add(move9);
			ChessMove move1 = new ChessMove(knightPosition, virtualForwardRight);
			move1.Player = player;
			move1.PieceType = ChessPieceType.Knight;
			attackMoves.Add(move1);
			ChessMove move8 = new ChessMove(knightPosition, virtualsmallForwardRight);
			move8.Player = player;
			move8.PieceType = ChessPieceType.Knight;
			attackMoves.Add(move8);
			ChessMove move2 = new ChessMove(knightPosition, virtualBackwardLeft);
			move2.Player = player;
			move2.PieceType = ChessPieceType.Knight;
			attackMoves.Add(move2);
			ChessMove move7 = new ChessMove(knightPosition, virtualsmallBackwardLeft);
			move7.Player = player;
			move7.PieceType = ChessPieceType.Knight;
			attackMoves.Add(move7);
			ChessMove move3 = new ChessMove(knightPosition, virtualBackwardRight);
			move3.Player = player;
			move3.PieceType = ChessPieceType.Knight;
			attackMoves.Add(move3);
			ChessMove move6 = new ChessMove(knightPosition, virtualsmallBackwardRight);
			move6.Player = player;
			move6.PieceType = ChessPieceType.Knight;
			attackMoves.Add(move6);
			return attackMoves;
		}

		private List<ChessMove> GetValidKnightMoves(BoardPosition knightPosition, int player) {
			var attackMoves = GetKnightAttackMoves(knightPosition, player);
			var validAttackMoves = new List<ChessMove>();
			foreach (var attackMove in attackMoves)
			{
				if (PositionIsEmpty(attackMove.EndPosition) || PositionIsEnemy(attackMove.EndPosition, player))
				{
					validAttackMoves.Add(attackMove);
				}
			}
			return validAttackMoves;
		}

		private List<ChessMove> GetValidDiagonalAttackMoves(BoardPosition pos, int player, ChessPieceType pieceType) {
			var validAttackMoves = new List<ChessMove>();
			int row = pos.Row;
			int col = pos.Col;
			int virtualForwardMovement = row;
			int virtualBackwardMovement = 7 - row;
			int virtualLeftwardMovement = col;
			int virtualRightwardMovement = 7 - col;
			for (int i = 1; i < virtualForwardMovement + 1;)
			{
				for (int j = 1; j < virtualLeftwardMovement + 1; j++)
				{
					BoardPosition virtualForwardLeft = pos.Translate(-i, -j);
					if (PositionIsEmpty(virtualForwardLeft))
					{
						ChessMove move = new ChessMove(pos, virtualForwardLeft);
						move.Player = player;
						move.PieceType = pieceType;
						validAttackMoves.Add(move);
					} else if (PositionIsEnemy(virtualForwardLeft, player))
					{
						ChessMove move = new ChessMove(pos, virtualForwardLeft);
						move.Player = player;
						move.PieceType = pieceType;
						validAttackMoves.Add(move);
						break;
					} else
					{
						break;
					}
					i++;
					if (i < virtualForwardMovement + 1)
					{
						continue;
					} else
					{
						break;
					}
				}
				break;
			}
			for (int i = 1; i < virtualBackwardMovement + 1;)
			{
				for (int j = 1; j < virtualLeftwardMovement + 1; j++)
				{
					BoardPosition virtualBackwardLeft = pos.Translate(i, -j);
					if (PositionIsEmpty(virtualBackwardLeft))
					{
						ChessMove move = new ChessMove(pos, virtualBackwardLeft);
						move.Player = player;
						move.PieceType = pieceType;
						validAttackMoves.Add(move);
					} else if (PositionIsEnemy(virtualBackwardLeft, player))
					{
						ChessMove move = new ChessMove(pos, virtualBackwardLeft);
						move.Player = player;
						move.PieceType = pieceType;
						validAttackMoves.Add(move);
						break;
					} else
					{
						break;
					}
					i++;
					if (i < virtualBackwardMovement + 1)
					{
						continue;
					} else
					{
						break;
					}
				}
				break;
			}
			for (int i = 1; i < virtualForwardMovement + 1;)
			{
				for (int j = 1; j < virtualRightwardMovement + 1; j++)
				{
					BoardPosition virtualForwardRight = pos.Translate(-i, j);
					if (PositionIsEmpty(virtualForwardRight))
					{
						ChessMove move = new ChessMove(pos, virtualForwardRight);
						move.Player = player;
						move.PieceType = pieceType;
						validAttackMoves.Add(move);
					} else if (PositionIsEnemy(virtualForwardRight, player))
					{
						ChessMove move = new ChessMove(pos, virtualForwardRight);
						move.Player = player;
						move.PieceType = pieceType;
						validAttackMoves.Add(move);
						break;
					} else
					{
						break;
					}
					i++;
					if (i < virtualForwardMovement + 1)
					{
						continue;
					} else
					{
						break;
					}
				}
				break;
			}
			for (int i = 1; i < virtualBackwardMovement + 1;)
			{
				for (int j = 1; j < virtualRightwardMovement + 1; j++)
				{
					BoardPosition virtualBackwardRight = pos.Translate(i, j);
					if (PositionIsEmpty(virtualBackwardRight))
					{
						ChessMove move = new ChessMove(pos, virtualBackwardRight);
						move.Player = player;
						move.PieceType = pieceType;
						validAttackMoves.Add(move);
					} else if (PositionIsEnemy(virtualBackwardRight, player))
					{
						ChessMove move = new ChessMove(pos, virtualBackwardRight);
						move.Player = player;
						move.PieceType = pieceType;
						validAttackMoves.Add(move);
						break;
					} else
					{
						break;
					}
					i++;
					if (i < virtualBackwardMovement + 1)
					{
						continue;
					} else
					{
						break;
					}
				}
				break;
			}
			return validAttackMoves;
		}

		private List<ChessMove> GetKingAttackMoves(BoardPosition kingPosition, int player) {
			var attackMoves = new List<ChessMove>();
			int row = kingPosition.Row;
			int col = kingPosition.Col;
			
			BoardPosition virtualForward = kingPosition.Translate(-1, 0);
			ChessMove moveVF = new ChessMove(kingPosition, virtualForward);
			moveVF.Player = player;
			moveVF.PieceType = ChessPieceType.King;
			attackMoves.Add(moveVF);
			
			BoardPosition virtualForwardLeft = kingPosition.Translate(-1, -1);
			ChessMove moveVFL = new ChessMove(kingPosition, virtualForwardLeft);
			moveVFL.Player = player;
			moveVFL.PieceType = ChessPieceType.King;
			attackMoves.Add(moveVFL);
			
			BoardPosition virtualLeft = kingPosition.Translate(0, -1);
			ChessMove moveVL = new ChessMove(kingPosition, virtualLeft);
			moveVL.Player = player;
			moveVL.PieceType = ChessPieceType.King;
			attackMoves.Add(moveVL);
			
			BoardPosition virtualBackwardLeft = kingPosition.Translate(1, -1);
			ChessMove moveVBL = new ChessMove(kingPosition, virtualBackwardLeft);
			moveVBL.Player = player;
			moveVBL.PieceType = ChessPieceType.King;
			attackMoves.Add(moveVBL);
			
			BoardPosition virtualBackward = kingPosition.Translate(1, 0);
			ChessMove moveVB = new ChessMove(kingPosition, virtualBackward);
			moveVB.Player = player;
			moveVB.PieceType = ChessPieceType.King;
			attackMoves.Add(moveVB);
			
			BoardPosition virtualBackwardRight = kingPosition.Translate(1, 1);
			ChessMove moveVBR = new ChessMove(kingPosition, virtualBackwardRight);
			moveVBR.Player = player;
			moveVBR.PieceType = ChessPieceType.King;
			attackMoves.Add(moveVBR);
			
			BoardPosition virtualRight = kingPosition.Translate(0, 1);
			ChessMove moveVR = new ChessMove(kingPosition, virtualRight);
			moveVR.Player = player;
			moveVR.PieceType = ChessPieceType.King;
			attackMoves.Add(moveVR);
			
			BoardPosition virtualForwardRight = kingPosition.Translate(-1, 1);
			ChessMove moveVFR = new ChessMove(kingPosition, virtualForwardRight);
			moveVFR.Player = player;
			moveVFR.PieceType = ChessPieceType.King;
			attackMoves.Add(moveVFR);
			return attackMoves;
		}

		private List<ChessMove> GetValidKingAttackMoves(BoardPosition kingPosition, int player) {
			var attackMoves = GetKingAttackMoves(kingPosition, player);
			var validAttackMoves = new List<ChessMove>();
			foreach (ChessMove attackMove in attackMoves)
			{
				if (PositionIsEmpty(attackMove.EndPosition) || PositionIsEnemy(attackMove.EndPosition, player))
				{
					validAttackMoves.Add(attackMove);
				}
			}
			return validAttackMoves;
		}

		/// <summary>
		/// Helper function
		/// </summary>
		public static int CountOneBits(String bitString) {
			int count = 0;
			foreach(char c in bitString) {
				if(c == '1') count++;
			}
			return count;
		}
		
		/// <summary>
		/// Helper function
		/// </summary>
		public static List<int> OneBitIndexes(String bitString) {
			var indexes = new List<int>();
			int prevIndex = bitString.IndexOf("1");
			indexes.Add(prevIndex);
			int oneBits = CountOneBits(bitString) - 1;
			while (oneBits != 0) {
				int index = bitString.IndexOf("1", prevIndex + 1);
				if (index != -1) {
					prevIndex = index;
					indexes.Add(index);
					oneBits--;
				}
			}
			return indexes;
		}

		/// <summary>
		/// Helper function
		/// Source: https://stackoverflow.com/questions/9367119/replacing-a-char-at-a-given-index-in-string
		/// </summary>
		public static string ReplaceAt(string input, int index, char newChar) {
			char[] chars = input.ToCharArray();
			chars[index] = newChar;
			return new string(chars);
		}
		
		/// <summary>
		/// Helper function
		/// </summary>
		public static Tuple<int, int> GetRowColFromBitString(String bitString) {
			int bitCount = bitString.Count();
			if (bitCount % 8 != 0) {
				int row = (64 - bitCount) / 8;
				int col = 8 - (bitCount % 8);
				return Tuple.Create(row, col);
			} else {
				int row = 8 - ((bitCount / 8));
				int col = 0;
				return Tuple.Create(row, col);
			}
		}

		/// <summary>
		/// Helper function
		/// </summary>
		private static List<Tuple<int,int>> TransformBitboardToRowsCols(ulong bitBoard) {
			String bitString = Convert.ToString((long)bitBoard, 2);
			String blankString = bitString.Replace("1", "0");
			var indexes = OneBitIndexes(bitString);
			var tuples = new List<Tuple<int,int>>();
			foreach(int i in indexes) {
				String pieceBoard = ReplaceAt(blankString, i, '1');
				if (i == 0) {
					var tup = GetRowColFromBitString(pieceBoard);
					tuples.Add(tup);
				} else {
					pieceBoard = pieceBoard.Substring(i);
					var tup = GetRowColFromBitString(pieceBoard);
					tuples.Add(tup);
				}
			}
			return tuples;
		}

		/// <summary>
		/// Helper function
		/// </summary>
		private List<BoardPosition> CreatePositionsListFromTuples(List<Tuple<int,int>> rowsCols) {
			var positions = new List<BoardPosition>();
			foreach (var rowCol in rowsCols)
			{
				positions.Add(new BoardPosition(rowCol.Item1, rowCol.Item2));
			}
			return positions;
		}

		/// <summary>
		/// Helper function
		/// </summary>
		private List<BoardPosition> CreatePositionsListFromBitboard(ulong bitBoard) {
			if (bitBoard == 0)
			{
				return new List<BoardPosition>();
			}
			var pieceRowsCols = TransformBitboardToRowsCols(bitBoard);
			return CreatePositionsListFromTuples(pieceRowsCols);
		}

		/// <summary>
		/// Helper function that turns row/col into corresponding 64bit value
		/// as described in the default constructor
		/// </summary>
		private static ulong TransformRowColToBitboard(int row, int col) {
			String formatString = "";
			for (int i = 0; i < 7 - row; i++)
			{
				formatString = "00000000" + formatString;
			}
			for (int j = 1; j < 7 - col; j++)
			{
				formatString = "0" + formatString;
			}
			if (col != 7) {
				formatString = "0" + formatString;
			}
			formatString = "1" + formatString;
			ulong bitBoard = Convert.ToUInt64(formatString, 2);
			return bitBoard;
		}

		/// <summary>
		/// Mutates the board state so that the given piece is at the given position.
		/// </summary>
		private void SetPieceAtPosition(BoardPosition position, ChessPiece piece) {
			int row = position.Row;
			int col = position.Col;
			ulong targetPosition = TransformRowColToBitboard(row, col);
			if ((targetPosition & WhitePawns) == targetPosition)
			{
				WhitePawns = WhitePawns ^ targetPosition;
			} else if ((targetPosition & WhiteRooks) == targetPosition)
			{
				WhiteRooks = WhiteRooks ^ targetPosition;
			} else if ((targetPosition & WhiteBishops) == targetPosition)
			{
				WhiteBishops = WhiteBishops ^ targetPosition;
			} else if ((targetPosition & WhiteKnights) == targetPosition)
			{
				WhiteKnights = WhiteKnights ^ targetPosition;
			} else if ((targetPosition & WhiteQueen) == targetPosition)
			{
				WhiteQueen = WhiteQueen ^ targetPosition;
			} else if ((targetPosition & WhiteKing) == targetPosition)
			{
				WhiteKing = WhiteKing ^ targetPosition;
			} else if ((targetPosition & BlackPawns) == targetPosition)
			{
				BlackPawns = BlackPawns ^ targetPosition;
			} else if ((targetPosition & BlackRooks) == targetPosition)
			{
				BlackRooks = BlackRooks ^ targetPosition;
			} else if ((targetPosition & BlackBishops) == targetPosition)
			{
				BlackBishops = BlackBishops ^ targetPosition;
			} else if ((targetPosition & BlackKnights) == targetPosition)
			{
				BlackKnights = BlackKnights ^ targetPosition;
			} else if ((targetPosition & BlackQueen) == targetPosition)
			{
				BlackQueen = BlackQueen ^ targetPosition;
			} else if ((targetPosition & BlackKing) == targetPosition)
			{
				BlackKing = BlackKing ^ targetPosition;
			}
			if (piece.PieceType != ChessPieceType.Empty)
			{
				if (piece.PieceType == ChessPieceType.Pawn)
				{
					if (piece.Player == 1)
					{
						WhitePawns = WhitePawns ^ targetPosition;
					} else {
						BlackPawns = BlackPawns ^ targetPosition;
					}
				} else if (piece.PieceType == ChessPieceType.Rook)
				{
					if (piece.Player == 1)
					{
						WhiteRooks = WhiteRooks ^ targetPosition;
					} else {
						BlackRooks = BlackRooks ^ targetPosition;
					}
				} else if (piece.PieceType == ChessPieceType.Knight)
				{
					if (piece.Player == 1)
					{
						WhiteKnights = WhiteKnights ^ targetPosition;
					} else {
						BlackKnights = BlackKnights ^ targetPosition;
					}
				} else if (piece.PieceType == ChessPieceType.Bishop)
				{
					if (piece.Player == 1)
					{
						WhiteBishops = WhiteBishops ^ targetPosition;
					} else {
						BlackBishops = BlackBishops ^ targetPosition;
					}
				}  else if (piece.PieceType == ChessPieceType.Queen)
				{
					if (piece.Player == 1)
					{
						WhiteQueen = WhiteQueen ^ targetPosition;
					} else {
						BlackQueen = BlackQueen ^ targetPosition;
					}
				} else if (piece.PieceType == ChessPieceType.King)
				{
					if (piece.Player == 1)
					{
						WhiteKing = WhiteKing ^ targetPosition;
					} else {
						BlackKing = BlackKing ^ targetPosition;
					}
				}
			}
		}

		#endregion

		#region Explicit IGameBoard implementations.
		IEnumerable<IGameMove> IGameBoard.GetPossibleMoves() {
			return GetPossibleMoves();
		}
		void IGameBoard.ApplyMove(IGameMove m) {
			ApplyMove(m as ChessMove);
		}
		IReadOnlyList<IGameMove> IGameBoard.MoveHistory => mMoveHistory;
		#endregion

		// You may or may not need to add code to this constructor.
		public ChessBoard() {
			/*
				Bit board diagram:
				  c0c1c2c3c4c5c6c7
				r0 R k B Q_K B k R_
				r1 P P P P_P P P P_
				r2 0 0 0 0_0 0 0 0_
				r3 0 0 0 0_0 0 0 0_
				r4 0 0 0 0_0 0 0 0_
				r5 0 0 0 0_0 0 0 0_
				r6 P P P P_P P P P_
				r7 R k B Q_K B k R
			 */
			BlackRooks   = 0b1000_0001_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000;
			BlackKnights = 0b0100_0010_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000;
			BlackBishops = 0b0010_0100_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000;
			BlackQueen   = 0b0001_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000;
			BlackKing    = 0b0000_1000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000;
			BlackPawns   = 0b0000_0000_1111_1111_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000;
			WhitePawns   = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1111_1111_0000_0000;
			WhiteRooks   = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000_0001;
			WhiteKnights = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0100_0010;
			WhiteBishops = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0010_0100;
			WhiteQueen   = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0000;
			WhiteKing    = 0b0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1000;
			Advantage = new GameAdvantage(0, 0);
			Player = 1;
			mIsCheck = false;
			mDrawCounterValues = new List<int>();
			mDrawCounterValues.Add(0);
		}

		public ChessBoard(IEnumerable<Tuple<BoardPosition, ChessPiece>> startingPositions)
			: this() {
			var king1 = startingPositions.Where(t => t.Item2.Player == 1 && t.Item2.PieceType == ChessPieceType.King);
			var king2 = startingPositions.Where(t => t.Item2.Player == 2 && t.Item2.PieceType == ChessPieceType.King);
			if (king1.Count() != 1 || king2.Count() != 1) {
				throw new ArgumentException("A chess board must have a single king for each player");
			}

			foreach (var position in BoardPosition.GetRectangularPositions(8, 8)) {
				SetPieceAtPosition(position, ChessPiece.Empty);
			}

			int[] values = { 0, 0 };
			Player = 1;
			Advantage = new GameAdvantage(0, 0);
			mIsCheck = false;
			mDrawCounterValues = new List<int>();
			mDrawCounterValues.Add(0);
			BoardPosition kingPosition = new BoardPosition(0,0);
			foreach (var pos in startingPositions) {
				if (pos.Item2.PieceType == ChessPieceType.King && pos.Item2.Player == 1) {
					kingPosition = pos.Item1;
				}
				SetPieceAtPosition(pos.Item1, pos.Item2);
				Advantage = CalculateCurrentAdvantage();
			}
			mIsCheck = PositionIsThreatened(kingPosition, 2);
		}
	}
}
