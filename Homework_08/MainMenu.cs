using System;
using System.Text;

namespace Homework_08
{
    static class MainMenu
    {
        /// <summary>
        /// Метод проверки ввода цифрового значения
        /// </summary>
        static void CheckInt(ref int input)
        {
            while (!Int32.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Введите верное значение:");
            }
        }

        static void CheckDate(ref DateTime date)
        {
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Введите дату в правильном формате:");
            }
        }

        /// <summary>
        /// Метод вывода главного меню
        /// </summary>
        static public void PrintMenu()
        {
            string pathJson = "data.json";
            string pathXml = "data.xml";
            string savepathJson = "savedata.json";
            string savepathXml = "savedata.xml";
            bool menu = true;
            string title, name, surname, department;
            int age, salary, projects, delnum, line, depnum;
            int input = 0;
            DateTime date = new DateTime(2000, 1, 1);
            Repo rep = new Repo();

            while (true)
            {
                Console.WriteLine("Выберите действие:\n");
                Console.WriteLine("1 - импорт данных");
                Console.WriteLine("2 - экспорт данных");
                Console.WriteLine("3 - добавление записей");
                Console.WriteLine("4 - генерация данных");
                Console.WriteLine("5 - удаление записей");
                Console.WriteLine("6 - редактирование записей");
                Console.WriteLine("7 - сортировка записей");
                Console.WriteLine("8 - вывод базы данных на экран");
                Console.WriteLine("0 - выход");
                CheckInt(ref input);

                switch (input)
                {
                    // Импорт - загрузка данных из файла
                    case 1:
                        menu = true;
                        while (menu)
                        {
                            Import();
                        }
                        break;

                    //Экспорт - сохранение данных в файл
                    case 2:
                        menu = true;
                        while (menu)
                        {
                            Export();
                        }
                        break;

                    // Добавление записей в базу
                    case 3:
                        menu = true;
                        while (menu)
                        {
                            AddData();
                        }
                        break;

                    // Генерация данных
                    case 4:
                        menu = true;
                        while (menu)
                        {
                            GenData();
                        }
                        break;

                    // Удаление записей
                    case 5:
                        menu = true;
                        while (menu)
                        {
                            DelData();
                        }
                        break;

                    // Редактирование записей
                    case 6:
                        menu = true;
                        while (menu)
                        {
                            EditData();
                        }
                        break;

                    // Сортировка записей по выбранному полю
                    case 7:
                        menu = true;
                        while (menu)
                        {
                            SortData();
                        }
                        break;

                    // Вывод базы данных на экран
                    case 8:
                        ShowData();
                        break;

                    case 0:
                        return;

                    default:
                        Console.Clear();
                        break;
                }
            }

            /// <summary>
            /// Метод импорта данных
            /// </summary>
            void Import()
            {
                Console.Clear();
                Console.WriteLine("Выберите формат загружаемого файла:\n");
                Console.WriteLine("1 - Json");
                Console.WriteLine("2 - Xml");
                Console.WriteLine("0 - Назад в основное меню");
                CheckInt(ref input);

                switch (input)
                {
                    case 1:
                        rep.DeserializeJson(pathJson);
                        Console.Clear();
                        rep.PrintDepDB();
                        Console.WriteLine();
                        rep.PrintWorkDB();
                        Console.ReadLine();
                        menu = false;
                        break;

                    case 2:
                        rep.DeserializeXml(pathXml);
                        Console.Clear();
                        rep.PrintDepDB();
                        Console.WriteLine();
                        rep.PrintWorkDB();
                        Console.ReadLine();
                        menu = false;
                        break;

                    case 0:
                        Console.Clear();
                        menu = false;
                        break;

                    default:
                        break;
                }
            }

            /// <summary>
            /// Метод экспорта данных
            /// </summary>
            void Export()
            {
                Console.Clear();
                Console.WriteLine("Выберите формат сохраняемого файла:\n");
                Console.WriteLine("1 - Json");
                Console.WriteLine("2 - Xml");
                Console.WriteLine("0 - Назад в основное меню");
                CheckInt(ref input);

                switch (input)
                {
                    case 1:
                        rep.SerializeJson(savepathJson);
                        Console.Clear();
                        Console.WriteLine("Файл сохранён\n\n");
                        Console.ReadLine();
                        Console.Clear();
                        menu = false;
                        break;

                    case 2:
                        rep.SerializeXml(savepathXml);
                        Console.Clear();
                        Console.WriteLine("Файл сохранён\n\n");
                        Console.ReadLine();
                        Console.Clear();
                        menu = false;
                        break;

                    case 0:
                        Console.Clear();
                        menu = false;
                        break;

                    default:
                        break;
                }
            }

            /// <summary>
            /// Метод добавления данных
            /// </summary>
            void AddData()
            {
                Console.Clear();
                Console.WriteLine("Добавление записей в базу:\n");
                Console.WriteLine("1 - Департаменты");
                Console.WriteLine("2 - Сотрудники");
                Console.WriteLine("0 - Назад в основное меню");
                CheckInt(ref input);

                switch (input)
                {
                    case 1:
                        Console.WriteLine("Введите название департамента: ");
                        title = Console.ReadLine();
                        Console.WriteLine("Введите дату(формат dd.mm.yyyy): ");
                        CheckDate(ref date);
                        rep.AddDep(title, date);
                        Console.Clear();
                        Console.WriteLine("Данные добавлены\n\n");
                        rep.PrintDepDB();
                        Console.WriteLine();
                        rep.PrintWorkDB();
                        Console.ReadLine();
                        menu = false;
                        break;

                    case 2:
                        Console.Clear();

                        if (!rep.CheckDep())
                        {
                            Console.WriteLine("Сначала необходимо создать хотя бы 1 департамент");
                            Console.ReadLine();
                            break;
                        }

                        rep.PrintDepDB();
                        Console.WriteLine();
                        Console.WriteLine("Выберите номер департамента, в который необходимо добавить нового сотрудника:");
                        depnum = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите имя: ");
                        name = Console.ReadLine();
                        Console.WriteLine("Введите фамилию: ");
                        surname = Console.ReadLine();
                        Console.WriteLine("Введите возраст: ");
                        age = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите зарплату: ");
                        salary = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите количество проектов: ");
                        projects = int.Parse(Console.ReadLine());
                        rep.AddWork(name, surname, age, depnum, salary, projects);
                        Console.Clear();
                        Console.WriteLine("Данные добавлены\n\n");
                        rep.PrintDepDB();
                        Console.WriteLine();
                        rep.PrintWorkDB();
                        Console.ReadLine();
                        menu = false;
                        break;

                    case 0:
                        Console.Clear();
                        menu = false;
                        break;

                    default:
                        break;
                }
            }

            /// <summary>
            /// Метод генерации данных
            /// </summary>
            void GenData()
            {
                Console.Clear();
                Console.WriteLine("Генерация данных:\n");
                Console.WriteLine("1 - Департаменты");
                Console.WriteLine("2 - Сотрудники");
                Console.WriteLine("0 - Назад в основное меню");
                CheckInt(ref input);

                switch (input)
                {
                    case 1:
                        Console.WriteLine("Необходимое количество департаментов");
                        rep.FillDep(int.Parse(Console.ReadLine()));
                        Console.Clear();
                        Console.WriteLine("Новые департаменты сгенерированы\n\n");
                        rep.PrintDepDB();
                        Console.ReadLine();
                        break;

                    case 2:
                        Console.WriteLine("Необходимое количество сотрудников");
                        rep.FillWork(int.Parse(Console.ReadLine()));
                        Console.Clear();
                        rep.PrintWorkDB();
                        Console.ReadLine();
                        break;

                    case 0:
                        Console.Clear();
                        menu = false;
                        break;

                    default:
                        break;
                }
            }

            /// <summary>
            /// Метод удаления данных
            /// </summary>
            void DelData()
            {
                Console.Clear();
                Console.WriteLine("Удаление записей:\n");
                Console.WriteLine("1 - Удаление департамента");
                Console.WriteLine("2 - Удаление сотрудника");
                Console.WriteLine("3 - Удаление всех сотрудников из одного департамента");
                Console.WriteLine("4 - Удаление всей базы");
                Console.WriteLine("0 - Назад в основное меню");
                CheckInt(ref input);

                switch (input)
                {
                    case 1:
                        rep.PrintDepDB();
                        Console.WriteLine();
                        Console.WriteLine("Введите номер департамента для удаления");
                        delnum = int.Parse(Console.ReadLine());
                        rep.DelDep(delnum);
                        Console.Clear();
                        Console.WriteLine("Запись удалена\n\n");
                        rep.PrintDepDB();
                        Console.ReadLine();
                        break;

                    case 2:
                        rep.PrintWorkDB();
                        Console.WriteLine();
                        Console.WriteLine("Введите номер сотрудника для удаления");
                        delnum = int.Parse(Console.ReadLine());
                        rep.DelWork(delnum);
                        Console.Clear();
                        Console.WriteLine("Запись удалена\n\n");
                        rep.PrintWorkDB();
                        Console.ReadLine();
                        break;

                    case 3:
                        rep.PrintDepDB();
                        Console.WriteLine();
                        Console.WriteLine("Введите номер департамента в котором удалить сотрудников");
                        delnum = int.Parse(Console.ReadLine());
                        rep.DelAllWorkinDep(delnum);
                        Console.Clear();
                        Console.WriteLine("Запись удалена\n\n");
                        rep.PrintDepDB();
                        Console.ReadLine();
                        break;

                    case 4:
                        rep.DelDB();
                        Console.Clear();
                        Console.WriteLine("База очищена\n\n");
                        Console.ReadLine();
                        break;

                    case 0:
                        Console.Clear();
                        menu = false;
                        break;

                    default:
                        break;
                }
            }

            /// <summary>
            /// Метод редактирования данных
            /// </summary>
            void EditData()
            {
                Console.Clear();
                Console.WriteLine("Редактирование записей:\n");
                Console.WriteLine("1 - Департаменты");
                Console.WriteLine("2 - Сотрудники");
                Console.WriteLine("0 - Назад в основное меню");
                CheckInt(ref input);

                switch (input)
                {
                    case 1:
                        rep.PrintDepDB();
                        Console.WriteLine();
                        Console.WriteLine("Введите номер департамента для редактирования:");
                        line = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите новую дату(формат dd.mm.yyyy):");
                        CheckDate(ref date);
                        Console.WriteLine("Введите новое название отдела:");
                        department = Console.ReadLine();
                        rep.DepEdit(line, date, department);
                        Console.Clear();
                        Console.WriteLine("Данные отредактированы\n\n");
                        rep.PrintDepDB();
                        Console.ReadLine();
                        break;

                    case 2:
                        rep.PrintWorkDB();
                        Console.WriteLine();
                        Console.WriteLine("Введите номер сотрудника для редактирования:");
                        line = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите новое имя сотрудника:");
                        name = Console.ReadLine();
                        Console.WriteLine("Введите новую фамилию сотрудника");
                        surname = Console.ReadLine();
                        Console.WriteLine("Введите новый возраст сотрудника");
                        age = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите новую зарплату сотрудника");
                        salary = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите новое количество проектов сотрудника");
                        projects = Convert.ToInt32(Console.ReadLine());
                        rep.WorkEdit(line, name, surname, age, salary, projects);
                        Console.Clear();
                        Console.WriteLine("Данные отредактированы\n\n");
                        rep.PrintWorkDB();
                        Console.ReadLine();
                        break;

                    case 0:
                        Console.Clear();
                        menu = false;
                        break;

                    default:
                        break;
                }
            }

            /// <summary>
            /// Метод сортировки данных
            /// </summary>
            void SortData()
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Сортировка сотрудников:\n");
                Console.WriteLine("1 - По фамилии");
                Console.WriteLine("2 - По возрасту");
                Console.WriteLine("3 - По размеру зарплаты");
                Console.WriteLine("4 - По порядковому номеру");
                Console.WriteLine("0 - Назад в основное меню");
                CheckInt(ref input);

                switch (input)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        rep.SortWork(input);
                        Console.Clear();
                        break;

                    case 0:
                        Console.Clear();
                        menu = false;
                        break;

                    default:
                        break;
                }
            }

            /// <summary>
            /// Метод вывода данных
            /// </summary>
            void ShowData()
            {
                Console.Clear();
                rep.PrintDepDB();
                Console.WriteLine();
                rep.PrintWorkDB();
                Console.ReadLine();
            }

        }
    }
}
