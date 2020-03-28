using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_07
{
    struct Content
    {
        public string Date { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Org { get; set; }
        public string Position { get; set; }

        public string Print()
        {
            return $"{Date,10} {Name,10} {Surname,10} {Org,15} {Position,15}";
        }

        /// <summary>
        /// Создание записи в блокноте
        /// </summary>
        /// <param name="date"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="org"></param>
        /// <param name="position"></param>
        public Content(string date, string name, string surname, string org, string position)
        {
            Date = date;
            Name = name;
            Surname = surname;
            Org = org;
            Position = position;
        }
    }
}
