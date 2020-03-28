using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_Theme_03
{
    class Program
    {
        static void Main(string[] args)
        {

            // * Бонус:
            // В качестве уровня сложности выступает настраиваемое в начале игры изменение диапазона gameNumber 
            // и задание диапазона userTry

            for (; ;)
            {
                Console.Clear();
                Console.WriteLine("Введите имя игрока 1: ");
                string player1 = Console.ReadLine();
                Console.WriteLine("Введите имя игрока 2: ");
                string player2 = Console.ReadLine();
                Console.Clear();

                // Диапазон для gameNumber
                Console.WriteLine("Начальное значение диапазона для игры: ");
                int startNum = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Конечное значение диапазона для игры: ");
                int endNum = Convert.ToInt32(Console.ReadLine());

                // Диапазон для userTry
                Console.WriteLine("Нижнее значение числа-шага: ");
                int stepLow = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Верхнее значение числа-шага: ");
                int stepHigh = Convert.ToInt32(Console.ReadLine());

                Random rnd = new Random();
                int gameNumber = rnd.Next(startNum, endNum+1);
                Console.WriteLine($"\nЗагаданное число: {gameNumber}");

                for (; ;)
                {
                    Console.WriteLine($"\nХодит {player1}, введи число от {stepLow} до {stepHigh}");
                    int userTry = Convert.ToInt32(Console.ReadLine());
                    gameNumber -= userTry;
                    Console.WriteLine($"\nЧисло: {gameNumber}");

                    if (gameNumber <= 0)
                    {
                        Console.WriteLine($"\nПобеда! Выиграл игрок {player1}");
                        Console.ReadKey();
                        break;
                    }

                    Console.WriteLine($"\nХодит {player2}, введи число от {stepLow} до {stepHigh}");
                    userTry = Convert.ToInt32(Console.ReadLine());
                    gameNumber -= userTry;
                    Console.WriteLine($"Число: {gameNumber}");

                    if (gameNumber <= 0)
                    {
                        Console.WriteLine($"\nПобеда! Выиграл игрок {player2}");
                        Console.ReadKey();
                        break;
                    }
                }

                Console.Clear();
                Console.WriteLine("Хотите сыграть ещё раз? (1 - да / 0 - нет)");
                int newGame = Convert.ToInt32(Console.ReadLine());
                if (newGame != 1) break;
            }
        }
    }
}
