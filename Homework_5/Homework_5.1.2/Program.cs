using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_005
{
    class Program
    {
        static int[,] MatrixSum(int[,] matrix1, int[,] matrix2)
        {
            int add, add2, add3;
            int[,] matrix3 = new int[matrix1.GetLength(0), matrix1.GetLength(1)];
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    add = matrix1[i, j];
                    add2 = matrix2[i, j];
                    add3 = add + add2;
                    matrix3[i, j] = add3;
                }
            }
            return matrix3;
        }
        static void Main(string[] args)
        {
            // Задание 1.
            // 1.2. Создать метод, принимающий две матрицы, возвращающий их сумму
            //
            // Весь код должен быть откомментирован

            #region Сложение и вычитание матриц (исходник)
            //int x, y;
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


            //Console.Clear();

            //Random rand = new Random();

            //int[,] matrix = new int[y, x];

            //Console.CursorLeft = 30;
            //Console.Write("Сложение матриц\n");
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

            //Console.WriteLine("\n + \n");

            //// Создание и вывод второго массива-матрицы
            //int[,] matrix2 = new int[y, x];

            //for (int i = 0; i < y; i++)
            //{
            //    Console.Write("|");
            //    for (int j = 0; j < x; j++)
            //    {
            //        matrix2[i, j] = rand.Next(50);
            //        Console.Write($"{matrix2[i, j],5} ");
            //    }
            //    Console.WriteLine("|");
            //}

            //Console.WriteLine("\n = \n");

            //// Создание третьего результирующего по сложению массива-матрицы
            //int add, add2, add3, sub, sub2, sub3;
            //int[,] matrix3 = new int[y, x];

            //for (int i = 0; i < y; i++)
            //{
            //    Console.Write("|");
            //    for (int j = 0; j < x; j++)
            //    {
            //        add = matrix[i, j];
            //        add2 = matrix2[i, j];
            //        add3 = add + add2;
            //        matrix3[i, j] = add3;
            //        Console.Write($"{matrix3[i, j],5} ");
            //    }
            //    Console.WriteLine("|");
            //}

            //Console.WriteLine("");
            //Console.CursorLeft = 30;
            //Console.Write("Вычитание матриц\n");

            //// Вывод первого массива-матрицы
            //for (int i = 0; i < y; i++)
            //{
            //    Console.Write("|");
            //    for (int j = 0; j < x; j++)
            //    {
            //        Console.Write($"{matrix[i, j],5} ");
            //    }
            //    Console.WriteLine("|");
            //}

            //Console.WriteLine("\n - \n");

            //// Вывод второго массива-матрицы
            //for (int i = 0; i < y; i++)
            //{
            //    Console.Write("|");
            //    for (int j = 0; j < x; j++)
            //    {
            //        Console.Write($"{matrix2[i, j],5} ");
            //    }
            //    Console.WriteLine("|");
            //}

            //Console.WriteLine("\n = \n");


            //// Создание четвёртого результирующего по вычитанию массива-матрицы
            //for (int i = 0; i < y; i++)
            //{
            //    Console.Write("|");
            //    for (int j = 0; j < x; j++)
            //    {
            //        sub = matrix[i, j];
            //        sub2 = matrix2[i, j];
            //        sub3 = sub - sub2;
            //        matrix3[i, j] = sub3;
            //        Console.Write($"{matrix3[i, j],5} ");
            //    }
            //    Console.WriteLine("|");
            //}
            //Console.ReadLine();
            #endregion

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

            Console.CursorLeft = 30;
            Console.Write("Сложение матриц\n");
            // Вывод первого массива-матрицы
            for (int i = 0; i < y; i++)
            {
                Console.Write("|");
                for (int j = 0; j < x; j++)
                {
                    matrix1[i, j] = rand.Next(50);
                    Console.Write($"{matrix1[i, j],5} ");
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

            // Вывод третьего результирующего по сложению массива-матрицы
            int[,] matrix3 = MatrixSum(matrix1, matrix2);

            for (int i = 0; i < y; i++)
            {
                Console.Write("|");
                for (int j = 0; j < x; j++)
                {
                    Console.Write($"{matrix3[i, j],5} ");
                }
                Console.WriteLine("|");
            }

            Console.ReadLine();


        }
    }
}
