using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Homework_18.Enums;

namespace Homework_18.Entities
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
