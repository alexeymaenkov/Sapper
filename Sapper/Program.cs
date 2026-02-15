namespace Sapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new();

            int battlefieldSizeX;
            int battlefieldSizeY;
            
            Console.Write("Введите ширину поля (от 10 до 40): ");
            int tempBattlefieldSizeY = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("Введите высоту поля (от 10 до 40): ");
            int tempBattlefieldSizeX = Convert.ToInt32(Console.ReadLine());
            
            if (tempBattlefieldSizeX < 10)
            {
                battlefieldSizeX = 10;
            }
            else if (tempBattlefieldSizeX > 40)
            {
                battlefieldSizeX = 40;
            }
            else
            {
                battlefieldSizeX = tempBattlefieldSizeX;
            }
            
            if (tempBattlefieldSizeY < 10)
            {
                battlefieldSizeY = 10;
            }
            else if (tempBattlefieldSizeY > 40)
            {
                battlefieldSizeY = 40;
            }
            else
            {
                battlefieldSizeY = tempBattlefieldSizeY;
            }
            
            int xMine;
            int yMine;
            int mine = -1;
            int mineQuantity = (battlefieldSizeX * battlefieldSizeY) / 100 * 10;

            int[] aroundX = { -1, -1, -1, 0, 1, 1, 1, 0 };
            int[] aroundY = { -1, 0, 1, 1, 1, 0, -1, -1 };
            int quantityStepsAround = 8;
            
            bool crash;

            string reset = "y";
            
            while (reset == "y")
            {
                int[,] battleField = new int[battlefieldSizeX, battlefieldSizeY];
                
                for (int mineIndex = 0; mineIndex < mineQuantity; mineIndex++)
                {
                    xMine = random.Next(0, battlefieldSizeX);
                    yMine = random.Next(0, battlefieldSizeY);
                    battleField[xMine, yMine] = mine;
                }

                for (int battleFieldX = 0; battleFieldX < battlefieldSizeX; battleFieldX++)
                {
                    for (int battleFieldY = 0; battleFieldY < battlefieldSizeY; battleFieldY++)
                    {
                        if (battleField[battleFieldX, battleFieldY] == mine)
                            continue;

                        int count = 0;

                        for (int stepAround = 0; stepAround < quantityStepsAround; stepAround++)
                        {
                            int newX = battleFieldX + aroundX[stepAround];
                            int newY = battleFieldY + aroundY[stepAround];

                            if (newX >= 0 && newX < battlefieldSizeX && newY >= 0 && newY < battlefieldSizeY)
                            {
                                if (battleField[newX, newY] == mine)
                                {
                                    count++;
                                }
                            }
                        }

                        battleField[battleFieldX, battleFieldY] = count;
                    }
                }
                
                Console.Clear();
                Console.SetCursorPosition(battlefieldSizeY + 4, 0);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Добро пожаловать в игру САПЕР!");
                Console.SetCursorPosition(battlefieldSizeY + 4, 2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"На карте случайным образом размещены {mineQuantity} мин. Найдите их!");
                Console.SetCursorPosition(battlefieldSizeY + 4, 4);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Для перемещения курсора используйте клавиши UP, DOWN, LEFT, RIGHT.");
                Console.SetCursorPosition(battlefieldSizeY + 4, 5);
                Console.WriteLine("Для вскрытия поля - клавишу ENTER.");
                Console.SetCursorPosition(battlefieldSizeY + 4, 6);
                Console.WriteLine("Для того чтобы выделить предполагаемое место нахождения бомбы - клавишу SPACE.");
                Console.SetCursorPosition(battlefieldSizeY + 4, 7);
                Console.WriteLine("Для того чтобы спрятать выделение предполагаемого места нахождения бомбы - клавишу BACKSPACE.");
                Console.ResetColor();

                Console.CursorVisible = false;
                /*
                char[,] map =
                {
                    { '-', '-', '-', '-', '-', '-', '-', '-', '-', '-' },
                    { '|', '*', '*', '*', '*', '*', '*', '*', '*', '|' },
                    { '|', '*', '*', '*', '*', '*', '*', '*', '*', '|' },
                    { '|', '*', '*', '*', '*', '*', '*', '*', '*', '|' },
                    { '|', '*', '*', '*', '*', '*', '*', '*', '*', '|' },
                    { '|', '*', '*', '*', '*', '*', '*', '*', '*', '|' },
                    { '|', '*', '*', '*', '*', '*', '*', '*', '*', '|' },
                    { '|', '*', '*', '*', '*', '*', '*', '*', '*', '|' },
                    { '|', '*', '*', '*', '*', '*', '*', '*', '*', '|' },
                    { '-', '-', '-', '-', '-', '-', '-', '-', '-', '-' },
                };
                */
                
                char[,] map = GetMap(battlefieldSizeX + 2, battlefieldSizeY + 2);
                
                int userX = 1, userY = 1;
                int countMine = 0;
                int countWin = 0;

                crash = true;

                while (crash)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(0, 0);
                    /*
                    for (int i = 0; i < map.GetLength(0); i++)
                    {
                        for (int j = 0; j < map.GetLength(1); j++)
                        {
                            Console.Write(map[i, j]);
                        }
                        Console.WriteLine();
                    }
                    */
                    DrawMap(map);
                    Console.ResetColor();

                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.SetCursorPosition(userY, userX);
                    Console.Write(" ");
                    Console.ResetColor();

                    Console.SetCursorPosition(0, battlefieldSizeX + 3);
                    Console.WriteLine($"Найдено МИН: {countMine}");

                    ConsoleKeyInfo charKey = Console.ReadKey();
                    
                    switch (charKey.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (map[userX - 1, userY] != '#')
                            {
                                userX--;
                            }
                            break;
                        
                        case ConsoleKey.DownArrow:
                            if (map[userX + 1, userY] != '#')
                            {
                                userX++;
                            }
                            break;
                        
                        case ConsoleKey.LeftArrow:
                            if (map[userX, userY - 1] != '#')
                            {
                                userY--;
                            }
                            break;
                        
                        case ConsoleKey.RightArrow:
                            if (map[userX, userY + 1] != '#')
                            {
                                userY++;
                            }
                            break;
                        
                        case ConsoleKey.Backspace:
                            map[userX, userY] = '*';
                            countMine--;
                            break;
                        
                        case ConsoleKey.Spacebar:
                            map[userX, userY] = '?';
                            countMine++;
                            break;
                        
                        case ConsoleKey.Enter:
                            if (map[userX, userY] == '*')
                            {
                                countWin++;
                            }
                            if (countWin == (battlefieldSizeX * battlefieldSizeY) - mineQuantity)
                            {
                                Console.SetCursorPosition(0, battlefieldSizeY + 5);
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.WriteLine("!----------ВЫ ВЫГРАЛИ---------!");
                                crash = false;
                            }
                            if (battleField[userX - 1, userY - 1] == mine)
                            {
                                map[userX, userY] = 'X';
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.SetCursorPosition(userY, userX);
                                Console.Write("X");
                                Console.ResetColor();
                                Console.SetCursorPosition(0, battlefieldSizeY + 5);
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.WriteLine("!----------ВЫ ПРОИГАЛИ---------!");
                                crash = false;
                            }
                            else if (battleField[userX - 1, userY - 1] == 0)
                            {
                                map[userX, userY] = '0';
                            }
                            else if (battleField[userX - 1, userY - 1] == 1)
                            {
                                map[userX, userY] = '1';
                            }
                            else if (battleField[userX - 1, userY - 1] == 2)
                            {
                                map[userX, userY] = '2';
                            }
                            else if (battleField[userX - 1, userY - 1] == 3)
                            {
                                map[userX, userY] = '3';
                            }
                            else if (battleField[userX - 1, userY - 1] == 4)
                            {
                                map[userX, userY] = '4';
                            }
                            else if (battleField[userX - 1, userY - 1] == 5)
                            {
                                map[userX, userY] = '5';
                            }
                            else if (battleField[userX - 1, userY - 1] == 6)
                            {
                                map[userX, userY] = '6';
                            }
                            else if (battleField[userX - 1, userY - 1] == 7)
                            {
                                map[userX, userY] = '7';
                            }
                            else if (battleField[userX - 1, userY - 1] == 8)
                            {
                                map[userX, userY] = '8';
                            }
                            break;
                    }
                }
                
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Хотите попробовать еще раз (y/n): ");
                reset = Convert.ToString(Console.ReadLine());
                Console.ResetColor();
            }
        }

        private static char[,] GetMap(int x, int y)
        {
            char border = '#';
            char field = '*';

            char[,] map = new char[x, y];

            for (int mapX = 0; mapX < map.GetLength(0); mapX++)
            {
                for (int mapY = 0; mapY < map.GetLength(1); mapY++)
                {
                    if ((mapX == 0) || (mapY == 0) || (mapX == x - 1) || (mapY == y - 1))
                    {
                        map[mapX, mapY] = border;
                    }
                    else { map[mapX, mapY] = field; }
                }
            }
            return map;
        }
        private static void DrawMap(char[,] map)
        {
            for (int mapX = 0; mapX < map.GetLength(0); mapX++)
            {
                for (int mapY = 0; mapY < map.GetLength(1); mapY++)
                {
                    Console.Write(map[mapX, mapY]);
                }
                Console.Write("\n");
            }
        }
    }
}
