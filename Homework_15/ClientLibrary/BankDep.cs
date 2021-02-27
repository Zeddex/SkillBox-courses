using System;
using System.Collections.ObjectModel;

namespace ClientLibrary
{
    public abstract class BankDep
    {
        public ObservableCollection<Client> Clients { get; set; }
        public abstract string Department { get; set; }
    }

    public class IndividBank : BankDep
    {
        public override string Department { get; set; } = "Individual";

        public IndividBank()
        {
            Clients = new ObservableCollection<Client>();
        }
    }

    public class BusinessBank : BankDep
    {
        public override string Department { get; set; } = "Business";

        public BusinessBank()
        {
            Clients = new ObservableCollection<Client>();
        }
    }

    public class VipBank : BankDep
    {
        public override string Department { get; set; } = "VIP";

        public VipBank()
        {
            Clients = new ObservableCollection<Client>();
        }
    }
}
