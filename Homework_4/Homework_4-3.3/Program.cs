using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_Theme_04
{
    class Program
    {
        static void Main(string[] args)
        {
            // *** Задание 3.3
            // Заказчику требуется приложение позволяющее перемножать математические матрицы
            // Справка https://ru.wikipedia.org/wiki/Матрица_(математика)
            // Справка https://ru.wikipedia.org/wiki/Матрица_(математика)#Умножение_матриц
            // Добавить возможность ввода количество строк и столбцов матрицы.
            // Матрицы заполняются автоматически
            // Если по введённым пользователем данным действие произвести нельзя - сообщить об этом
            //  
            //  |  1  3  5  |   |  1  3  4  |   | 22  48  57 |
            //  |  4  5  7  | х |  2  5  6  | = | 35  79  95 |
            //  |  5  3  1  |   |  3  6  7  |   | 14  36  45 |
            //

            //  |  1  3  5  |   |  1  3  |   | x  x |
            //  |  4  5  7  | х |  2  5  | = | x  x |
            //  |  5  3  1  |   |  3  7  |   | x  x |
            //
            //
            //  
            //                  | 4 |   
            //  |  1  2  3  | х | 5 | = | 32 |
            //                  | 6 |  
            //

            int x1, y1, x2, y2;

            //  Проверка введённых параметров на положительное число и соответствие матриц друг другу
            while (true)
            {
                // Ввод данных 1й матрицы
                do
                {
                    Console.WriteLine("Введите количество строк 1й матрицы: ");
                    y1 = int.Parse(Console.ReadLine());
                    if (y1 <= 0)
                    {
                        Console.WriteLine("Неверное значение\n");
                    }
                } while (y1 <= 0);

                do
                {
                    Console.WriteLine("Введите количество столбцов 1й матрицы: ");
                    x1 = int.Parse(Console.ReadLine());
                    if (x1 <= 0)
                    {
                        Console.WriteLine("Неверное значение\n");
                    }
                } while (x1 <= 0);

                // Ввод данных 2й матрицы
                do
                {
                    Console.WriteLine("Введите количество строк 2й матрицы: ");
                    y2 = int.Parse(Console.ReadLine());
                    if (y2 <= 0)
                    {
                        Console.WriteLine("Неверное значение\n");
                    }
                } while (y2 <= 0);

                do
                {
                    Console.WriteLine("Введите количество столбцов 2й матрицы: ");
                    x2 = int.Parse(Console.ReadLine());
                    if (x2 <= 0)
                    {
                        Console.WriteLine("Неверное значение\n");
                    }
                } while (x2 <= 0);

                if (x1 != y2)
                {
                    Console.WriteLine("Матрицы с текущими параметрами не могут быть перемножены!");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }
                break;
            }

            Console.Clear();

            Random rand = new Random();

            //  Создание и вывод первого массива-матрицы
            int[,] matrix1 = new int[y1, x1];
            for (int i = 0; i < y1; i++)
            {
                Console.Write("|");
                for (int k = 0; k < x1; k++)
                {
                    matrix1[i, k] = rand.Next(1, 6);
                    Console.Write($"{matrix1[i, k],5} ");
                }
                Console.WriteLine("|");
            }

            Console.WriteLine("\n х \n");

            // Создание и вывод второго массива-матрицы
            int[,] matrix2 = new int[y2, x2];

            for (int i = 0; i < y2; i++)
            {
                Console.Write("|");
                for (int k = 0; k < x2; k++)
                {
                    matrix2[i, k] = rand.Next(1, 6);
                    Console.Write($"{matrix2[i, k],5} ");
                }
                Console.WriteLine("|");
            }

            Console.WriteLine("\n = \n");


            // Создание третьего результирующего массива-матрицы
            int[,] matrix3 = new int[y1, x2];

            for (int i = 0; i < y2 && i < y1; i++)
            {
                Console.Write("|");
                for (int k = 0; k < x1 && k < x2; k++)
                {
                    for (int j = 0; j < x1; j++)
                    {
                        matrix3[i, k] += matrix1[k, j] * matrix2[j, i];
                    }
                    Console.Write($"{matrix3[i, k],5} ");
                }
                Console.WriteLine("|");
            }
            Console.ReadLine();
        }
    }
}
