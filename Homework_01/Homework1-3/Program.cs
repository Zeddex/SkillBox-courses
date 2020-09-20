using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_01
{
    class Program
    {

        // ** Задание 3. Создать отдел из 50 сотрудников и реализовать увольнение работников
        //               чья зарплата превышает 30000руб


        static void Main(string[] args)
        {
            // Создание базы данных из 50 сотрудников
            Repository repository = new Repository(50);

            // Печать в консоль всех сотрудников
            repository.Print("База данных до преобразования");

            // Увольнение всех работников, зарплата которых выше 30 000 руб
            repository.DeleteWorkerBySalary(30000);
            // Печать в консоль сотрудников, которые не попали под увольнение
            repository.Print("База данных после преобразования");

        }
    }
}
