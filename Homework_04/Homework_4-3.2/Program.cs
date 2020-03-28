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

            // ** Задание 3.2
            // Заказчику требуется приложение позволяющщее складывать и вычитать математические матрицы
            // Справка https://ru.wikipedia.org/wiki/Матрица_(математика)
            // Справка https://ru.wikipedia.org/wiki/Матрица_(математика)#Сложение_матриц
            // Добавить возможность ввода количество строк и столцов матрицы.
            // Матрицы заполняются автоматически
            // Если по введённым пользователем данным действие произвести невозможно - сообщить об этом
            //
            // Пример
            //  |  1  3  5  |   |  1  3  4  |   |  2   6   9  |
            //  |  4  5  7  | + |  2  5  6  | = |  6  10  13  |
            //  |  5  3  1  |   |  3  6  7  |   |  8   9   8  |
            //  
            //  
            //  |  1  3  5  |   |  1  3  4  |   |  0   0   1  |
            //  |  4  5  7  | - |  2  5  6  | = |  2   0   1  |
            //  |  5  3  1  |   |  3  6  7  |   |  2  -3  -6  |
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

            int[,] matrix = new int[y, x];

            Console.CursorLeft = 30;
            Console.Write("Сложение матриц\n") ;
            // Вывод первого массива-матрицы
            for (int i = 0; i < y; i++)
            {
                Console.Write("|");
                for (int j = 0; j < x; j++)
                {
                    matrix[i, j] = rand.Next(50);
                    Console.Write($"{matrix[i, j],5} ");
                }
                Console.WriteLine("|");
            }

            Console.WriteLine("\n + \n");

            // Создание и вывод второго массива-матрицы
            int[,] matrix2 = new int[y, x];

            for (int i = 0; i < y; i++)
            {
                Console.Write("|");
                for (int j = 0; j < x; j++)
                {
                    matrix2[i, j] = rand.Next(50);
                    Console.Write($"{matrix2[i, j],5} ");
                }
                Console.WriteLine("|");
            }

            Console.WriteLine("\n = \n");

            // Создание третьего результирующего по сложению массива-матрицы
            int add, add2, add3, sub, sub2, sub3;
            int[,] matrix3 = new int[y, x];

            for (int i = 0; i < y; i++)
            {
                Console.Write("|");
                for (int j = 0; j < x; j++)
                {
                    add = matrix[i, j];
                    add2 = matrix2[i, j];
                    add3 = add + add2;
                    matrix3[i, j] = add3;
                    Console.Write($"{matrix3[i, j],5} ");
                }
                Console.WriteLine("|");
            }

            Console.WriteLine("");
            Console.CursorLeft = 30;
            Console.Write("Вычитание матриц\n");

            // Вывод первого массива-матрицы
            for (int i = 0; i < y; i++)
            {
                Console.Write("|");
                for (int j = 0; j < x; j++)
                {
                    Console.Write($"{matrix[i, j],5} ");
                }
                Console.WriteLine("|");
            }

            Console.WriteLine("\n - \n");

            // Вывод второго массива-матрицы
            for (int i = 0; i < y; i++)
            {
                Console.Write("|");
                for (int j = 0; j < x; j++)
                {
                    Console.Write($"{matrix2[i, j],5} ");
                }
                Console.WriteLine("|");
            }

            Console.WriteLine("\n = \n");


            // Создание четвёртого результирующего по вычитанию массива-матрицы
            for (int i = 0; i < y; i++)
            {
                Console.Write("|");
                for (int j = 0; j < x; j++)
                {
                    sub = matrix[i, j];
                    sub2 = matrix2[i, j];
                    sub3 = sub - sub2;
                    matrix3[i, j] = sub3;
                    Console.Write($"{matrix3[i, j],5} ");
                }
                Console.WriteLine("|");
            }
            Console.ReadLine();
        }
    }
}
