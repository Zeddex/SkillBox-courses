using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Homework_18.Enums;

namespace Homework_18.Entities
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DepartmentName Name { get; set; }

        public int LoanRate { get; set; }

        public int DepositRate { get; set; }

        public List<Client> Clients { get; set; }
    }
}
