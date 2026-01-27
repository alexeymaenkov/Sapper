
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Study
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;


            Random random = new Random();

            bool crash = true;

            string reset = "y";

            while (reset == "y")
            {
                Console.Clear();
                Console.SetCursorPosition(0, 13);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Добро пожаловать в игру САПЕР!");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Для перемещения курсора используйте клавиши UP, DOWN, LEFT, RIGHT.");
                Console.WriteLine("Для вскрытия поля - клавишу ENTER.");
                Console.WriteLine("Для того чтобы выделить предполагаемое место нахождения бомбы - клавишу SPACE.");
                Console.WriteLine("Для того чтобы спрятать выделение предполагаемого места нахождения бомбы - клавишу BACKSPACE.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("На карте случайным образом размещены 10 мин. Найдите их!");
                Console.ResetColor();

                int[,] batleField = new int[8, 8] {
                                { 0, 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0, 0 },
                            };

                int xMine;
                int yMine;
                int mine = -1;

                for (int cicle = 0; cicle < 11; cicle++)
                {
                    xMine = random.Next(0, 8);
                    yMine = random.Next(0, 8);
                    batleField[xMine, yMine] = mine;
                }

                int[] dx = { -1, -1, -1, 0, 1, 1, 1, 0 };
                int[] dy = { -1, 0, 1, 1, 1, 0, -1, -1 };

                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        if (batleField[x, y] == -1) continue; // если мина, не считаем

                        int count = 0;

                        for (int d = 0; d < 8; d++)
                        {
                            int newX = x + dx[d];
                            int newY = y + dy[d];

                            // Проверка на границы поля
                            if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8)
                            {
                                if (batleField[newX, newY] == -1)
                                {
                                    count++;
                                }
                            }
                        }

                        batleField[x, y] = count;
                    }
                }

                //----------------------------------------------------------------------------------

                Console.CursorVisible = false;

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

                int userX = 1, userY = 1;

                crash = true;

                int countMine = 0;
                int countWin = 0;

                while (crash == true)
                {


                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(0, 0);
                    for (int i = 0; i < map.GetLength(0); i++)
                    {
                        for (int j = 0; j < map.GetLength(1); j++)
                        {
                            Console.Write(map[i, j]);
                        }
                        Console.WriteLine();
                    }
                    Console.ResetColor();

                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.SetCursorPosition(userY, userX);
                    Console.Write(" ");
                    Console.ResetColor();

                    Console.SetCursorPosition(0, 11);
                    Console.WriteLine($"Найдено МИН: {countMine}");

                    ConsoleKeyInfo charKey = Console.ReadKey();
                    switch (charKey.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (map[userX - 1, userY] != '|' && map[userX - 1, userY] != '-')
                            {
                                userX--;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (map[userX + 1, userY] != '|' && map[userX + 1, userY] != '-')
                            {
                                userX++;
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (map[userX, userY - 1] != '|' && map[userX, userY - 1] != '-')
                            {
                                userY--;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if (map[userX, userY + 1] != '|' && map[userX, userY + 1] != '-')
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
                            if (countWin == 54)
                            {
                                Console.SetCursorPosition(0, 20);
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("!----------ВЫ ВЫГРАЛИ---------!");
                                crash = false;
                            }
                            if (batleField[userX - 1, userY - 1] == -1)
                            {
                                map[userX, userY] = 'X';
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.SetCursorPosition(userY, userX);
                                Console.Write("X");
                                Console.ResetColor();
                                Console.SetCursorPosition(0, 20);
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("!----------ВЫ ПРОИГАЛИ---------!");
                                crash = false;
                            }
                            else if (batleField[userX - 1, userY - 1] == 0)
                            {
                                map[userX, userY] = '0';
                            }
                            else if (batleField[userX - 1, userY - 1] == 1)
                            {
                                map[userX, userY] = '1';
                            }
                            else if (batleField[userX - 1, userY - 1] == 2)
                            {
                                map[userX, userY] = '2';
                            }
                            else if (batleField[userX - 1, userY - 1] == 3)
                            {
                                map[userX, userY] = '3';
                            }
                            else if (batleField[userX - 1, userY - 1] == 4)
                            {
                                map[userX, userY] = '4';
                            }
                            else if (batleField[userX - 1, userY - 1] == 5)
                            {
                                map[userX, userY] = '5';
                            }
                            else if (batleField[userX - 1, userY - 1] == 6)
                            {
                                map[userX, userY] = '6';
                            }
                            else if (batleField[userX - 1, userY - 1] == 7)
                            {
                                map[userX, userY] = '7';
                            }
                            else if (batleField[userX - 1, userY - 1] == 8)
                            {
                                map[userX, userY] = '8';
                            }
                            break;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Хотите попробовать еще раз (y/n): ");
                reset = Convert.ToString(Console.ReadLine());
                Console.ResetColor();
            }
        }
    }
}
