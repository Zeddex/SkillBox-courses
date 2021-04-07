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

        public static int result = 0;
        public static object locker = new object();

        static void Main(string[] args)
        {
            int startNumber = 1_000_000_000;
            int endNumber = 2_000_000_000;

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

            var tasks = new Task[workingCores];

            // 1 thread mode
            DateTime startTime = DateTime.Now;
            Calculate(startNumber, endNumber);
            TimeSpan span = DateTime.Now.Subtract(startTime);

            Console.WriteLine($"Numbers from {startNumber} to {endNumber}");
            Console.WriteLine($"Amount of multiple numbers: {result}\n");
            Console.WriteLine($"Execution time in 1 thread mode: {span.TotalSeconds} s\n");        // 1b - 130s, 100mln - 13s

            // multi thread mode
            Console.WriteLine("Multi thread mode:");
            result = 0;
            startTime = DateTime.Now;

            //var outerTask = Task.Run(() =>
            //{
            for (int i = 0; i < workingCores; i++)
            {
                int fromNumb = startNumber;
                int toNumb = startNumber + perCore;

                Console.WriteLine($"Process: {i + 1}. Numbers from {fromNumb} to {toNumb}");
                tasks[i] = Task.Run(() => Calculate(fromNumb, toNumb));

                startNumber += perCore;
            }
            //});
            //outerTask.Wait();

            foreach (var task in tasks)
            {
                task.Wait();
            }

            //Task.WaitAll();

            TimeSpan span2 = DateTime.Now.Subtract(startTime);

            Console.WriteLine($"\nAmount of multiple numbers: {result}\n");
            Console.WriteLine($"Execution time in multi thread mode: {span2.TotalSeconds} s");    // 1b - 69s, 100mln - 7s

            Console.ReadLine();
        }

        public static void Calculate(int start, int end)
        {
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
                //Console.WriteLine($"Number {i}, sum of all digits {sum}, last digit {lastDigit}");

                if (lastDigit == 0 || lastDigit == 1 || sum % lastDigit == 0)
                {
                    lock (locker)
                    {
                        result++;
                    }
                }
            }

        }
    }
}
