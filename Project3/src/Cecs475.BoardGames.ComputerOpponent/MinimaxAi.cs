using Cecs475.BoardGames.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.BoardGames.ComputerOpponent
{
    internal struct MinimaxBestMove
    {
        public long Weight { get; set; }
        public IGameMove Move { get; set; }
    }

    public class MinimaxAi : IGameAi
    {
        private long mMaxDepth;
        public MinimaxAi(int maxDepth)
        {
            mMaxDepth = maxDepth;
        }

        public IGameMove FindBestMove(IGameBoard b)
        {
            return FindBestMove(b,
                true ? long.MinValue : long.MaxValue,
                true ? long.MaxValue : long.MinValue,
                mMaxDepth).Move;
        }

        private static MinimaxBestMove FindBestMove(IGameBoard b, long alpha, long beta, long depthLeft)
        {
            if (depthLeft == 0 || b.IsFinished)
            {
                return new MinimaxBestMove
                {
                    Move = null,
                    Weight = b.BoardWeight
                };
            }

            var iAlpha = alpha;
            var iBeta = beta;
            bool isMaximizing = b.CurrentPlayer == 1;
            IGameMove bestMove = null;
            foreach (var move in b.GetPossibleMoves())
            {
                if (!(iAlpha < iBeta))
                {
                    return new MinimaxBestMove
                    {
                        Weight = isMaximizing ? iBeta : iAlpha,
                        Move = bestMove
                    };
                }
                b.ApplyMove(move);
                var w = FindBestMove(b, iAlpha, iBeta, depthLeft - 1).Weight;
                b.UndoLastMove();
                if (isMaximizing)
                {
                    if (w > iAlpha)
                    {
                        iAlpha = w;
                        bestMove = move;
                    }
                } else
                {
                    if (w < iBeta)
                    {
                        iBeta = w;
                        bestMove = move;
                    }
                }
                
            }
            return new MinimaxBestMove
            {
                Weight = isMaximizing ? iAlpha : iBeta,
                Move = bestMove
            };
        }

    }
}