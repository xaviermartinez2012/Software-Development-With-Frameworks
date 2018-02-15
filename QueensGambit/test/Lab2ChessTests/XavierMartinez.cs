using System;
using System.Collections.Generic;
using System.Text;

using Cecs475.BoardGames.Model;
using Cecs475.BoardGames.Chess.Test;
using Cecs475.BoardGames.Chess.Model;
using Cecs475.BoardGames.Chess.View;
using Xunit;
using FluentAssertions;
using System.Linq;

namespace Lab2ChessTests {
	public class MyChessTests : ChessTest {

		/* This is where you will write your tests.
		 * Each test must be marked with the [Test] attribute.
		 * 
		 * Double check that you follow these rules:
		 * 
		 * 0. RENAME THIS FILE to YourName.cs, but USE YOUR ACTUAL NAME.
		 * 1. Every test method should have a meaningful name.
		 * 2. Every Should() must include a string description of the expectation.
		 * 3. Your buster test should be LAST in this file, and should be given a meaningful name
		 *		FOLLOWED BY AN UNDERSCORE, followed by the LAST 6 digits of your student ID.
		 *		Example:
		 *		
		 *		If my ID is 012345678 and involves undoing a castling move, my test might be named
		 *		UndoCastleQueenSide_345678
		 *	
		 */
		 
		/// <summary>
		/// Testing the initial starting board state
		/// </summary>
		[Fact]
		public void InitialBoardState() {
			ChessBoard b = new ChessBoard();

			var currentPlayer = b.CurrentPlayer;
			currentPlayer.Should().Be(1, "Player 1 should be the first to make a move");
			var stalemate = b.IsStalemate;
			stalemate.Should().BeFalse("there should not be a stalement at the beginning of the game");
			var checkmate = b.IsCheckmate;
			checkmate.Should().BeFalse("there should not be a checkmate at the beginning of the game");
			var check = b.IsCheck;
			check.Should().BeFalse("there should not be a check at the beginning of the game");
			var drawCounter = b.DrawCounter;
			drawCounter.Should().Be(0, "draws should not have happened at the beginning of the game");
			var moveHistory = b.MoveHistory;
			moveHistory.Should().BeEmpty("no moves should have been made yet");
			var isFinished = b.IsFinished;
			isFinished.Should().BeFalse("the game should not be finished at the beginning");
			var currentAdvantage = b.CurrentAdvantage;
			currentAdvantage.Player.Should().Be(0, "no player should have an advantage at the beginning of the game");
			currentAdvantage.Advantage.Should().Be(0, "no player should have an advantage at the beginning of the game");
			char[] columns = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'};
			foreach (var column in columns)
			{
				var pawnWhite = b.GetPieceAtPosition(Pos(string.Format("{0}2", column)));
				var pawnBlack = b.GetPieceAtPosition(Pos(string.Format("{0}7", column)));
				pawnWhite.PieceType.Should().Be(ChessPieceType.Pawn, "pieces in row 2 should be pawns at the beginning of the game");
				pawnBlack.PieceType.Should().Be(ChessPieceType.Pawn, "pieces in row 7 should be pawns at the beginning of the game");
				if (column == 'a')
				{
					var pieceWhite = b.GetPieceAtPosition(Pos(string.Format("{0}1", column)));
					var pieceBlack = b.GetPieceAtPosition(Pos(string.Format("{0}8", column)));
					pieceWhite.PieceType.Should().Be(ChessPieceType.Rook, string.Format("piece at ({0},1) should be a rook at the beginning of the game", column));
					pieceBlack.PieceType.Should().Be(ChessPieceType.Rook, string.Format("piece at ({0},8) should be a rook at the beginning of the game", column));
				} else if (column == 'b')
				{
					var pieceWhite = b.GetPieceAtPosition(Pos(string.Format("{0}1", column)));
					var pieceBlack = b.GetPieceAtPosition(Pos(string.Format("{0}8", column)));
					pieceWhite.PieceType.Should().Be(ChessPieceType.Knight, string.Format("piece at ({0},1) should be a knight at the beginning of the game", column));
					pieceBlack.PieceType.Should().Be(ChessPieceType.Knight, string.Format("piece at ({0},8) should be a knight at the beginning of the game", column));
				} else if (column == 'c')
				{
					var pieceWhite = b.GetPieceAtPosition(Pos(string.Format("{0}1", column)));
					var pieceBlack = b.GetPieceAtPosition(Pos(string.Format("{0}8", column)));
					pieceWhite.PieceType.Should().Be(ChessPieceType.Bishop, string.Format("piece at ({0},1) should be a bishop at the beginning of the game", column));
					pieceBlack.PieceType.Should().Be(ChessPieceType.Bishop, string.Format("piece at ({0},8) should be a bishop at the beginning of the game", column));
				} else if (column == 'd')
				{
					var pieceWhite = b.GetPieceAtPosition(Pos(string.Format("{0}1", column)));
					var pieceBlack = b.GetPieceAtPosition(Pos(string.Format("{0}8", column)));
					pieceWhite.PieceType.Should().Be(ChessPieceType.Queen, string.Format("piece at ({0},1) should be a queen at the beginning of the game", column));
					pieceBlack.PieceType.Should().Be(ChessPieceType.Queen, string.Format("piece at ({0},8) should be a queen at the beginning of the game", column));
				} else if (column == 'e')
				{
					var pieceWhite = b.GetPieceAtPosition(Pos(string.Format("{0}1", column)));
					var pieceBlack = b.GetPieceAtPosition(Pos(string.Format("{0}8", column)));
					pieceWhite.PieceType.Should().Be(ChessPieceType.King, string.Format("piece at ({0},1) should be a King at the beginning of the game", column));
					pieceBlack.PieceType.Should().Be(ChessPieceType.King, string.Format("piece at ({0},8) should be a King at the beginning of the game", column));
				} else if (column == 'f')
				{
					var pieceWhite = b.GetPieceAtPosition(Pos(string.Format("{0}1", column)));
					var pieceBlack = b.GetPieceAtPosition(Pos(string.Format("{0}8", column)));
					pieceWhite.PieceType.Should().Be(ChessPieceType.Bishop, string.Format("piece at ({0},1) should be a Bishop at the beginning of the game", column));
					pieceBlack.PieceType.Should().Be(ChessPieceType.Bishop, string.Format("piece at ({0},8) should be a Bishop at the beginning of the game", column));
				} else if (column == 'g')
				{
					var pieceWhite = b.GetPieceAtPosition(Pos(string.Format("{0}1", column)));
					var pieceBlack = b.GetPieceAtPosition(Pos(string.Format("{0}8", column)));
					pieceWhite.PieceType.Should().Be(ChessPieceType.Knight, string.Format("piece at ({0},1) should be a knight at the beginning of the game", column));
					pieceBlack.PieceType.Should().Be(ChessPieceType.Knight, string.Format("piece at ({0},8) should be a knight at the beginning of the game", column));
				} else
				{
					var pieceWhite = b.GetPieceAtPosition(Pos(string.Format("{0}1", column)));
					var pieceBlack = b.GetPieceAtPosition(Pos(string.Format("{0}8", column)));
					pieceWhite.PieceType.Should().Be(ChessPieceType.Rook, string.Format("piece at ({0},1) should be a rook at the beginning of the game", column));
					pieceBlack.PieceType.Should().Be(ChessPieceType.Rook, string.Format("piece at ({0},8) should be a rook at the beginning of the game", column));
				}
			}
		}
		
