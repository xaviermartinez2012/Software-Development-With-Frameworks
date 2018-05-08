using System.Windows.Data;
using Cecs475.BoardGames.WpfView;

namespace Cecs475.BoardGames.Chess.WpfView
{
    public class ChessGameFactory : IWpfGameFactory
    {
        public string GameName
        {
            get
            {
                return "Chess";
            }
        }

        public IValueConverter CreateBoardAdvantageConverter()
        {
            return new ChessAdvantageConverter();
        }

        public IValueConverter CreateCurrentPlayerConverter()
        {
            return new ChessPlayerConverter();
        }

        public IWpfGameView CreateGameView()
        {
            return new ChessView();
        }
    }
}
