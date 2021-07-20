using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_18.Entities
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int DepartmentRefId { get; set; }

        [ForeignKey(nameof(DepartmentRefId))]
        public Department Department { get; set; }

        public Money Funds { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
