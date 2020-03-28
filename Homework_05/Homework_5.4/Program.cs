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
        /// Метод, определяющий является ли заданная прогрессия арифметической или геометрической
        /// </summary>
        /// <param name="text">Последовательность чисел для обработки</param>
        static void Progression (string text)
        {
            string[] symbols = text.Split(new Char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);      // Разбираем строку на отдельные элементы/слова и удаляем разделители
            int[] digits = Array.ConvertAll(symbols, int.Parse);                                                // Конвертация массива string в int

            int a = digits[1] - digits[0];          // Находим знаменатель последовательности
            double g = digits[1] / digits[0];          // Находим знаменатель последовательности

            Console.WriteLine("\nЭлементы арифметической прогресии: ");
            for (int i = 0; i < digits.Length; i++)
            {
                if (digits[i] != (digits[0] + i * a))      // Формула арифметической прогресии
                {
                    break;
                }
                Console.Write($"{digits[i]} ");
            }


            Console.WriteLine("\nЭлементы геометрической прогресии: ");
            for (int i = 0; i < digits.Length; i++)
            {
                if (i == 0)
                {
                    Console.Write($"{digits[0]} ");
                    continue;
                }
                else if (digits[i] != (digits[i - 1] * g))           // Формула геометрической прогресии
                {
                    break;
                }
                Console.Write($"{digits[i]} ");
            }

        }

        /// <summary>
        /// Метод, определяющий является ли заданная прогрессия геометрической
        /// </summary>
        /// <param name="data">Последовательность чисел для обработки</param>
        static void Main(string[] args)
        {
            // Задание 4. Написать метод принимающий некоторое количесво чисел, выяснить
            // является заданная последовательность элементами арифметической или геометрической прогрессии
            // 
            // Примечание
            //             http://ru.wikipedia.org/wiki/Арифметическая_прогрессия
            //             http://ru.wikipedia.org/wiki/Геометрическая_прогрессия
            //
            // Весь код должен быть откомментирован


            Console.WriteLine("Введите последовательность чисел: ");
            string n = Console.ReadLine();

            // Вызов метода, сравнивающего прогрессии
            Progression(n);

            Console.ReadLine();
        }
    }
}
