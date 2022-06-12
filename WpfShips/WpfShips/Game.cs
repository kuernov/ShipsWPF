using System;

namespace WpfShips
{
    class Game
    {

        static Random rnd = new Random();
        int quadrupleShips = 4;
        int tripleShips = 3;
        int doubleShips = 2;
        int singleShips = 1;

        public GameState gameState { get; set; } = GameState.PICKING;

        ButtonCondition[,] playerBoardCondition = new ButtonCondition[8, 8];
        ButtonCondition[,] computerBoardCondition = new ButtonCondition[8, 8];

        public Game()
        {
            for (int i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    playerBoardCondition[i, j] = new ButtonCondition();
                    computerBoardCondition[i, j] = new ButtonCondition();
                }
            }
        }

        public ButtonCondition getPlayerAtCoordinates(int x, int y)
        {
            return playerBoardCondition[x, y];
        }
        public ButtonCondition getComputerAtCoordinates(int x, int y)
        {
            return computerBoardCondition[x, y];
        }

        internal void Pick(Coords coords)
        {
            var boardCondition = playerBoardCondition[coords.y, coords.x];
            if (boardCondition.Occupied)
            {
                return;
            }
            if (quadrupleShips > 0)
            {
                quadrupleShips--;
                boardCondition.Occupied = true;
                boardCondition.TypeOfShip = 4;
            }
            else if (tripleShips > 0)
            {
                tripleShips--;
                boardCondition.Occupied = true;
                boardCondition.TypeOfShip = 3;
            }
            else if (doubleShips > 0)
            {
                doubleShips--;
                boardCondition.Occupied = true;
                boardCondition.TypeOfShip = 2;
            }
            else if (singleShips > 0)
            {
                singleShips--;
                boardCondition.Occupied = true;
                boardCondition.TypeOfShip = 1;
            }

            if (quadrupleShips == 0 && tripleShips == 0 && doubleShips == 0 && singleShips == 0)
            {
                GenerateComputer();
                gameState = GameState.PLAYING;
            }
        }

        public void RandomShip(int n)
        {
            ButtonCondition[] checkArray = new ButtonCondition[n];
            int iterator = 0;
            int x = rnd.Next(0, 8);
            int y = rnd.Next(0, 8);
            int XorY = rnd.Next(1, 3);
            if (computerBoardCondition[y, x].Occupied == true)
            {
                RandomShip(n);
            }
            else if (n == 1)
            {
                computerBoardCondition[y, x].TypeOfShip = 1;
                computerBoardCondition[y, x].Occupied = true;
            }
            else
            {
                checkArray[iterator] = computerBoardCondition[y, x];
                iterator++;
                if (XorY == 1 && x > 0 && x < 7 && (computerBoardCondition[y, x + 1].Occupied == false || computerBoardCondition[y, x - 1].Occupied == false))
                {
                    int left = x;
                    int right = x;
                    while (iterator < n)
                    {
                        if (right < 7 && computerBoardCondition[y, x + 1].Occupied == false)
                        {
                            right++;
                            checkArray[iterator] = computerBoardCondition[y, right];
                            iterator++;
                        }
                        else if (left > 0 && computerBoardCondition[y, x - 1].Occupied == false)
                        {
                            left--;
                            checkArray[iterator] = computerBoardCondition[y, left];
                            iterator++;
                        }
                        else
                        {
                            RandomShip(n);
                        }

                    }
                }
                else if (XorY == 2 && y < 7 && y > 0 && (computerBoardCondition[y + 1, x].Occupied == false || computerBoardCondition[y - 1, x].Occupied == false))
                {
                    int up = y;
                    int down = y;
                    while (iterator < n)
                    {
                        if (down < 7 && computerBoardCondition[y + 1, x].Occupied == false)
                        {
                            down++;
                            checkArray[iterator] = computerBoardCondition[down, x];
                            iterator++;
                        }
                        else if (up > 0 && computerBoardCondition[y - 1, x].Occupied == false)
                        {
                            up--;
                            checkArray[iterator] = computerBoardCondition[up, x];
                            iterator++;
                        }
                        else
                        {
                            RandomShip(n);
                        }
                    }
                }
                else
                {
                    RandomShip(n);
                }
            }
            if (iterator == n && n != 1)
            {
                for (int i = 0; i < n; i++)
                {
                    checkArray[i].Occupied = true;
                    checkArray[i].TypeOfShip = n;
                }
            }
        }


        internal bool PlayerShoot(Coords coords)
        {

            computerBoardCondition[coords.y, coords.x].Hit = true;
        
            IsGameOver(computerBoardCondition);
            if (computerBoardCondition[coords.y, coords.x].Occupied == true)
                return true;
            else
                return false;


            // Check if game is over
            // Update game summary
        }

        internal void ComputerShoot()
        {

            int x = rnd.Next(0, 8);
            int y = rnd.Next(0, 8);
            if (playerBoardCondition[y, x].Hit == true)
            {
                ComputerShoot();
            }
            else if (playerBoardCondition[y, x].Occupied == true)
            {
                playerBoardCondition[y, x].Hit = true;
                ComputerShoot();
                Console.WriteLine($"Computer attacked x: {x}, y: {y}");
            }
            else
            {
                playerBoardCondition[y, x].Hit = true;
                Console.WriteLine($"Computer attacked x: {x}, y: {y}");
            }
            // todo implement me
        }

        private void GenerateComputer()
        {
            RandomShip(4);
            RandomShip(3);
            RandomShip(2);
            RandomShip(1);
        }

        private void IsGameOver(ButtonCondition[,] Array)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Array[i, j].Occupied == true && Array[i, j].Hit == false)
                        return;
                }
            }
            gameState = GameState.GAMEOVER;
        }
    }


    public class ButtonCondition
    {
        public bool Occupied;
        public bool Hit;
        public int TypeOfShip;

        public ButtonCondition()
        {
            Occupied = false;
            Hit = false;
            TypeOfShip = 0;
        }
    }

    public enum GameState
    {
        PICKING, PLAYING, GAMEOVER
    }

}
