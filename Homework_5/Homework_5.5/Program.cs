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
        /// Метод, вычисляющий функцию Аккермана
        /// </summary>
        /// <param name="m">Параметр m функции</param>
        /// <param name="n">Параметр n функции</param>
        /// <returns></returns>
        static uint A(uint m, uint n)
        {
            uint a;
            if (m > 0 && n == 0)
            {
                a = (A(m-1, 1));
            }
            else if (m > 0 && n > 0)
            {
                a = A((m-1), A(m, n-1));
            }
            else           // if m = 0
            {
                a = n + 1;
            }

            return a;
        }
        static void Main(string[] args)
        {
            // *Задание 5
            // Вычислить, используя рекурсию, функцию Аккермана:
            // A(2, 5), A(1, 2)
            // A(m, n) = n + 1,                 если m = 0,
            //         = A(m - 1, 1),           если m > 0, n = 0,
            //         = A(m - 1, A(m, n - 1)), если m > 0, n > 0.
            // 
            // Весь код должен быть откомментирован

            Console.WriteLine("Функция Аккермана\n");


            Console.WriteLine("Введите значение m:");
            uint m = uint.Parse(Console.ReadLine());
            Console.WriteLine("\nВведите значение n:");
            uint n = uint.Parse(Console.ReadLine());

            if (m >= 0 && n >= 0)
            {
                Console.WriteLine($"\nA(m,n) = {(A(m, n))}");
            }
            else
            {
                Console.WriteLine("\nВведены неверные значения");
            }

            Console.ReadLine();
        }
    }
}
