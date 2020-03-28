using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_06
{
    class Program
    {
        /// <summary>
        /// Метод, высчитывающий количество групп
        /// </summary>
        /// <param name="n">Конечное число диапазона</param>
        static int GroupsNum(int n)
        {
            int groups = 1;
            int div = n;

            do
            {
                div /= 2;
                groups++;
            } while (div > 1);
            return groups;
        }

        /// <summary>
        /// Метод, заполняющий группы числами по делимости из введённого диапазона
        /// </summary>
        /// <param name="m">Количество групп</param>
        /// <param name="n">Последее значение диапазона</param>
        static int[][] MakeGroups(int n)
        {

            #region если задать количество групп изначально
            //int[][] groupsNumbers = new int[m][];

            //for (int g = 0; g < groupsNumbers.Length; g++)
            //{

            //    for (int i = 1; i <= n; i++)
            //    {

            //        NewGroup = false;
            //        if (i == 1 && g < i)
            //        {
            //            groupsNumbers[g] = new int[0];                                          // Инициализация массива [][x]
            //            Array.Resize(ref groupsNumbers[g], groupsNumbers[g].Length + 1);        // Расширяем массив [][x] на 1 единицу
            //            groupsNumbers[g][groupsNumbers[g].Length - 1] = i;                      // Записываем число в массив
            //        }
            //        else
            //        {
            //            for (int j = 0; j < groupsNumbers[j].Length; j++)
            //            {
            //                AddinGroup = true;
            //                for (int k = 0; k < groupsNumbers[j].Length; k++)
            //                {
            //                    if (i % groupsNumbers[j][k] == 0)
            //                    {
            //                        AddinGroup = false;
            //                        break;
            //                    }
            //                }
            //                if (AddinGroup)
            //                {
            //                    groupsNumbers[j][groupsNumbers[j].Length - 1] = i;
            //                    break;
            //                }
            //                else if (!AddinGroup && j == groupsNumbers[j].Length - 1)
            //                {
            //                    NewGroup = true;
            //                }
            //            }
            //            if (NewGroup)
            //            {
            //                groupsNumbers[g] = new int[0];
            //                Array.Resize(ref groupsNumbers[g], groupsNumbers[g].Length + 1);
            //                groupsNumbers[g][groupsNumbers[g].Length - 1] = i;
            //            }
            //        }
            //    }
            //}
            #endregion

            bool NewGroup;
            bool AddinGroup;
            int[][] groups = new int[0][];

            for (int i = 1; i <= n; i++)
            {
                NewGroup = false;
                if (i == 1 && groups.Length < i)
                {
                    Array.Resize(ref groups, groups.Length + 1);
                    groups[groups.Length - 1] = new int[0];
                    Array.Resize(ref groups[groups.Length - 1], groups[groups.Length - 1].Length + 1);
                    groups[groups.Length - 1][groups[groups.Length - 1].Length - 1] = i;
                }
                else
                {
                    for (int j = 0; j < groups.Length; j++)
                    {
                        AddinGroup = true;
                        for (int k = 0; k < groups[j].Length; k++)
                        {
                            if (i % groups[j][k] == 0)
                            {
                                AddinGroup = false;
                                break;
                            }
                        }
                        if (AddinGroup)
                        {
                            Array.Resize(ref groups[j], groups[j].Length + 1);
                            groups[j][groups[j].Length - 1] = i;
                            break;
                        }
                        else if (!AddinGroup && j == groups.Length - 1)
                        {
                            NewGroup = true;
                        }
                    }

                    if (NewGroup)
                    {
                        Array.Resize(ref groups, groups.Length + 1);
                        groups[groups.Length - 1] = new int[0];
                        Array.Resize(ref groups[groups.Length - 1], groups[groups.Length - 1].Length + 1);
                        groups[groups.Length - 1][groups[groups.Length - 1].Length - 1] = i;
                    }
                }
            }
            return groups;
        }

        /// <summary>
        /// Метод возвращает текущее время
        /// </summary>
        /// <returns></returns>
        static DateTime TimerStart()
        {
            DateTime timenow = DateTime.Now;
            return timenow;
        }

        /// <summary>
        /// Метод возвращающий прошедший отрезок времени
        /// </summary>
        /// <returns></returns>
        static TimeSpan TimerStop(DateTime starttime)
        {
            TimeSpan stopwatch = DateTime.Now.Subtract(starttime);
            return stopwatch;
        }

        /// <summary>
        /// Метод чтения из файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
        static int ReadFile(string path)
        {
            var n = File.ReadAllText(path);
            return int.Parse(n);
        }

        /// <summary>
        /// Метод архивации файла
        /// </summary>
        static void Compress (string sourceFile)
        {
            string compressFile = "groups.zip";

            // Поток для чтения файла
            using (FileStream rf = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                // Поток для записи файла
                using (FileStream wf = File.Create(compressFile))
                {
                    // Поток архивации
                    using (GZipStream cf = new GZipStream(wf, CompressionMode.Compress))
                    {
                        rf.CopyTo(cf);
                    }
                }

            }
            File.Delete(sourceFile);
            Console.WriteLine($"Файл заархивирован");
        }

        /// <summary>
        /// Метод записи в файл
        /// </summary>
        /// <param name="groups">Массив с группами</param>
        static string WriteFile(int[][] groups)
        {
            //// Создаём директорию для файла
            //string path = @"c:\tmp\output";
            //DirectoryInfo dir = new DirectoryInfo(path);
            //if (!dir.Exists)
            //{
            //    dir.Create();
            //}

            string file = "groups.txt";

            // Вывод массива чисел в файл используя класс StreamWriter
            using (StreamWriter swrite = new StreamWriter(file))        // Создание потока для записи в файл
            {
                for (int m = 0; m < groups.Length; m++)
                {
                    swrite.WriteLine($"Группа {m + 1}:");
                    for (int j = 0; j < groups[m].Length; j++)
                    {
                        swrite.Write($"{Convert.ToString(groups[m][j])} ");
                    }
                    swrite.WriteLine("\n");
                }
            }
            return file;
        }

        static void Main(string[] args)
        {
            /// ДЗ
            /// N = 50
            /// 
            /// Группа 1: 1
            /// Группа 2: 2 3 5 7 11 13 17 19 23 29 31 37 41 43 47
            /// Группа 3: 4 6 9 10 14 15 21 22 25 26 33 34 35 38 39 46 49
            /// Группа 4: 8 12 18 20 27 28 30 42 44 45 50
            /// Группа 5: 16 24 36 40
            /// Группа 6: 32 48
            /// 
            /// M = 6
            /// 
            /// ===========
            /// 
            /// 1. Программа считыват из файла (путь к которому можно указать) некоторое N, 
            ///    для которого нужно подсчитать количество групп
            ///    Программа работает с числами N не превосходящими 1 000 000 000
            ///   
            /// 2. В ней есть два режима работы:
            ///   2.1. Первый - в консоли показывается только количество групп, т е значение M
            ///   2.2. Второй - программа получает заполненные группы и записывает их в файл используя один из
            ///                 вариантов работы с файлами
            ///            
            /// 3. После выполения пунктов 2.1 или 2.2 в консоли отображается время, за которое был выдан результат 
            ///    в секундах и миллисекундах
            /// 
            /// 4. После выполнения пунта 2.2 программа предлагает заархивировать данные и если пользователь соглашается -
            /// делает это.
            ///
            /// * При выполнении текущего задания, необходимо документировать код 
            ///   Как пометками, так и xml документацией
            ///   В обязательном порядке создать несколько собственных методов

            Console.WriteLine("Делимость чисел от 1 до N");
            Console.WriteLine("\nУкажите путь к файлу, содержащему число N:");
            string path = Console.ReadLine();
            int n = ReadFile(path);
            Console.WriteLine($"\nЗагруженное число n = {n}");

            Console.WriteLine($"\nВыберите режим работы:");
            Console.WriteLine($"1 - Показать в консоли количество групп для заданного значения");
            Console.WriteLine($"2 - Заполнить все группы и записать их в файл");
            int mode = int.Parse(Console.ReadLine());

            switch (mode)
            {
                case 1:
                    var timestart = TimerStart();                       // Запускаем счётчик

                    // Вызываем метод расчёта количества групп и выводим результат
                    Console.WriteLine($"\nКоличество групп: {GroupsNum(n)}");

                    var stopwatch = TimerStop(timestart);               // Останавливаем счётчик
                    Console.WriteLine($"\nЗатраченное время на выполение {stopwatch.TotalSeconds} сек");
                    Console.Read();

                    break;

                case 2:
                    
                    timestart = TimerStart();                           // Запускаем счётчик

                    int[][] groups = MakeGroups(n);                     // Распределение чисел по группам
                    string file = WriteFile(groups);

                    stopwatch = TimerStop(timestart);                   // Останавливаем счётчик
                    Console.WriteLine($"\n\nЗатраченное время на выполение {stopwatch.TotalSeconds} сек");

                    Console.WriteLine($"\nЗаархивировать данные? (1 - да, 0 - нет)");
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        Compress(file);
                    }
                    else
                    {
                        break;
                    }

                    Console.Read();

                    break;

                default:
                    break;
            }
        }
    }
}
