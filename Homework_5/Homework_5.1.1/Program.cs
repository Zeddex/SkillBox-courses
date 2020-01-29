using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_005
{
    class Program
    {
        /// <summary>
        /// Метод, возвращающий матрицу, умноженную на число
        /// </summary>
        /// <param name="matrix">Двумерный массив-матрица</param>
        /// <param name="x">Количество столбцов матрицы</param>
        /// <param name="y">Количество строк матрицы</param>
        /// <param name="m">Множитель</param>
        /// <returns></returns>
        static int[,] MatrixMult(int[,] matrix, int x, int y, int m)
        {
            int n;
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    n = matrix[i, j];
                    n *= m;
                    matrix[i, j] = n;
                }
            }
            return matrix;
        }
        /// <summary>
        /// Метод возвращающий матрицу, умноженную на число (версия 2 - без доп параметров строк/столбцов)
        /// </summary>
        /// <param name="matrix">Двумерный массив-матрица</param>
        /// <param name="m">Множитель</param>
        /// <returns></returns>
        static int[,] MatrixMultV2(int[,] matrix, int m)
        {
            int n;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    n = matrix[i, j];
                    n *= m;
                    matrix[i, j] = n;
                }
            }
            return matrix;
        }
        static void Main(string[] args)
        {
            // Задание 1.
            // Воспользовавшись решением задания 3 четвертого модуля
            // 1.1. Создать метод, принимающий число и матрицу, возвращающий матрицу умноженную на число
            //
            // Весь код должен быть откомментирован

            #region Умножение матрицы на число (исходник)
            //int x, y, m;
            //// Проверка введённых параметров на положительное число
            //do
            //{
            //    Console.WriteLine("Введите количество строк матрицы: ");
            //    y = int.Parse(Console.ReadLine());
            //    if (y <= 0)
            //    {
            //        Console.WriteLine("Неверное значение\n");
            //    }
            //} while (y <= 0);

            //do
            //{
            //    Console.WriteLine("Введите количество столбцов матрицы: ");
            //    x = int.Parse(Console.ReadLine());
            //    if (x <= 0)
            //    {
            //        Console.WriteLine("Неверное значение\n");
            //    }
            //} while (x <= 0);

            //do
            //{
            //    Console.WriteLine("Введите множитель: ");
            //    m = int.Parse(Console.ReadLine());
            //    if (m <= 0)
            //    {
            //        Console.WriteLine("Неверное значение\n");
            //    }
            //} while (m <= 0);


            //Console.Clear();

            //Random rand = new Random();

            //int[,] matrix = new int[y, x];

            //Console.WriteLine($"{m} x \n");

            //// Вывод первого массива-матрицы
            //for (int i = 0; i < y; i++)
            //{
            //    Console.Write("|");
            //    for (int j = 0; j < x; j++)
            //    {
            //        matrix[i, j] = rand.Next(50);
            //        Console.Write($"{matrix[i, j],5} ");
            //    }
            //    Console.WriteLine("|");
            //}

            //Console.WriteLine("\n = \n");

            //int n;

            //// Вывод второго массива-матрицы и умножение всех элементов на множитель
            //for (int i = 0; i < y; i++)
            //{
            //    Console.Write("|");
            //    for (int j = 0; j < x; j++)
            //    {
            //        n = matrix[i, j];
            //        n *= m;
            //        matrix[i, j] = n;
            //        Console.Write($"{matrix[i, j],5} ");
            //    }
            //    Console.WriteLine("|");
            //}
            //Console.ReadLine();
            #endregion

            int x, y, m;
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

            do
            {
                Console.WriteLine("Введите множитель: ");
                m = int.Parse(Console.ReadLine());
                if (m <= 0)
                {
                    Console.WriteLine("Неверное значение\n");
                }
            } while (m <= 0);

            Console.Clear();

            Random rand = new Random();

            int[,] matrix = new int[y, x];

            Console.WriteLine($"{m} x \n");

            // Вывод исходной матрицы
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

            Console.WriteLine("\n = \n");

            // Вызов метода умножения матрицы на число
            //MatrixMult(matrix,x,y,m);

            // Вызов метода умножения матрицы на число(v2)
            MatrixMultV2(matrix,m);

            // Вывод результирующей матрицы
            for (int i = 0; i < y; i++)
            {
                Console.Write("|");
                for (int j = 0; j < x; j++)
                {
                    Console.Write($"{matrix[i, j],5} ");
                }
                Console.WriteLine("|");
            }
            Console.ReadLine();
        }
    }
}
