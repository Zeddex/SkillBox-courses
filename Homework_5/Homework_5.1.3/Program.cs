using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_005
{
    class Program
    {
        static int[,] MultMatrix(int[,] matrix1, int[,] matrix2)
        {
            int[,] matrix3 = new int[matrix1.GetLength(0), matrix2.GetLength(1)];

            for (int i = 0; i < matrix2.GetLength(0) && i < matrix1.GetLength(0); i++)
            {
                for (int k = 0; k < matrix1.GetLength(1) && k < matrix2.GetLength(1); k++)
                {
                    for (int j = 0; j < matrix1.GetLength(1); j++)
                    {
                        matrix3[i, k] += matrix1[k, j] * matrix2[j, i];
                    }
                }
            }
            return matrix3;
        }

        static void Main(string[] args)
        {
            // Задание 1.
            // Воспользовавшись решением задания 3 четвертого модуля
            // 1.3. Создать метод, принимающий две матрицы, возвращающий их произведение
            //
            // Весь код должен быть откомментирован

            #region Перемножение матриц (исходник)
            //int x1, y1, x2, y2;

            ////  Проверка введённых параметров на положительное число и соответствие матриц друг другу
            //while (true)
            //{
            //    // Ввод данных 1й матрицы
            //    do
            //    {
            //        Console.WriteLine("Введите количество строк 1й матрицы: ");
            //        y1 = int.Parse(Console.ReadLine());
            //        if (y1 <= 0)
            //        {
            //            Console.WriteLine("Неверное значение\n");
            //        }
            //    } while (y1 <= 0);

            //    do
            //    {
            //        Console.WriteLine("Введите количество столбцов 1й матрицы: ");
            //        x1 = int.Parse(Console.ReadLine());
            //        if (x1 <= 0)
            //        {
            //            Console.WriteLine("Неверное значение\n");
            //        }
            //    } while (x1 <= 0);

            //    // Ввод данных 2й матрицы
            //    do
            //    {
            //        Console.WriteLine("Введите количество строк 2й матрицы: ");
            //        y2 = int.Parse(Console.ReadLine());
            //        if (y2 <= 0)
            //        {
            //            Console.WriteLine("Неверное значение\n");
            //        }
            //    } while (y2 <= 0);

            //    do
            //    {
            //        Console.WriteLine("Введите количество столбцов 2й матрицы: ");
            //        x2 = int.Parse(Console.ReadLine());
            //        if (x2 <= 0)
            //        {
            //            Console.WriteLine("Неверное значение\n");
            //        }
            //    } while (x2 <= 0);

            //    if (x1 != y2)
            //    {
            //        Console.WriteLine("Матрицы с текущими параметрами не могут быть перемножены!");
            //        Console.ReadLine();
            //        Console.Clear();
            //        continue;
            //    }
            //    break;
            //}

            //Console.Clear();

            //Random rand = new Random();

            ////  Создание и вывод первого массива-матрицы
            //int[,] matrix1 = new int[y1, x1];
            //for (int i = 0; i < y1; i++)
            //{
            //    Console.Write("|");
            //    for (int k = 0; k < x1; k++)
            //    {
            //        matrix1[i, k] = rand.Next(1, 6);
            //        Console.Write($"{matrix1[i, k],5} ");
            //    }
            //    Console.WriteLine("|");
            //}

            //Console.WriteLine("\n х \n");

            //// Создание и вывод второго массива-матрицы
            //int[,] matrix2 = new int[y2, x2];

            //for (int i = 0; i < y2; i++)
            //{
            //    Console.Write("|");
            //    for (int k = 0; k < x2; k++)
            //    {
            //        matrix2[i, k] = rand.Next(1, 6);
            //        Console.Write($"{matrix2[i, k],5} ");
            //    }
            //    Console.WriteLine("|");
            //}

            //Console.WriteLine("\n = \n");


            //// Создание третьего результирующего массива-матрицы
            //int[,] matrix3 = new int[y1, x2];

            //for (int i = 0; i < y2 && i < y1; i++)
            //{
            //    Console.Write("|");
            //    for (int k = 0; k < x1 && k < x2; k++)
            //    {
            //        for (int j = 0; j < x1; j++)
            //        {
            //            matrix3[i, k] += matrix1[k, j] * matrix2[j, i];
            //        }
            //        Console.Write($"{matrix3[i, k],5} ");
            //    }
            //    Console.WriteLine("|");
            //}
            //Console.ReadLine();
            #endregion

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


            // Вывод третьего результирующего массива-матрицы
            int[,] matrix3 = MultMatrix(matrix1, matrix2);

            for (int i = 0; i < matrix3.GetLength(0); i++)
            {
                Console.Write("|");
                for (int k = 0; k < matrix3.GetLength(1); k++)
                {
                    Console.Write($"{matrix3[i, k],5} ");
                }
                Console.WriteLine("|");
            }
            Console.ReadLine();

        }
    }
}
