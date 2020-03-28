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
            // * Задание 3.1
            // Заказчику требуется приложение позволяющее умножать математическую матрицу на число
            // Справка https://ru.wikipedia.org/wiki/Матрица_(математика)
            // Справка https://ru.wikipedia.org/wiki/Матрица_(математика)#Умножение_матрицы_на_число
            // Добавить возможность ввода количество строк и столбцов матрицы и число,
            // на которое будет производиться умножение.
            // Матрицы заполняются автоматически. 
            // Если по введённым пользователем данным действие произвести невозможно - сообщить об этом
            //
            // Пример
            //
            //      |  1  3  5  |   |  5  15  25  |
            //  5 х |  4  5  7  | = | 20  25  35  |
            //      |  5  3  1  |   | 25  15   5  |
            //
            //

            int x, y, m;

            // Проверка введённых параметров на положительное число
            do
            {
                Console.WriteLine("Введите количество строк матрицы: ");
                y = int.Parse(Console.ReadLine());
                if (y <= 0)
                {
                    Console.WriteLine("Неверное значение\n");
                }
            } while (y <= 0);

            do
            {
                Console.WriteLine("Введите количество столбцов матрицы: ");
                x = int.Parse(Console.ReadLine());
                if (x <= 0)
                {
                    Console.WriteLine("Неверное значение\n");
                }
            } while (x <= 0);

            do
            {
                Console.WriteLine("Введите множитель: ");
                m = int.Parse(Console.ReadLine());
                if (m <= 0)
                {
                    Console.WriteLine("Неверное значение\n");
                }
            } while (m <= 0);    
                

            Console.Clear();

            Random rand = new Random();

            int[,] matrix = new int[y,x];

            Console.WriteLine($"{m} x \n");

            // Вывод первого массива-матрицы
            for (int i = 0; i < y; i++)
            {
                Console.Write("|");
                for (int j = 0; j < x; j++)
                {
                    matrix[i,j] = rand.Next(50);
                    Console.Write($"{matrix[i,j],5} ");
                }
                Console.WriteLine("|");
            }

            Console.WriteLine("\n = \n");

            int n;

            // Вывод второго массива-матрицы и умножение всех элементов на множитель
            for (int i = 0; i < y; i++)
            {
                Console.Write("|");
                for (int j = 0; j < x; j++)
                {
                    n = matrix[i, j];
                    n *= m;
                    matrix[i, j] = n;
                    Console.Write($"{matrix[i, j],5} ");
                }
                Console.WriteLine("|");
            }
            Console.ReadLine();

        }
    }
}
