using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public string Operation { get; set; }

        public int? ClientRefId { get; set; }

        public Client Client { get; set; }
    }
}
