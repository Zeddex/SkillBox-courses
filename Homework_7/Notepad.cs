using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Homework_07
{
    struct Notepad
    {
        public List<Content> content;
        string[] titles;                            // Заголовки блокнота

        /// <summary>
        /// Конструктор
        /// </summary>
        public Notepad(params Content[] Args)
        {
            content = Args.ToList();
            titles = new string[]                   // Инициализируем заголовки
            {"Date", "Name", "Surname", "Organisation", "Position"};
        }

        /// <summary>
        /// Метод создания/добавления записей
        /// </summary>
        /// <param name="contentLine">Строка данных для добавления в блокнот</param>
        public void AddLine(Content contentLine)
        {
            content.Add(contentLine);
        }

        /// <summary>
        /// Метод загрузки данных из файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        public void Load(string path)
        {
            //Очищаем список перед загрузкой нового файла
            content.Clear();

            using (StreamReader read = new StreamReader(path))
            {
                while (!read.EndOfStream)
                {
                    string[] arg = read.ReadLine().Split(',');
                    AddLine(new Content(arg[0], arg[1], arg[2], arg[3], arg[4]));
                }
            }
        }

        /// <summary>
        /// Метод добавления строки со своими данными
        /// </summary>
        /// <param name="arg">Данные строки</param>
        public void AddItem(params string[] arg)
        {
            AddLine(new Content(arg[0], arg[1], arg[2], arg[3], arg[4]));
        }

        /// <summary>
        /// Метод вывода данных из базы блокнота
        /// </summary>
        public void PrintDB()
        {
            Console.WriteLine($"{titles[0],10} {titles[1],10} {titles[2],10} {titles[3],15} {titles[4],15}\n");
            for (int i = 0; i < content.Count; i++)
            {
                Console.WriteLine(content[i].Print());
            }
            Console.WriteLine($"\nКоличество записей в списке {content.Count}");
        }

        /// <summary>
        /// Метод удаления записей
        /// </summary>
        /// <param name="line">Номер строки для удаления</param>
        public void Del(int line)
        {
            content.RemoveAt(line-1);
        }

        /// <summary>
        /// Метод сохранения данных в файл
        /// </summary>
        /// <param name="savepath">Путь к файлу для сохранения</param>
        public void Save(string savepath)
        {
            File.Delete(savepath);              // Удаляем старый файл
            string line = String.Format($"{titles[0]},{titles[1]},{titles[2]},{titles[3]},{titles[4]}");        // Записываем заголовки
            File.AppendAllText(savepath, $"{line}\n");

            for (int i = 0; i < content.Count; i++)               // Сохраняем тело файла
            {
                line = String.Format($"{content[i].Date},{content[i].Name},{content[i].Surname},{content[i].Org},{content[i].Position}");
                File.AppendAllText(savepath, $"{line}\n");
            }
        }

        /// <summary>
        /// Метод добавления данных из файла
        /// </summary>
        /// <param name="addfile">Путь к файлу</param>
        public void Merge(string addfile)
        {
            using (StreamReader read = new StreamReader(addfile))
            {
                while (!read.EndOfStream)
                {
                    string[] arg = read.ReadLine().Split(',');
                    AddLine(new Content(arg[0], arg[1], arg[2], arg[3], arg[4]));
                }
            }

        }

        /// <summary>
        /// Метод импортирования записей по выбранному диапазону
        /// </summary>
        /// <param name="date1">Начальная дата для импорта</param>
        /// <param name="date2">Конечная дата для импорта</param>
        /// <param name="importfile">Путь к файлу для импорта данных</param>
        public void Import(string date1, string date2, string importfile)
        {
            DateTime startDate = Convert.ToDateTime(date1);
            DateTime endDate = Convert.ToDateTime(date2);

            using (StreamReader read = new StreamReader(importfile))
            {
                while (!read.EndOfStream)
                {
                    string[] arg = read.ReadLine().Split(',');
                    DateTime arg0 = Convert.ToDateTime(arg[0]);

                    if (arg0 >= startDate && arg0 <= endDate)               // Проверка на заданный диапазон дат
                    {
                        AddLine(new Content(arg[0], arg[1], arg[2], arg[3], arg[4]));
                    }
                }
            }
        }

        /// <summary>
        /// Метод сортировки записей по выбранному полю
        /// </summary>
        /// <param name="line">Номер поля для редактирования</param>
        public void Sort(int line)
        {
            switch (line)
            {
                case 1:
                    content = content.OrderBy(e => e.Date)
                        .ThenBy(e => e.Name)
                        .ThenBy(e => e.Surname)
                        .ThenBy(e => e.Org)
                        .ThenBy(e => e.Position)
                        .ToList();
                    break;

                case 2:
                    content = content.OrderBy(e => e.Name)
                        .ThenBy(e => e.Date)
                        .ThenBy(e => e.Surname)
                        .ThenBy(e => e.Org)
                        .ThenBy(e => e.Position)
                        .ToList();
                    break;

                case 3:
                    content = content.OrderBy(e => e.Surname)
                        .ThenBy(e => e.Name)
                        .ThenBy(e => e.Date)
                        .ThenBy(e => e.Org)
                        .ThenBy(e => e.Position)
                        .ToList();
                    break;

                case 4:
                    content = content.OrderBy(e => e.Org)
                        .ThenBy(e => e.Name)
                        .ThenBy(e => e.Surname)
                        .ThenBy(e => e.Date)
                        .ThenBy(e => e.Position)
                        .ToList();
                    break;

                case 5:
                    content = content.OrderBy(e => e.Position)
                        .ThenBy(e => e.Name)
                        .ThenBy(e => e.Surname)
                        .ThenBy(e => e.Date)
                        .ThenBy(e => e.Org)
                        .ToList();
                    break;


                default:
                    break;
            }

        }

        /// <summary>
        /// Метод редактирования записи
        /// </summary>
        /// <param name="line">Номер строки для редактирования</param>
        public void Edit(int line)
        {
            Content temp = new Content();      // Создание временной переменной
            Console.WriteLine("Введите новую дату: ");
            temp.Date = Console.ReadLine();
            Console.WriteLine("Введите новое имя: ");
            temp.Name = Console.ReadLine();
            Console.WriteLine("Введите новую фамилию: ");
            temp.Surname = Console.ReadLine();
            Console.WriteLine("Введите новую организацию: ");
            temp.Org = Console.ReadLine();
            Console.WriteLine("Введите новую должность: ");
            temp.Position = Console.ReadLine();

            content[line - 1] = temp;         // Перекидываем изменённые данные из переменной назад в список

        }

    }
}
