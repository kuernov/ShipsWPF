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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace WpfShips
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 
 
    public partial class MainWindow : Window
    {
        Game game;
       
        Button[,] playerPositionButtons;
        Button[,] computerPositionButtons;
        SolidColorBrush[] foregroundByShipSize = new SolidColorBrush[] { Brushes.Black, Brushes.Purple, Brushes.Red, Brushes.Green, Brushes.Orange }; 
        
        public MainWindow()
        {
            InitializeComponent();
            game = new Game();
            playerPositionButtons = new Button[,] { { A1, B1, C1, D1, E1, F1, G1, H1 }, { A2, B2, C2, D2, E2, F2, G2, H2 }, { A3, B3, C3, D3, E3, F3, G3, H3 }, { A4, B4, C4, D4, E4, F4, G4, H4 }, { A5, B5, C5, D5, E5, F5, G5, H5 }, { A6, B6, C6, D6, E6, F6, G6, H6 }, { A7, B7, C7, D7, E7, F7, G7, H7 }, { A8, B8, C8, D8, E8, F8, G8, H8 } };
            computerPositionButtons = new Button[,] { { A1c, B1c, C1c, D1c, E1c, F1c, G1c, H1c }, { A2c, B2c, C2c, D2c, E2c, F2c, G2c, H2c }, { A3c, B3c, C3c, D3c, E3c, F3c, G3c, H3c }, { A4c, B4c, C4c, D4c, E4c, F4c, G4c, H4c }, { A5c, B5c, C5c, D5c, E5c, F5c, G5c, H5c }, { A6c, B6c, C6c, D6c, E6c, F6c, G6c, H6c }, { A7c, B7c, C7c, D7c, E7c, F7c, G7c, H7c }, { A8c, B8c, C8c, D8c, E8c, F8c, G8c, H8c } };
        }

        private void RestartGame()
        {
            game = new Game();
            renderGame();
        }

        private void renderGame()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var player = game.getPlayerAtCoordinates(i, j);
                    RenderPlayerButton(playerPositionButtons[i, j], player, game.gameState);

                    var computer = game.getComputerAtCoordinates(i, j);
                    RenderComputerButton(computerPositionButtons[i, j], computer, game.gameState);

                }
            }
        }

        private void RenderPlayerButton(Button button, ButtonCondition condition, GameState gameState)
        {
            button.IsEnabled = true;
            button.Tag = null;
            button.BorderBrush = GetBorderBrush(condition);
            button.Foreground = foregroundByShipSize[condition.TypeOfShip];
        }

        private void RenderComputerButton(Button button, ButtonCondition condition, GameState gameState)
        {
            button.IsEnabled = gameState == GameState.PLAYING;
            button.Tag = null;
            button.BorderBrush = GetBorderBrush(condition);

            // DO NOT RENDER COLORS FOR COMPUTER IN FINAL VERSION
            button.Foreground = foregroundByShipSize[condition.TypeOfShip];
        }

        private SolidColorBrush GetBorderBrush(ButtonCondition condition)
        {
            if (condition.Hit)
            {
                return condition.Occupied ? Brushes.Red : Brushes.Black;
            }
            else
            {
                return Brushes.Gray;
            }
        }

        private void playerLocationPicker(object sender, EventArgs e)
        {
            Button button = (Button) sender;
            var coords = ButtonToCoords(button);
            if (game.gameState == GameState.PICKING)
            {
                game.Pick(coords);
            } 

            renderGame();
        }
        private void HandlePlayerShoot(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var coords = ButtonToCoords(button);
            if (game.gameState == GameState.PLAYING && game.PlayerShoot(coords)==true )
            {
                game.PlayerShoot(coords);
            }
            else if (game.gameState == GameState.PLAYING)
            {
                game.PlayerShoot(coords);
                game.ComputerShoot();
            }
            renderGame();
        }

        private void HandleShoot(Coords coords)
        {
            game.PlayerShoot(coords);
            game.ComputerShoot();

        }

        private Coords ButtonToCoords(Button button)
        {
            var buttonCoordinates = (string )button.Content;
            int x = buttonCoordinates[0] - 'A';
            int y = buttonCoordinates[1] - '1';
            Console.WriteLine($"Detected button click at x: {x}, y: {y}");

            return new Coords(x, y);
        }
        private void GameOverEvent()
        {

        }

        private void ClickButtonRestart(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }
    }
}
