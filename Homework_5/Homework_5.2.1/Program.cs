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
        /// Метод, возвращающий слово, содержащее минимальное количество букв
        /// </summary>
        /// <param name="text">Текст для обработки</param>
        /// <returns></returns>
        static string MinWord(string text)
        {
            string[] values = text.Split();             // Разделение строки на слова
            string word = values[0];
            int letterCountChk = 100;                   // Максимальное количество букв в слове
            int letterCount;

            Console.WriteLine("");

            // Перебор слов в строке
            for (int i = 0; i < values.Length; i++)
            {
                letterCount = 0;
                //Console.WriteLine($"Слово: {values[i]}");

                foreach (var letters in values[i])          //Перебор количества букв в слове
                {
                    letterCount++;
                }

                if (letterCount < letterCountChk)
                {
                    letterCountChk = letterCount;
                    word = values[i];
                }
                //Console.WriteLine($"Количество букв в слове: {letterCount}\n");
            }
            //Console.WriteLine($"Минимальное количество букв в слове '{word}' - {letterCountChk} шт");
            return word;
        }
        static void Main(string[] args)
        {
            // Задание 2.
            // 1. Создать метод, принимающий  текст и возвращающий слово, содержащее минимальное количество букв
            //
            // Весь код должен быть откоммментирован

            string text;
            Console.WriteLine("Введите текст: ");

            text = Console.ReadLine();

            Console.WriteLine("\nСлово с минимальным количеством букв: " + MinWord(text));
            Console.ReadLine();
        }
    }
}
