using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12
{
    class Organisation
    {
        public string Title { get; set; }
        public uint Id { get; set; }
        public uint ParId { get; set; }
        public uint AdministratorId { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }
        public static uint count = 0;

        public bool SalaryFlag { get; set; }        // true if the salary is counted in current department
        public uint SalaryAmount { get; set; }      // salary amount of all employees in current department

        public Organisation(string title)
        {
            Title = title;
            Id = ++count;
            Employees = new ObservableCollection<Employee>();
        }

        public Organisation() : this($"dep{count}") { }
    }
}
