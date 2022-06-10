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
    public class ButtonCondition
    {
        public bool IsthereShip;
        public bool Isdestroyed;
        public int TypeOfship;
        public Button button;
        public ButtonCondition()
        {
            IsthereShip = false;
            Isdestroyed = false;
            TypeOfship = 0;
        }
    }
    public class BoardCondition
    {
        int ShipsAlive;
    }
    public partial class MainWindow : Window
    {
        BoardCondition boardCondition;
        
        //
        ButtonCondition[,] playerBoardCondition =new ButtonCondition[8,8];
        Button[,] playerPositionButtons;
        Button[,] computerPositionButtons;
        Random rnd=new Random();
        int totalShips = 10;
        int quadrupleShips = 4;
        int tripleShips = 3;
        int doubleShips = 2;
        int singleShips = 1;
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void RestartGame()
        {
            
            playerPositionButtons = new Button[,] { { A1, B1, C1, D1, E1, F1, G1, H1 }, { A2, B2, C2, D2, E2, F2, G2, H2 }, { A3, B3, C3, D3, E3, F3, G3, H3 }, { A4, B4, C4, D4, E4, F4, G4, H4 }, { A5, B5, C5, D5, E5, F5, G5, H5 }, { A6, B6, C6, D6, E6, F6, G6, H6 }, { A7, B7, C7, D7, E7, F7, G7, H7 }, { A8, B8, C8, D8, E8, F8, G8, H8 } };
            computerPositionButtons = new Button[,] { { A1c, B1c, C1c, D1c, E1c, F1c, G1c, H1c }, { A2c, B2c, C2c, D2c, E2c, F2c, G2c, H2c }, { A3c, B3c, C3c, D3c, E3c, F3c, G3c, H3c }, { A4c, B4c, C4c, D4c, E4c, F4c, G4c, H4c }, { A5c, B5c, C5c, D5c, E5c, F5c, G5c, H5c }, { A6c, B6c, C6c, D6c, E6c, F6c, G6c, H6c }, { A7c, B7c, C7c, D7c, E7c, F7c, G7c, H7c }, { A8c, B8c, C8c, D8c, E8c, F8c, G8c, H8c } };
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    playerPositionButtons[i, j].IsEnabled = true;
                    playerPositionButtons[i, j].Tag = null;
                    playerPositionButtons[i, j].Background = Brushes.LightGray;
                    playerPositionButtons[i, j].Foreground = Brushes.Black;
                    playerBoardCondition[i, j] = new ButtonCondition();
                    playerBoardCondition[i, j].button = playerPositionButtons[i, j];
                }
            }
        }

        private void playerLocationPicker(object sender, EventArgs e)
        {
            Button[] QuadrupleLocation = new Button[4];
            Button[] TripleLocation = new Button[3];
            Button[] DoubleLocation = new Button[2];
            Button SingleLocation;
            if (quadrupleShips > 0)
            {
                Button button = (Button)sender;
                button.Foreground = Brushes.Orange;
                button.IsEnabled = false;
                button.Tag = "playerQuadrupleShip";
                quadrupleShips--;
                totalShips--;
                QuadrupleLocation[quadrupleShips] = button;
                int index = Array.FindIndex(playerPositionButtons)
            }

            else if (tripleShips > 0)
            {

                var button = (Button)sender;
                button.Foreground = Brushes.Green;
                button.IsEnabled = false;
                button.Tag = "playerTripleShip";
                tripleShips -= 1;
                totalShips -= 1;
                TripleLocation[tripleShips] = button;
            }

            else if (doubleShips > 0)
            {

                var button = (Button)sender;
                button.Foreground = Brushes.Red;
                button.IsEnabled = false;
                button.Tag = "playerDoubleShip";
                doubleShips -= 1;
                totalShips -= 1;
                DoubleLocation[doubleShips] = button;
            }

            else if (singleShips > 0)
            {

                var button = (Button)sender;
                button.Foreground = Brushes.Purple;
                button.IsEnabled = false;
                button.Tag = "playerSingleShip";
                singleShips -= 1;
                totalShips -= 1;
                SingleLocation = button;
            }
        }

        private void VerticalSpaceCheck(int x, int y, int ShipSize)
        {
            int iterator = 1;
            int left = x;
            int right = x;
            while (right < 7 && iterator!=ShipSize)
            {
                if(computerPositionButtons[y,right].IsEnabled==true)
                {
                    right++;
                    iterator++;
                }
            }
            while (right >0 && iterator != ShipSize)
            {
                if (computerPositionButtons[y, left].IsEnabled == true)
                {
                    left--;
                    iterator++;
                }
            }
        }

        private void RandomQuadrupleShip()
        {
            Button[] computerQuadrupleLocation = new Button[4];
            int iterator = 1;
            int  x= rnd.Next(0, 8);
            int y = rnd.Next(0, 8);
            int XorY = rnd.Next(1,3);
            computerQuadrupleLocation[0] = computerPositionButtons[y, x];
            computerQuadrupleLocation[0].Background = Brushes.Blue;
            if (XorY == 1)
            {
                int left = x;
                int right = x;
                while (iterator < 4)
                {
                    if (right < 7)
                    {
                        right++;
                        computerQuadrupleLocation[iterator] = computerPositionButtons[y,right];
                        computerPositionButtons[y,right].Tag = "computerQuadrupleShip";
                        computerPositionButtons[y, right].Background = Brushes.Blue;
                        iterator++;
                    }
                    else
                    {
                        left--;
                        computerQuadrupleLocation[iterator] = computerPositionButtons[y,left];
                        computerPositionButtons[y,left].Tag = "computerQuadrupleShip";
                        computerPositionButtons[y,left].Background = Brushes.Blue;
                        iterator++;
                    }
                }
            }
            else if(XorY==2)
            {
                int up = y;
                int down = y;
                while (iterator < 4)
                {
                    if (down < 7)
                    {
                        down++;
                        computerQuadrupleLocation[iterator] = computerPositionButtons[down, x];
                        computerPositionButtons[down,x].Tag = "computerQuadrupleShip";
                        computerPositionButtons[down,x].Background = Brushes.Blue;
                        iterator++;

                  
                    }
                    else
                    {
                        up--;
                        computerQuadrupleLocation[iterator] = computerPositionButtons[up,x];
                        computerPositionButtons[up,x].Tag = "computerQuadrupleShip";
                        computerPositionButtons[up,x].Background = Brushes.Blue;
                        iterator++;
                    }
                }


            }
        }
        private void RandomTripleShip()
        {
            Button[] computerTripleLocation = new Button[3];
            int iterator = 1;
            int x = rnd.Next(0, 8);
            int y = rnd.Next(0, 8);
            int XorY = rnd.Next(1, 3);
            computerTripleLocation[0] = computerPositionButtons[y, x];
            computerTripleLocation[0].Background = Brushes.Red;
            if (XorY == 1)
            {
                int left = x;
                int right = x;
                while (iterator < 3)
                {
                    if (right < 7)
                    {
                        right++;
                        computerTripleLocation[iterator] = computerPositionButtons[y, right];
                        computerPositionButtons[y, right].Tag = "computerTripleShip";
                        computerPositionButtons[y, right].Background = Brushes.Red;
                        iterator++;
                    }
                    else
                    {
                        left--;
                        computerTripleLocation[iterator] = computerPositionButtons[y, left];
                        computerPositionButtons[y, left].Tag = "computerTripleShip";
                        computerPositionButtons[y, left].Background = Brushes.Red;
                        iterator++;
                    }
                }
            }
            else if (XorY == 2)
            {
                int up = y;
                int down = y;
                while (iterator < 3)
                {
                    if (down < 7)
                    {
                        down++;
                        computerTripleLocation[iterator] = computerPositionButtons[down, x];
                        computerPositionButtons[down, x].Tag = "computerTripleShip";
                        computerPositionButtons[down, x].Background = Brushes.Red;
                        iterator++;
                    }
                    else
                    {
                        up--;
                        computerTripleLocation[iterator] = computerPositionButtons[up, x];
                        computerPositionButtons[up, x].Tag = "computerTripleShip";
                        computerPositionButtons[up, x].Background = Brushes.Red;
                        iterator++;
                    }
                }


            }
        }
        private void enemyLocationPicker()
        {

            RandomQuadrupleShip();
            RandomTripleShip();
        }
        private void ClickButtonRestart(object sender, RoutedEventArgs e)
        {
            RestartGame();
            enemyLocationPicker();
        }
    }
}
