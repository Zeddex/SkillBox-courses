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

    internal class IndividBank : BankDep
    {
        public override string Department { get; set; } = "IndividualDepartment";

        public IndividBank()
        {
            Clients = new ObservableCollection<Client>();
        }
    }

    internal class BusinessBank : BankDep
    {
        public override string Department { get; set; } = "BusinessDepartment";

        public BusinessBank()
        {
            Clients = new ObservableCollection<Client>();
        }
    }

    internal class VipBank : BankDep
    {
        public override string Department { get; set; } = "VipDepartment";

        public VipBank()
        {
            Clients = new ObservableCollection<Client>();
        }
    }
}
