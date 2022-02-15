using Domain.Enums;
using Domain.Ext;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Money
    {
        public int? ClientId { get; set; }

        public decimal Funds { get; set; }

        public decimal Loan { get; set; }

        public decimal Deposit { get; set; }

        public string DepositTypeString
        {
            get => DepositType.ToString();
            private set => DepositType = value.ParseEnum<DepositType>();
        }

        public DepositType DepositType { get; set; }

        public Client Client { get; set; }
    }
}
