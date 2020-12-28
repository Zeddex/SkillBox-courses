using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Homework_13
{
    public abstract class BankDep
    {
        public ObservableCollection<Client> Clients { get; set; }
        public abstract string Department { get; set; }
    }

    internal class IndividDep : BankDep
    {
        public override string Department { get; set; } = "IndividualDepartment";

        public IndividDep()
        {
            Clients = new ObservableCollection<Client>();
        }
    }

    internal class BusinessDep : BankDep
    {
        public override string Department { get; set; } = "BusinessDepartment";

        public BusinessDep()
        {
            Clients = new ObservableCollection<Client>();
        }
    }

    internal class VipDep : BankDep
    {
        public override string Department { get; set; } = "VipDepartment";

        public VipDep()
        {
            Clients = new ObservableCollection<Client>();
        }
    }
}
