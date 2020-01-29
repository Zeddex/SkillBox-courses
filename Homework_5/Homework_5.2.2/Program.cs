using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_005
{
    class Program
    {
        static string[] MaxWord(string text)
        {
            string[] wordFromString = text.Split(new Char[] {' ',',','.'}, StringSplitOptions.RemoveEmptyEntries);       // Разделение строки на слова и добавление доп разделителей
            string[] words = new string[wordFromString.Length];
            int letterCountChk = 0;
            int maxLetters;
            string maxWord = wordFromString[0];
            int letterCount;

            Console.WriteLine("");


            // Перебор слов в строке
            for (int i = 0; i < wordFromString.Length; i++)
            {
                letterCount = 0;
                //Console.WriteLine($"Слово: {wordFromString[i]}");

                foreach (var letters in wordFromString[i])          // Перебор количества букв в слове
                {
                    letterCount++;
                }

                if (letterCount > letterCountChk)                   // Проверка на максимальное количество букв
                {
                    letterCountChk = letterCount;
                    maxWord = wordFromString[i];
                }
                //Console.WriteLine($"Количество букв в слове: {letterCount}\n");
            }
            // Поиск слов с таким же количеством букв, как и в самом длинном слове
            words[0] = maxWord;             // Первый элемент массива - самое длинное слово
            maxLetters = letterCountChk;
            int n = 0;

            // Второй круг перебора для нахождения слов, равных по количеству символов максимальному слову
            for (int i = 0; i < wordFromString.Length; i++)
            {
                letterCount = 0;

                foreach (var letters in wordFromString[i])          // Перебор количества букв в слове
                {
                    letterCount++;
                }
                if (letterCount == maxLetters && i > 1)             // Проверка количества букв и соответствие значения максимальному слову
                {
                    words[n+1] = wordFromString[i];
                }
            }
            words = words.Distinct().ToArray();                     // Удаление повторов

            return words;
        }

        static void Main(string[] args)
        {
            // Задание 2.
            // 2.* Создать метод, принимающий  текст и возвращающий слово(слова) с максимальным количеством букв 
            // Примечание: слова в тексте могут быть разделены символами (пробелом, точкой, запятой) 
            // Пример: Текст: "A ББ ВВВ ГГГГ ДДДД  ДД ЕЕ ЖЖ ЗЗЗ"
            // Ответ:
            // ГГГГ, ДДДД
            //
            // Весь код должен быть откоммментирован

            string text;
            Console.WriteLine("Введите текст: ");

            text = Console.ReadLine();

            Console.WriteLine("\nСлово/слова с максимальным количеством букв: "); ;
            foreach (var w in MaxWord(text))
            {
                Console.WriteLine($"{w} ");
            }

            Console.ReadLine();

        }
    }
}
