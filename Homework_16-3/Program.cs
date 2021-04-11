using System;
using System.Management;
using System.Threading.Tasks;

namespace Homework_16_3
{
    class Program
    {
        #region HW 16-3
        // Написать программу перемножения матриц используя многопоточность
        #endregion

        public static Random rnd = new();

        static void Main(string[] args)
        {
            int x1 = 3000;
            int y1 = 2000;
            int x2 = 2000;
            int y2 = 3000;
            int[,] matrix1 = new int[y1, x1];
            int[,] matrix2 = new int[y2, x2];
            int[,] matrix3 = new int[y1, x2];
            int cycle = 0;

            int iteration = y1 < y2 ? y1 : y2;

            // 1st matrix
            for (int i = 0; i < y1; i++)
            {
                for (int k = 0; k < x1; k++)
                {
                    matrix1[i, k] = rnd.Next(1, 6);
                }
            }

            // 2nd matrix
            for (int i = 0; i < y2; i++)
            {
                for (int k = 0; k < x2; k++)
                {
                    matrix2[i, k] = rnd.Next(1, 6);
                }
            }

            // Result matrix

            // 1 thread mode
            DateTime startTime = DateTime.Now;

            while (iteration != 0)
            {
                CalcMatrix(0);
                iteration--;
            }

            TimeSpan span = DateTime.Now.Subtract(startTime);
            Console.WriteLine($"Execution time in 1 thread mode: {span.TotalSeconds} s");         // 187s

            // Multi thread mode
            cycle = 0;
            iteration = y1 < y2 ? y1 : y2;

            startTime = DateTime.Now;

            Parallel.For(0, iteration, CalcMatrix);

            TimeSpan span2 = DateTime.Now.Subtract(startTime);
            Console.WriteLine($"Execution time in multi thread mode: {span2.TotalSeconds} s");     // 41s

            Console.ReadLine();

            void CalcMatrix(int x)
            {
                for (int k = 0; k < x1 && k < x2; k++)
                {
                    for (int j = 0; j < x1; j++)
                    {
                        matrix3[cycle, k] += matrix1[k, j] * matrix2[j, cycle];
                    }
                }
                cycle++;
            }
        }
    }
}
