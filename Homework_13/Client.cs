using System;
using System.Collections.Generic;
using System.Text;

namespace Homework_13
{
    public enum DepositType
    {
        Simple,
        Capitalization
    }

    public abstract class Client
    {
        static Random rnd = new Random();
        int randScore = rnd.Next(0, 3);
        private int randCash = rnd.Next(0, 10000);

        public string Name { get; set; }
        public int Money { get; set; }
        public bool IsLoan { get; set; }
        public abstract int LoanRate { get; set; }
        public bool IsDeposit { get; set; }
        public abstract int DepositRate { get; set; }
        public DepositType DepositType { get; set; }
        public bool CreditScore { get; set; }
        public abstract string Status { get; set; }

        public Client(string name = "RandomClient")
        {
            Name = name;

            switch (randScore)                  // 33% probability is good credit score
            {
                case 0:
                case 1:
                    CreditScore = false;
                    break;
                default:
                    CreditScore = true;
                    break;
            }

            Money = randCash;

        }
    }

    public class Individual : Client
    {
        public override int LoanRate { get; set; } = 15;
        public override int DepositRate { get; set; } = 5;
        public override string Status { get; set; } = "Individual";
        public Individual() : base($"Individual Client-{Guid.NewGuid().ToString().Substring(0, 5)}") {}
    }

    internal class Business : Client
    {
        public override int LoanRate { get; set; } = 10;
        public override int DepositRate { get; set; } = 10;
        public override string Status { get; set; } = "Business";
        public Business() : base($"Business Client-{Guid.NewGuid().ToString().Substring(0, 5)}") {}
    }

    internal class Vip : Client
    {
        public override int LoanRate { get; set; } = 5;
        public override int DepositRate { get; set; } = 15;
        public override string Status { get; set; } = "VIP";
        public Vip() : base($"VIP Client-{Guid.NewGuid().ToString().Substring(0, 5)}") {}
    }

}

