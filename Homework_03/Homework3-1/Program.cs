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

            // Написать игру, в которою могут играть два игрока.
            // При старте, игрокам предлагается ввести свои никнеймы.
            // Никнеймы хранятся до конца игры.
            for (; ; )
            {
                Console.Clear();
                Console.WriteLine("Введите имя игрока 1: ");
                string player1 = Console.ReadLine();
                Console.WriteLine("Введите имя игрока 2: ");
                string player2 = Console.ReadLine();
                Console.Clear();

                // Программа загадывает случайное число gameNumber от 12 до 120 сообщая это число игрокам.

                Random rnd = new Random();
                int gameNumber = rnd.Next(12, 121);
                Console.WriteLine($"\nЗагаданное число: {gameNumber}");

                // Игроки ходят по очереди(игра сообщает о ходе текущего игрока)
                // Игрок, ход которого указан вводит число userTry, которое может принимать значения 1, 2, 3 или 4,
                // введенное число вычитается из gameNumber
                // Новое значение gameNumber показывается игрокам на экране.

                for (; ; )
                {
                    Console.WriteLine($"\nХодит {player1}, введи число от 1 до 4");
                    int userTry = Convert.ToInt32(Console.ReadLine());
                    gameNumber -= userTry;
                    Console.WriteLine($"\nЧисло: {gameNumber}");

                   if (gameNumber <= 0)
                   {
                        Console.WriteLine($"\nПобеда! Выиграл игрок {player1}");
                        Console.ReadKey();
                        break;
                   }

                    Console.WriteLine($"\nХодит {player2}, введи число от 1 до 4");
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

                // Игра поздравляет победителя, предлагая сыграть реванш

                Console.Clear();
                Console.WriteLine("Хотите сыграть ещё раз? (1 - да / 0 - нет)");
                int newGame = Convert.ToInt32(Console.ReadLine());
                if (newGame != 1) break;
            }
        }
    }
}
