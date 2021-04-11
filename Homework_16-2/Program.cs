using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Management;

namespace Homework_16_2
{
    class Program
    {
        #region HW 16-2

        // Вычислить количество чисел на промежутке 
        // от 1 000 000 000
        // до 2 000 000 000
        // в которых сумма цифр числа кратна последней цифре числа

        // Число        Сумма цифр      Последняя цифра     Кратность
        //    11                 2                    1            Да    
        //    12                 3                    2           Нет     
        //    19                10                    9           Нет      
        //    20                 2                    0            Да      
        //   123                 6                    3            Да       
        //                                                           
        // 
        #endregion

        public static int Calculate(int start, int end)
        {
            int result = 0;
            int sum, currNumb, lastDigit;

            for (int i = start; i <= end; i++)
            {
                sum = 0;
                currNumb = i;

                while (currNumb != 0)
                {
                    sum += currNumb % 10;
                    currNumb /= 10;
                }

                lastDigit = i % 10;

                if (lastDigit == 0 || lastDigit == 1 || sum % lastDigit == 0)
                {
                    result++;
                }
            }
            return result;
        }

        static void Main(string[] args)
        {
            int startNumber = 1_000_000_000;
            int endNumber = 2_000_000_000;
            int result = 0;

            Console.WriteLine("Cores information:");

            int coreCount = 0;
            foreach (var item in new ManagementObjectSearcher("Select * from Win32_Processor").Get())
            {
                coreCount += int.Parse(item["NumberOfCores"].ToString());
            }
            Console.WriteLine($"Physical cores = {coreCount}");

            int threadsCount = Environment.ProcessorCount;
            Console.WriteLine($"Logical cores = {threadsCount}");

            int workingCores = threadsCount - 1;

            int perCore = (endNumber - startNumber) / workingCores;
            Console.WriteLine($"Working cores = {workingCores}\n");

            Console.WriteLine("Counting...");

            var tasks = new Task<int>[workingCores];

            // 1 thread mode
            DateTime startTime = DateTime.Now;
            result = Calculate(startNumber, endNumber);
            TimeSpan span = DateTime.Now.Subtract(startTime);

            Console.WriteLine($"Numbers from {startNumber} to {endNumber}");
            Console.WriteLine($"Amount of multiple numbers: {result}\n");
            Console.WriteLine($"Execution time in 1 thread mode: {span.TotalSeconds} s\n");        // 1b - 118s, 100mln - 12s

            // multi thread mode
            Console.WriteLine("Multi thread mode:");
            result = 0;
            int fromNumb, toNumb = 0;

            startTime = DateTime.Now;

            for (int i = 0; i < workingCores; i++)
            {
                if (i == 0)
                    fromNumb = startNumber;
                else
                    fromNumb = startNumber + 1;

                if (i == workingCores - 1)
                    toNumb = endNumber;
                else
                    toNumb = startNumber + perCore;

                Console.WriteLine($"Process: {i + 1}. Numbers from {fromNumb} to {toNumb}");

                int numbA = fromNumb;
                int numbB = toNumb;
                tasks[i] = Task<int>.Run(() => Calculate(numbA, numbB));

                startNumber += perCore;
            }

            Task.WaitAll(tasks);

            foreach (var task in tasks)
            {
                result += task.Result;
            }

            TimeSpan span2 = DateTime.Now.Subtract(startTime);

            Console.WriteLine($"\nAmount of multiple numbers: {result}\n");
            Console.WriteLine($"Execution time in multi thread mode: {span2.TotalSeconds} s");    // 1b - 20s, 100mln - 2s

            Console.ReadLine();
        }
    }
}
