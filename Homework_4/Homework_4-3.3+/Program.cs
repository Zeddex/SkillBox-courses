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
            //  |  1  3  5  |   |  1  3  4  |   | 22  48  57  |
            //  |  4  5  7  | х |  2  5  6  | = | 35  79  95  |
            //  |  5  3  1  |   |  3  6  7  |   | 14  36  45  |
            //
            //  
            //                  | 4 |   
            //  |  1  2  3  | х | 5 | = | 32 |
            //                  | 6 |  
            //

            int x, y;

            // Проверка введённых параметров на положительное число
            do
            {
                Console.WriteLine("Введите количество строк матрицы: ");
                y = int.Parse(Console.ReadLine());
                if (y <= 0)
                {
                    Console.WriteLine("Неверное значение\n");
                }
            } while (y <= 0);

            do
            {
                Console.WriteLine("Введите количество столбцов матрицы: ");
                x = int.Parse(Console.ReadLine());
                if (x <= 0)
                {
                    Console.WriteLine("Неверное значение\n");
                }
            } while (x <= 0);


            Console.Clear();

            Random rand = new Random();

            int[,] matrix1 = new int[y, x];

            // Вывод первого массива-матрицы
            for (int i = 0; i < y; i++)
            {
                Console.Write("|");
                for (int k = 0; k < x; k++)
                {
                    matrix1[i, k] = rand.Next(1, 10);
                    Console.Write($"{matrix1[i, k],5} ");
                }
                Console.WriteLine("|");
            }

            Console.WriteLine("\n х \n");

            // Создание и вывод второго массива-матрицы
            int[,] matrix2 = new int[y, x];

            for (int i = 0; i < y; i++)
            {
                Console.Write("|");
                for (int k = 0; k < x; k++)
                {
                    matrix2[i, k] = rand.Next(1, 10);
                    Console.Write($"{matrix2[i, k],5} ");
                }
                Console.WriteLine("|");
            }

            Console.WriteLine("\n = \n");

            // Создание третьего результирующего массива-матрицы
            int[,] matrix3 = Mult(matrix1, matrix2);

            for (int i = 0; i < y; i++)
            {
                Console.Write("|");
                for (int k = 0; k < x; k++)
                {
                    Console.Write($"{matrix3[i, k],5} ");
                }
                Console.WriteLine("|");
            }

            Console.ReadLine();
        }
        static int[,] Mult(int[,] a, int[,] b)
        {
            int[,] r = new int[a.Length, b.Length];
            for (int i = 0; i < b.GetLength(1); i++)
            {
                for (int j = 0; j < b.GetLength(0); j++)
                {
                    r[i, j] = 0;
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }
    }
}