		/// <summary>
		/// Testing enpassant
		/// </summary>
		[Fact]
		public void EnPassant() {
			ChessBoard b = CreateBoardFromMoves(
				"c2, c4",
				"f7, f6",
				"c4, c5",
				"d7, d5"
			);

			var possMoves = b.GetPossibleMoves();
			var enPassantExpected = GetMovesAtPosition(possMoves, Pos("c5"));
			enPassantExpected.Should().HaveCount(2, "pawn can move forward one space or capture en passant")
				.And.Contain(Move("c5, c6"))
				.And.Contain(Move(Pos("c5"), Pos("d6"), ChessMoveType.EnPassant));

			// Apply the en passant
			Apply(b, Move(Pos("c5"), Pos("d6"), ChessMoveType.EnPassant));
			var pawn = b.GetPieceAtPosition(Pos("d6"));
			pawn.Player.Should().Be(1, "pawn performed en passant move");
			pawn.PieceType.Should().Be(ChessPieceType.Pawn, "pawn performed en passant move");
			var captured = b.GetPieceAtPosition(Pos("d5"));
			captured.Player.Should().Be(0, "the pawn that moved to b5 was captured by en passant");
			captured.PieceType.Should().Be(ChessPieceType.Empty, "the pawn that moved to b5 was captured by en passant");
			b.CurrentAdvantage.Should().Be(Advantage(1, 1), "player 1 captured player 2's pawn by en passant");

			// Undo the move and check the board state
			b.UndoLastMove();
			b.CurrentAdvantage.Should().Be(Advantage(0, 0), "en passant move was undone");
			pawn = b.GetPieceAtPosition(Pos("c5"));
			pawn.Player.Should().Be(1, "en passant move was undone");
			captured = b.GetPieceAtPosition(Pos("d5"));
			captured.Player.Should().Be(2, "en passant move was undone");
			var empty = b.GetPieceAtPosition(Pos("d6"));
			empty.Player.Should().Be(0, "neither player has a piece at d6");
			empty.PieceType.Should().Be(ChessPieceType.Empty, "en passant move was undone");
		}

