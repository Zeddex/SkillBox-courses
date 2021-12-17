using Homework_19.Enums;
using Homework_19.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_19.Entities
{
    public class Money
    {
        [Key]
        public int ClientId { get; set; }

        [Required]
        public decimal Funds { get; set; }

        public decimal Loan { get; set; }

        public decimal Deposit { get; set; }

        [Column("DepositType")]
        public string DepositTypeString
        {
            get => DepositType.ToString();
            private set => DepositType = value.ParseEnum<DepositType>();
        }

        [NotMapped]
        [Column(TypeName = "nvarchar(50)")]
        public DepositType DepositType { get; set; }

        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }
    }
}
