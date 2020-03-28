using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Homework_08
{
    class Repo
    {
        string[] titleWork;     // Заголовки базы сотрундиков
        string[] titleDep;      // Заголовки базы департаментов
        string json;
        List<Department> dept;
        List<Worker> allworkers;
        XmlSerializer xmlSerializer;
        Random rnd = new Random();

        /// <summary>
        /// Конструктор
        /// </summary>
        public Repo()
        {
            titleWork = new string[] { "№", "Name", "Surname", "Age", "Department №", "Salary", "Projects" };
            titleDep = new string[] { "Dep №", "Dep_title", "Creation_date", "Workers" };
            dept = new List<Department>();
        }

        /// <summary>
        /// Метод, выводящий на консоль базу данных сотрудников
        /// </summary>
        public void PrintWorkDB()
        {
            Console.WriteLine($"{titleWork[0],5} {titleWork[1],10} {titleWork[2],15} {titleWork[3],10} {titleWork[4],15} {titleWork[5],15} {titleWork[6],10}");
            for (int i = 0; i < dept.Count; i++)
            {
                for (int j = 0; j < dept[i].Workers.Count; j++)
                {
                    Console.WriteLine($"{(dept[i].Workers[j].Number+1),5} {dept[i].Workers[j].Name,10} {dept[i].Workers[j].Surname,15} {dept[i].Workers[j].Age,10}" +
                        $"{(dept[i].Workers[j].Department+1),15} {dept[i].Workers[j].Salary,15} {dept[i].Workers[j].Projects,10}");
                }
            }
            Console.WriteLine($"\nКоличество работников {Worker.count}");
            
        }

        /// <summary>
        /// Метод, выводящий на печать отсортированных сотрудников
        /// </summary>
        /// <param name="allworkers">Список отсортированных работников</param>
        public void PrintSortWork(List<Worker> allworkers)
        {
            Console.WriteLine($"{titleWork[0],5} {titleWork[1],10} {titleWork[2],15} {titleWork[3],10} {titleWork[4],15} {titleWork[5],15} {titleWork[6],10}");
            for (int i = 0; i < allworkers.Count; i++)
            {
                Console.WriteLine($"{(allworkers[i].Number + 1),5} {allworkers[i].Name,10} {allworkers[i].Surname,15} {allworkers[i].Age,10}" +
                        $"{(allworkers[i].Department + 1),15} {allworkers[i].Salary,15} {allworkers[i].Projects,10}");
            }
            Console.WriteLine($"\nКоличество работников {Worker.count}");
        }

        /// <summary>
        /// Метод, выводящий базу данных департаментов
        /// </summary>
        public void PrintDepDB()
        {
            Console.WriteLine($"{titleDep[0],5} {titleDep[1],15} {titleDep[2],20} {titleDep[3],10}");
            int depnumb = 1;
            foreach (var line in dept)
            {
                Console.WriteLine($"{depnumb,5}" + line.Output() + $"{line.Workers.Count,10}");
                depnumb++;
            }
            Console.WriteLine($"\nКоличество департаментов {dept.Count}");
        }

        /// <summary>
        /// Метод генерации сотрудников
        /// </summary>
        /// <param name="sum">Количество записей</param>
        public void FillWork(int sum)
        {
            for (int i = 1; i <= sum; i++)
            {
                AddWork($"Name_{rnd.Next(1, 1000)}", $"Surname_{rnd.Next(1, 1000)}", rnd.Next(20, 50), rnd.Next(1, (dept.Count+1)), rnd.Next(1000, 10001), rnd.Next(1, 10));
            }
        }

        /// <summary>
        /// Метод генерации департаментов
        /// </summary>
        /// <param name="sum">Количество департаментов для генерации</param>

        public void FillDep(int sum)
        {
            for (int i = 1; i <= sum; i++)
            {
                AddDep($"dep_{rnd.Next(1, 100)}", RandomDate());
            }
        }

        /// <summary>
        /// Метод генерации случайной даты
        /// </summary>
        /// <returns></returns>
        DateTime RandomDate()
        {
            DateTime randomDate = new DateTime(2000, 1, 1);
            int range = (DateTime.Today - randomDate).Days;
            return randomDate.AddDays(rnd.Next(range));
        }

        /// <summary>
        /// Метод проверки наличия департаментов
        /// </summary>
        /// <returns></returns>
        public bool CheckDep()
        {
            bool isAnyDep = true;
            if (dept.Count == 0)
            {
                isAnyDep = false;
            }
            return isAnyDep;
        }


        /// <summary>
        /// Метод добавления сотрудников
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="age">Возраст</param>
        /// <param name="department">Отдел</param>
        /// <param name="salary">Зарплата</param>
        /// <param name="projects">Количество проектов</param>
        public void AddWork(string name, string surname, int age, int department, int salary, int projects)
        {
            if (dept[department - 1].Workers.Count > 999999)           // Проверка на максимально возможное количество сотрудников
            {
                Console.WriteLine("Максимальное количество работников в одном отделе - 1.000.000");
                Console.ReadLine();
                return;
            }
            dept[department-1].Workers.Add(new Worker(name, surname, age, (department-1), salary, projects));
        }

        /// <summary>
        /// Метод добавления департаментов
        /// </summary>
        /// <param name="title">Наименование департамента</param>
        /// <param name="date">Дата создания департамента</param>
        public void AddDep(string title, DateTime date)
        {
            dept.Add(new Department(title, date));
        }

        /// <summary>
        /// Метод удаления всех сотрудников из выбранного департамента
        /// </summary>
        /// <param name="delnum">Строка для удаления</param>
        public void DelAllWorkinDep(int delnum)
        {
            int workersum = dept[delnum - 1].Workers.Count;         // Получаем количество работников в департаменте
            dept[delnum - 1].Workers.Clear();                       // Удаляем всех работников из выбранного департамента
            Worker.count -= workersum;                              // Уменьшаем счётчик работников
        }

        /// <summary>
        /// Метод удаления департамента
        /// </summary>
        /// <param name="delnum">Строка для удаления</param>
        public void DelDep(int delnum)
        {
            int workersum = dept[delnum - 1].Workers.Count;         // Получаем количество работников в департаменте
            dept.RemoveAt(delnum - 1);                              // Удаляем выбранный департамент
            Worker.count -= workersum;                              // Уменьшаем счётчик работников
            DepFix(delnum-1);                                       // Перестраиваем номера департаментов у работников
        }

        /// <summary>
        /// Метод удаления работника
        /// </summary>
        /// <param name="delnum">Строка для удаления</param>
        public void DelWork(int delnum)
        {
            for (int i = 0; i < dept.Count; i++)                    // Обходим весь список для поиска нужного ID работника
            {
                for (int j = 0; j < dept[i].Workers.Count; j++)
                {
                    if (dept[i].Workers[j].Number == (delnum-1))
                    {
                        dept[i].Workers.RemoveAt(j);                // Удаляем работника
                        Worker.count--;                             // Уменьшаем счётчик работников
                    }
                }
            }
        }

        /// <summary>
        /// Метод удаления всех работников
        /// </summary>
        public void DelAllWorkers()
        {
            for (int i = 0; i < dept.Count; i++)
            {
                dept[i].Workers.Clear();
            }
            Worker.count = 0;
        }

        /// <summary>
        /// Метод подсчёта количества работников
        /// </summary>
        /// <returns></returns>
        public int WorkCount ()
        {
            int totalWork = 0;
            for (int i = 0; i < dept.Count; i++)                    // Обходим весь список департаментов с работниками
            {
                for (int j = 0; j < dept[i].Workers.Count; j++)
                {
                    totalWork++;
                }
            }
            return totalWork;
        }

        /// <summary>
        /// Метод удаления всей базы
        /// </summary>
        public void DelDB()
        {
            dept.Clear();
            Worker.count = 0;           // Сбрасываем счётчик работников
        }

        /// <summary>
        /// Метод перестроения номеров департамента после удаления одного из департаментов
        /// </summary>
        /// <param name="delnum">Номер департамента который был удалён</param>
        public void DepFix(int delnum)
        {
            for (int i = delnum; i < dept.Count; i++)                   // Обходим весь список департаментов, начиная со следующего после удалённого
            {
                for (int j = 0; j < dept[i].Workers.Count; j++)
                {
                    Worker worker = new Worker();                       // Создание временного работника для перекидывания данных
                    worker.Number = dept[i].Workers[j].Number;
                    worker.Name = dept[i].Workers[j].Name;
                    worker.Surname = dept[i].Workers[j].Surname;
                    worker.Age = dept[i].Workers[j].Age;
                    worker.Department = dept[i].Workers[j].Department;
                    worker.Salary = dept[i].Workers[j].Salary;
                    worker.Projects = dept[i].Workers[j].Projects;
                    worker.Department--;
                    dept[i].Workers[j] = worker;
                }
            }
        }

        /// <summary>
        /// Метод редактирования департамента
        /// </summary>
        /// <param name="depnum">Номер департамента</param>
        /// <param name="date">Дата</param>
        /// <param name="department">Название департамента</param>
        public void DepEdit(int depnum, DateTime date, string depname)
        {
            Department tempDep = new Department();              // Создание временного департамента для перекидывания данных
            tempDep.Date = date;
            tempDep.Title = depname;
            tempDep.Workers = dept[depnum - 1].Workers;

            dept[depnum - 1] = tempDep;                       // Возврат значений в основной департамент
        }

        /// <summary>
        /// Метод редактирования сотрудника
        /// </summary>
        /// <param name="worknum">Номер сотрудника</param>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="age">Возраст</param>
        /// <param name="salary">Зарплата</param>
        /// <param name="projects">Проекты</param>
        public void WorkEdit(int worknum, string name, string surname, int age, int salary, int projects)
        {
            Worker worker = new Worker();                       // Создание временного работника для перекидывания данных
            worker.Number = worknum-1;
            worker.Name = name;
            worker.Surname = surname;
            worker.Age = age;
            worker.Salary = salary;
            worker.Projects = projects;

            for (int i = 0; i < dept.Count; i++)                // Поиск департамента по номеру сотрудника, в котором он работает
            {
                for (int j = 0; j < dept[i].Workers.Count; j++)
                {
                    if (dept[i].Workers[j].Number == (worknum - 1))
                    {
                        worker.Department = dept[i].Workers[j].Department;      // Присваиваем временному воркеру номер искомого департамента
                        dept[i].Workers[j] = worker;                            // Перекидываем все данные назад в основного работника
                    }
                }
            }
        }

        /// <summary>
        /// Метод сериализации XML
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        public void SerializeXml(string path)
        {
            xmlSerializer = new XmlSerializer(typeof(List<Department>));                    // Создаём сериализатор
            using (StreamWriter fStream = new StreamWriter(path, false))                    // Создаём поток для сохранения данных
            {
            xmlSerializer.Serialize(fStream, dept);                                         // Запускаем сериализацию
            }
        }

        /// <summary>
        /// Метод десериализации XML
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
        public void DeserializeXml(string path)
        {
            if (!File.Exists(path))                                                         // Проверка на наличие файла
            {
                Console.WriteLine("Нет сохранённых файлов");
                Console.ReadLine();
                return;
            }
            DelDB();                                                                        // Очищаем всю базу перед десериализацией

            using (StreamReader fStream = new StreamReader(path))                           // Создаём поток для загрузки данных
            {
                xmlSerializer = new XmlSerializer(typeof(List<Department>));                // Создаём сериализатор
                dept = (List<Department>)xmlSerializer.Deserialize(fStream);                // dept = xmlSerializer.Deserialize(fStream) as List<Department> (2й вариант)
                Worker.count = WorkCount();                                                 // Получаем количество работников в департаменте
            }
        }

        /// <summary>
        /// Метод сериализации JSON
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        public void SerializeJson(string path)
        {
            json = JsonConvert.SerializeObject(dept);                       // Процесс сериализации
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Метод десериализации JSON
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        public void DeserializeJson(string path)
        {
            if (!File.Exists(path))                                         // Проверка на наличие файла
            {
                Console.WriteLine("Нет сохранённых файлов");
                Console.ReadLine();
                return;
            }
            DelDB();                                                        // Очищаем всю базу перед десериализацией

            json = File.ReadAllText(path);
            dept = JsonConvert.DeserializeObject<List<Department>>(json);   // Процесс десериализации
            Worker.count = WorkCount();                                     // Получаем количество работников в департаменте
        }

        /// <summary>
        /// Метод сортировки работников
        /// </summary>
        /// <param name="row">Столбец сортировки</param>
        public void SortWork(int row)
        {
            allworkers = new List<Worker>();                    // Создаём отдельный список для хранения работников отдельно от отделов

            for (int i = 0; i < dept.Count; i++)
            {
                for (int j = 0; j < dept[i].Workers.Count; j++)
                {
                    allworkers.Add(dept[i].Workers[j]);         // Перекидываем всех работников в отдельный список
                }
            }

            switch (row)
            {
                case 1:
                    allworkers = allworkers.OrderBy(e => e.Surname)     // Сортировка по фамилии
                        .ThenBy(e => e.Number)
                        .ThenBy(e => e.Name)
                        .ThenBy(e => e.Age)
                        .ThenBy(e => e.Department)
                        .ThenBy(e => e.Salary)
                        .ThenBy(e => e.Projects)
                        .ToList();

                    Console.Clear();
                    PrintSortWork(allworkers);
                    Console.ReadLine();
                    break;

                case 2:
                    allworkers = allworkers.OrderBy(e => e.Age)         // Сортировка по возрасту
                        .ThenBy(e => e.Number)
                        .ThenBy(e => e.Surname)
                        .ThenBy(e => e.Name)
                        .ThenBy(e => e.Department)
                        .ThenBy(e => e.Salary)
                        .ThenBy(e => e.Projects)
                        .ToList();

                    Console.Clear();
                    PrintSortWork(allworkers);
                    Console.ReadLine();
                    break;

                case 3:
                    allworkers = allworkers.OrderBy(e => e.Salary)      // Сортировка по зарплате
                        .ThenBy(e => e.Number)
                        .ThenBy(e => e.Surname)
                        .ThenBy(e => e.Name)
                        .ThenBy(e => e.Age)
                        .ThenBy(e => e.Department)
                        .ThenBy(e => e.Projects)
                        .ToList();

                    Console.Clear();
                    PrintSortWork(allworkers);
                    Console.ReadLine();
                    break;

                case 4:
                    allworkers = allworkers.OrderBy(e => e.Number)      // Сортировка по порядковому номеру
                        .ThenBy(e => e.Name)
                        .ThenBy(e => e.Surname)
                        .ThenBy(e => e.Salary)
                        .ThenBy(e => e.Age)
                        .ThenBy(e => e.Department)
                        .ThenBy(e => e.Projects)
                        .ToList();

                    Console.Clear();
                    PrintSortWork(allworkers);
                    Console.ReadLine();
                    break;

                default:
                    break;
            }
        }
    }
}
