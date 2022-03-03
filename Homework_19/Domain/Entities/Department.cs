using Domain.Enums;
using Domain.Ext;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }

        public string DepartmentNameString
        {
            get => DepartmentName.ToString();
            set => DepartmentName = value.ParseEnum<DepartmentName>();
        }

        public DepartmentName DepartmentName { get; set; }

        public int LoanRate { get; set; }

        public int DepositRate { get; set; }

        public List<Client> Clients { get; set; }
    }
}
