using Homework_18.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Homework_18.Infrastructure;

namespace Homework_18.Entities
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("DepartmentName")]
        public string DepartmentNameString
        {
            get => DepartmentName.ToString();
            set => DepartmentName = value.ParseEnum<DepartmentName>();
        }

        [NotMapped]
        [Column(TypeName = "nvarchar(50)")]
        public DepartmentName DepartmentName { get; set; }

        public int LoanRate { get; set; }

        public int DepositRate { get; set; }

        public List<Client> Clients { get; set; }
    }
}
