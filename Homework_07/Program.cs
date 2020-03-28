using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_07
{

    class Program
    {
        static void Main(string[] args)
        {
            #region ДЗ
            /// Разработать ежедневник.
            /// В ежедневнике реализовать возможность 
            /// - создания          +
            /// - удаления          +
            /// - реактирования     +
            /// записей
            /// 
            /// В отдельной записи должно быть не менее пяти полей (дата, имя, фамилия, организация, должность )
            /// 
            /// Реализовать возможность 
            /// - Загрузки данных из файла      +
            /// - Выгрузки данных в файл        +
            /// - Добавления данных в текущий ежедневник из выбранного файла    +
            /// - Импорт записей по выбранному диапазону дат                    +
            /// - Упорядочивания записей ежедневника по выбранному полю         +
            /// 
            #endregion

            string path = "data.csv";               // Путь к файлу с данными для загрузки
            string savepath = "savedata.csv";       // Путь к файлу для сохранения данных
            string addfile = "adddata.csv";         // Путь к файлу с доп данными для добавления в таблицу
            string importfile = "importfile.csv";   // Путь к файлу с данными для импорта
            int line;                               // Переменная номера строки/поля
            string date, date1, date2, name, surname, org, position;

            Notepad notepad = new Notepad(new Content());

            while (true)
            {
                Console.WriteLine("Выберите действие:\n");
                Console.WriteLine("1 - загрузка данных из файла");
                Console.WriteLine("2 - сохранение данных в файл");
                Console.WriteLine("3 - добавление данных в текущий блокнот из файла");
                Console.WriteLine("4 - создание записи");
                Console.WriteLine("5 - удаление записи");
                Console.WriteLine("6 - редактирование записи");
                Console.WriteLine("7 - импорт записей по выбранному диапазону дат");
                Console.WriteLine("8 - сортировка записей по выбранному полю");
                Console.WriteLine("9 - вывод блокнота на экран");
                Console.WriteLine("0 - выход");
                int key = int.Parse(Console.ReadLine());

                switch (key)
                {
                    case 1:
                        notepad.Load(path);
                        Console.Clear();
                        Console.WriteLine("Файл загружен\n\n");
                        notepad.PrintDB();
                        Console.ReadLine();
                        break;

                    case 2:
                        notepad.Save(savepath);
                        Console.Clear();
                        Console.WriteLine("Файл сохранён\n\n");
                        notepad.PrintDB();
                        Console.ReadLine();
                        break;

                    case 3:
                        notepad.Merge(addfile);
                        Console.Clear();
                        Console.WriteLine("Данные добавлены\n\n");
                        notepad.PrintDB();
                        Console.ReadLine();
                        break;

                    case 4:
                        Console.WriteLine("Введите дату(формат dd-mm-yy): ");
                        date = Console.ReadLine();
                        Console.WriteLine("Введите имя: ");
                        name = Console.ReadLine();
                        Console.WriteLine("Введите фамилию: ");
                        surname = Console.ReadLine();
                        Console.WriteLine("Введите организацию: ");
                        org = Console.ReadLine();
                        Console.WriteLine("Введите должность: ");
                        position = Console.ReadLine();
                        notepad.AddItem(date, name, surname, org, position);
                        Console.Clear();
                        Console.WriteLine("Данные добавлены\n\n");
                        notepad.PrintDB();
                        Console.ReadLine();
                        break;

                    case 5:
                        Console.WriteLine("Введите номер строки для удаления");
                        line = int.Parse(Console.ReadLine());
                        notepad.Del(line);
                        Console.Clear();
                        Console.WriteLine("Запись удалена\n\n");
                        notepad.PrintDB();
                        Console.ReadLine();
                        break;

                    case 6:
                        Console.WriteLine("Введите номер строки для редактирования");
                        line = int.Parse(Console.ReadLine());
                        notepad.Edit(line);
                        Console.Clear();
                        Console.WriteLine("Данные отредактированы\n\n");
                        notepad.PrintDB();
                        Console.ReadLine();
                        break;

                    case 7:
                        Console.WriteLine("Введите начальную дату для импорта(формат dd-mm-yy):");
                        date1 = Console.ReadLine();
                        Console.WriteLine("Введите конечную дату для импорта(формат dd-mm-yy):");
                        date2 = Console.ReadLine();
                        notepad.Import(date1, date2, importfile);
                        Console.Clear();
                        Console.WriteLine("Данные импортированы\n\n");
                        notepad.PrintDB();
                        Console.ReadLine();
                        break;

                    case 8:
                        Console.WriteLine("Выберите номер поля, по которому делать сортировку (1-5)");
                        line = int.Parse(Console.ReadLine());
                        notepad.Sort(line);
                        Console.Clear();
                        Console.WriteLine("Данные отсортированы\n\n");
                        notepad.PrintDB();
                        Console.ReadLine();
                        break;

                    case 9:
                        Console.Clear();
                        notepad.PrintDB();
                        Console.ReadLine();
                        break;

                    case 0:
                        return;

                    default:
                        break;
                }
            }
        }
    }
}
