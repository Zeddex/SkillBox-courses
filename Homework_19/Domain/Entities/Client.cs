using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? DepartmentRefId { get; set; }

        public Department Department { get; set; }

        public Money Funds { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
