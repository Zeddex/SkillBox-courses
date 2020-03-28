using System;
using System.Collections.Generic;

namespace Homework_08
{
    public struct Department
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public List<Worker> Workers { get; set; }

        /// <summary>
        /// Конструктор создающий департамент
        /// </summary>
        /// <param name="date">Дата создание отдела</param>
        /// <param name="title">Наименование отдела</param>
        /// <param name="workCount">Количество сотрудников в отделе</param>
        public Department(string title, DateTime date)
        {
            Title = title;
            Date = date;
            Workers = new List<Worker>();
        }

        /// <summary>
        /// Метод вывода значений полей
        /// </summary>
        /// <returns></returns>
        public string Output()
        {
            return $"{Title,15} {Date.ToShortDateString(),20}";
        }

    }
}
