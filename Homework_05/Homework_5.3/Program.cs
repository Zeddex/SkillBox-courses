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
        /// Метод удаляющий повторяющиеся буквы в тексте
        /// </summary>
        /// <param name="text">Текст для обработки</param>
        static void TextCut(string text)
        {
            char check;
            check = text[0];
            for (int i = 0; i < text.Length; i++)               // Перебор букв в тексте
            {
                if (i == 0)                                     // Первый символ выводим без проверки
                {
                    Console.Write($"{text[i]}");
                }
                else if (text[i] != check)
                {
                    Console.Write($"{text[i]}");
                    check = text[i];
                }               
            }
        }
        static void Main(string[] args)
        {
            // Задание 3. Создать метод, принимающий текст. 
            // Результатом работы метода должен быть новый текст, в котором
            // удалены все кратные рядом стоящие символы, оставив по одному 
            // Пример: ПППОООГГГООООДДДААА >>> ПОГОДА
            // Пример: Ххххоооорррооошшшиий деееннннь >>> хороший день
            //
            // Весь код должен быть откоммментирован

            Console.WriteLine("Введите текст:");
            string inputText = Console.ReadLine();

            TextCut(inputText);

            Console.ReadLine();
        }
    }
}
