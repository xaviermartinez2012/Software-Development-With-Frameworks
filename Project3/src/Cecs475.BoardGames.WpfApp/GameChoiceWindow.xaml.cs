using Cecs475.BoardGames.WpfView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace Cecs475.BoardGames.WpfApp {
	/// <summary>
	/// Interaction logic for GameChoiceWindow.xaml
	/// </summary>
	public partial class GameChoiceWindow : Window {

        private IEnumerable<IWpfGameFactory> GameTypes { get; set; }

        public GameChoiceWindow() {
            FindGames();
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			Button b = sender as Button;
			// Retrieve the game type bound to the button
			IWpfGameFactory gameType = b.DataContext as IWpfGameFactory;
            // Construct a GameWindow to play the game.
            var gameWindow = new GameWindow(gameType,
                mHumanBtn.IsChecked.Value ? NumberOfPlayers.Two : NumberOfPlayers.One)
            {
                Title = gameType.GameName
            };
            // When the GameWindow closes, we want to show this window again.
            gameWindow.Closed += GameWindow_Closed;

			// Show the GameWindow, hide the Choice window.
			gameWindow.Show();
			this.Hide();
		}

		private void GameWindow_Closed(object sender, EventArgs e) {
			this.Show();
		}

        private void FindGames()
        {
            Type gameType = typeof(IWpfGameFactory);
            var files = Directory.EnumerateFiles(@"games", "*.dll");
            foreach (var file in files)
            {
                Assembly.LoadFrom(file);
            }
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var matchingTypes = new List<Type>();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                var target = types.Where(t => gameType.IsAssignableFrom(t) && !t.IsInterface).Select(t => t);
                if (target.Any())
                {
                    matchingTypes.AddRange(target);
                }
            }
            GameTypes = matchingTypes.Select(t => (IWpfGameFactory) Activator.CreateInstance(t));
            this.Resources.Add("GameTypes", GameTypes);
        }
	}
}
