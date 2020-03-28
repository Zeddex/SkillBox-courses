namespace Homework_08
{
    public struct Worker
    {
        #region Свойства
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int Department { get; set; }
        public int Salary { get; set; }
        public int Projects { get; set; }
        public int Number { get; set; }
        #endregion

        static public int count = 0;


        /// <summary>
        /// Конструктор создающий сотрудника
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="age">Возраст</param>
        /// <param name="department">Отдел</param>
        /// <param name="salary">Зарплата</param>
        /// <param name="projects">Кол-во проектов</param>
        public Worker(string name, string surname, int age, int department, int salary, int projects)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Department = department;
            Salary = salary;
            Projects = projects;

            Number = count++;
        }
    }
}
