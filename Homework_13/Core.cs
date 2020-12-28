using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_13
{
    internal class Core
    {
        BankDep individDepartment = new IndividDep();
        BankDep businessDepartment = new BusinessDep();
        BankDep vipDepartment = new VipDep();

        private ObservableCollection<Individual> individualClients = new ObservableCollection<Individual>();
        
        public Client client1 = new Individual();

    }
}
