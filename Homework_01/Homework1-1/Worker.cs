using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_01
{
    /// <summary>
    /// Класс, описывающий модель работника
    /// </summary>
    class Worker
    {
        /// <summary>
        /// Имя работника
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия работника
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Возраст работника
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Зарплата работника
        /// </summary>
        public int Salary { get; set; }

        /// <summary>
        /// Конструктор, позволяющий присвоить значение соответствующим полям работника
        /// </summary>
        /// <param name="FirstName">Имя</param>
        /// <param name="LastName">Фамилия</param>
        /// <param name="Age">Возраст</param>
        /// <param name="Salary">Зарплата</param>
        public Worker(string FirstName, string LastName, int Age, int Salary)
        {
            this.FirstName = FirstName; // Сохранить переданное значение имени
            this.LastName = LastName;   // Сохранить переданное значение фамилии
            this.Age = Age;             // Сохранить переданное значение возраста
            this.Salary = Salary;       // Сохранить переданное значение зарплаты
        }

        /// <summary>
        /// Организация вывода информации о работнике
        /// </summary>
        /// <returns>Строковое представление информации</returns>
        public override string ToString()
        {
            return $"{FirstName,15} {LastName,15} {Age,10} {Salary.ToString("## ###"),10} руб.";
        }
    }
}