		/// <summary>
		/// Testing undo of knight capture
		/// </summary>
		[Fact]
		public void UndoKnightCapture() {
			ChessBoard b = CreateBoardFromMoves(
				"c2, c4",
				"e7, e5",
				"c4, c5",
				"e5, e4",
				"c5, c6"
			);

			var possMoves = b.GetPossibleMoves();
			var kightCaptureExpected = GetMovesAtPosition(possMoves, Pos("b8"));
			kightCaptureExpected.Should().HaveCount(2, "kight can move two different ways from b8")
				.And.Contain(Move("b8, a6"))
				.And.Contain(Move(Pos("b8"), Pos("c6"), ChessMoveType.Normal));

			// Apply the knight capture passant
			Apply(b, Move(Pos("b8"), Pos("c6"), ChessMoveType.Normal));
			var knight = b.GetPieceAtPosition(Pos("c6"));
			knight.Player.Should().Be(2, "kight performed capture");
			knight.PieceType.Should().Be(ChessPieceType.Knight, "kight performed capture");
			b.CurrentAdvantage.Should().Be(Advantage(2, 1), "player 2 captured player 1's pawn with a knight");

			// Undo the move and check the board state
			b.UndoLastMove();
			b.CurrentAdvantage.Should().Be(Advantage(0, 0), "knight capture was undone");
			knight = b.GetPieceAtPosition(Pos("b8"));
			knight.Player.Should().Be(2, "knight capture was undone");
			var captured = b.GetPieceAtPosition(Pos("c6"));
			captured.Player.Should().Be(1, "knight capture was undone");
			var boardIntegrity = b.GetPieceAtPosition(Pos("e4"));
			boardIntegrity.Player.Should().Be(2, "player 2 has a piece at e4");
			boardIntegrity.PieceType.Should().Be(ChessPieceType.Pawn, "knight capture was undone");
		}

		/// <summary>
		/// Testing undo of queen capture
		/// </summary>
		[Fact]
		public void UndoQueenCapture() {
			ChessBoard b = CreateBoardFromMoves(
				"c2, c4",
				"e7, e5",
				"c4, c5",
				"e5, e4",
				"d1, c2",
				"d7, d5"
			);

			var possMoves = b.GetPossibleMoves();
			var queenCaptureExpected = GetMovesAtPosition(possMoves, Pos("c2"));
			b.GetPieceAtPosition(Pos("c2")).PieceType.ShouldBeEquivalentTo(ChessPieceType.Queen, "the queen was moved from d1 -> c2");

			queenCaptureExpected.Should().HaveCount(7, "queen can move diagonally in three different ways and vertically in one from c2")
				.And.Contain(Move("c2, b3"))
				.And.Contain(Move("c2, a4"))
				.And.Contain(Move("c2, d3"))
				.And.Contain(Move("c2, c3"))
				.And.Contain(Move("c2, c4"))
				.And.Contain(Move("c2, d1"))
				.And.Contain(Move(Pos("c2"), Pos("e4"), ChessMoveType.Normal));

			// Apply the queen capture
			Apply(b, Move(Pos("c2"), Pos("e4"), ChessMoveType.Normal));
			var queen = b.GetPieceAtPosition(Pos("e4"));
			queen.Player.Should().Be(1, "queen performed capture");
			queen.PieceType.Should().Be(ChessPieceType.Queen, "queen performed capture");
			b.CurrentAdvantage.Should().Be(Advantage(1, 1), "player 1 captured player 2's pawn with a queen");

			// Undo the move and check the board state
			b.UndoLastMove();
			b.CurrentAdvantage.Should().Be(Advantage(0, 0), "queen capture was undone");
			queen = b.GetPieceAtPosition(Pos("c2"));
			queen.Player.Should().Be(1, "queen capture was undone");
			queen.PieceType.Should().Be(ChessPieceType.Queen, "queen capture was undone");
			var captured = b.GetPieceAtPosition(Pos("e4"));
			captured.Player.Should().Be(2, "queen capture was undone");
			var boardIntegrity = b.GetPieceAtPosition(Pos("c5"));
			boardIntegrity.Player.Should().Be(1, "player 1 has a piece at e4");
			boardIntegrity.PieceType.Should().Be(ChessPieceType.Pawn, "queen capture was undone");
		}

