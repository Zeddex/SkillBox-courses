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
            // * Задание 2
            // Заказчику требуется приложение строящее первых N строк треугольника паскаля. N < 25
            // 
            // При N = 9. Треугольник выглядит следующим образом:
            //                                 1
            //                             1       1
            //                         1       2       1
            //                     1       3       3       1
            //                 1       4       6       4       1
            //             1       5      10      10       5       1
            //         1       6      15      20      15       6       1
            //     1       7      21      35      35       21      7       1
            //                                                              
            //                                                              
            // Простое решение:                                                             
            // 1
            // 1       1
            // 1       2       1
            // 1       3       3       1
            // 1       4       6       4       1
            // 1       5      10      10       5       1
            // 1       6      15      20      15       6       1
            // 1       7      21      35      35       21      7       1
            // 
            // Справка: https://ru.wikipedia.org/wiki/Треугольник_Паскаля


            int n = 25;
            Random rand = new Random();


            int[][] triangle = new int[n][];

            // Создаём массивы внутри основного массива
            for (int i = 0; i < triangle.Length; i++)
            {
                triangle[i] = new int[i+1];
            }

            #region Зубчатый массив

            //for (int i = 0; i < triangle.Length; i++)
            //{
            //    for (int j = 0; j < triangle[i].Length; j++)
            //    {
            //        triangle[i][j] = j;
            //        Console.Write($"{triangle[i][j],3} ");
            //    }
            //    Console.WriteLine();
            //}
            #endregion

            for (int i = 0; i < triangle.Length; i++)
            {
                Console.CursorLeft = 50 - i;                    // Отступ курсора
                for (int j = 0; j < triangle[i].Length; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        triangle[i][j] = 1;
                    }
                    else
                    {
                        if (i < 3)
                        {
                            triangle[i][j] = i / j;
                        }
                        else
                        {
                            triangle[i][j] = Convert.ToInt32(factorial(i) / (factorial(j) * factorial(i - j)));
                        }
                    }
                    Console.Write($"{triangle[i][j]} ");
                }
                Console.WriteLine();
            }


            Console.ReadLine();
        }
        public static float factorial(int n)
        {
            float i, x = 1;
            for (i = 1; i <= n; i++)
            {
                x *= i;
            }
            return x;
        }
    }
}
