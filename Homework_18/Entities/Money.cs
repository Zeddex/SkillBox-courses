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

        public DepositType Type { get; set; }

        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }
    }
}