		/// <summary>
		/// Testing bishop possible moves
		/// </summary>
		[Fact]
		public void BishopPossibleMoves() {
			ChessBoard b = CreateBoardFromMoves(
				"a2, a4",
				"d7, d5",
				"b2, b4"
			);
			var possMoves = b.GetPossibleMoves();
			var bishopMoves = GetMovesAtPosition(possMoves, Pos("c8"));
			bishopMoves.Should().HaveCount(5, "kight can move in one diagonal from b8")
				.And.Contain(Move("c8, d7"))
				.And.Contain(Move("c8, e6"))
				.And.Contain(Move("c8, f5"))
				.And.Contain(Move("c8, g4"))
				.And.Contain(Move("c8, h3"));
		}
		
		/// <summary>
		/// Testing possible moves from placing a king in check
		/// </summary>
		[Fact]
		public void KingInCheck() {
			ChessBoard b = CreateBoardFromMoves(
				"d2, d4",
				"d7, d5",
				"e2, e4",
				"e7, e6",
				"h2, h4",
				"f8, b4"
			);
			b.IsCheck.Should().BeTrue("the king is in check");
			var possMoves = b.GetPossibleMoves();
			possMoves.Should().HaveCount(6, "the king can move out of check")
				.And.Contain(Move("e1, e2"))
				.And.Contain(Move("d1, d2"))
				.And.Contain(Move("c1, d2"))
				.And.Contain(Move("b1, d2"))
				.And.Contain(Move("b1, c3"))
				.And.Contain(Move("c2, c3"));
			b.UndoLastMove();
			b.IsCheck.Should().BeFalse("the king is no longer in check as the last move was undone");
		}
		
		/// <summary>
		/// BUSTER CASE
		/// </summary>
		[Fact]
		public void ConsistencyCheck_024398() {
			ChessBoard b = new ChessBoard();
			char[] columns = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'};
			int count = 1;
			while (count < 3)
			{
				foreach (var column in columns)
				{
					int white = 2 + count;
					int whitePrev = white - 1;
					int black = 7 - count;
					int blackPrev = black + 1;
					Apply(b, string.Format("{0}{1}, {2}{3}", column, whitePrev, column, white));
					Apply(b, string.Format("{0}{1}, {2}{3}", column, blackPrev, column, black));
				}
				count++;
			}
			count++;
			for (int i = 0; i < columns.Count(); i+=2)
			{
				char column = columns[i];
				char columnPlusOne = columns[i+1];
				Apply(b, Move(Pos(string.Format("{0}{1}", column, count)), Pos(string.Format("{0}{1}", columnPlusOne, count + 1)), ChessMoveType.Normal));
				b.CurrentAdvantage.Should().Be(Advantage(1, 1), "Player 1 captured pawn");
				Apply(b, Move(Pos(string.Format("{0}{1}", column, count + 1)), Pos(string.Format("{0}{1}", columnPlusOne, count)), ChessMoveType.Normal));
				b.CurrentAdvantage.Should().Be(Advantage(0, 0), "Player 2 captured pawn");
				if (column == 'g')
				{
					break;
				}
			}
			for (int i = 2; i < 8; i++)
			{
				Apply(b, string.Format("{0}{1}, {2}{3}", 'a', 1, 'a', i));
				b.GetPieceAtPosition(Pos(string.Format("{0}{1}", 'a', i))).PieceType.ShouldBeEquivalentTo(ChessPieceType.Rook, string.Format("rook was moved to forward from a1 -> a{0}", i));
				b.UndoLastMove();
				b.GetPieceAtPosition(Pos(string.Format("{0}{1}", 'a', i))).PieceType.ShouldBeEquivalentTo(ChessPieceType.Empty, string.Format("board moved rook back from a{0} -> a1", i));
			}
			for (int i = 1; i < columns.Count(); i+=2)
			{
				char column = columns[i];
				var white = b.GetPieceAtPosition(Pos(string.Format("{0}{1}", column, 5)));
				white.Player.ShouldBeEquivalentTo(1, string.Format("white has a pawn at {0}5", column));
				white.PieceType.ShouldBeEquivalentTo(ChessPieceType.Pawn, string.Format("white has a pawn at {0}5", column));
				var black = b.GetPieceAtPosition(Pos(string.Format("{0}{1}", column, 4)));
				black.Player.ShouldBeEquivalentTo(2, string.Format("black has a pawn at {0}4", column));
				black.PieceType.ShouldBeEquivalentTo(ChessPieceType.Pawn, string.Format("black has a pawn at {0}4", column));
			}
			b.CurrentAdvantage.Should().Be(Advantage(0, 0), "no player has an advantage");
		}
	}
}
