using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_01
{
    class Program
    {
        static void Main(string[] args)
        {

            // * Задание 2. Создать отдел из 40 сотрудников и реализовать несколько увольнений, по результатам
            //              которых в отделе должно остаться не более 30 работников


            // Создание базы данных из 40 сотрудников
            Repository repository = new Repository(40);

            // Печать в консоль всех сотрудников
            repository.Print("База данных до преобразования");

            // Увольнение всех работников с именем "Агата"
            repository.DeleteWorkerByName("Агата");
            // Печать в консоль сотрудников, которые не попали под увольнение
            repository.Print("База данных после первого преобразования");

            // Увольнение всех работников с именем "Агата"
            repository.DeleteWorkerByName("Агнес");
            // Печать в консоль сотрудников, которые не попали под увольнение
            repository.Print("База данных после второго преобразования");

            // Увольнение всех работников с именем "Агата"
            repository.DeleteWorkerByName("Аделаида");
            // Печать в консоль сотрудников, которые не попали под увольнение
            repository.Print("База данных после третьего преобразования");

        }
    }
}
