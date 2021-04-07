using System;

namespace Homework_17
{
    public abstract class Client
    {
        static readonly Random rnd = new Random();
        readonly int randScore = rnd.Next(0, 3);
        private readonly int randCash = rnd.Next(0, 10000);

        public string Name { get; set; }
        public uint Money { get; set; }
        public Loan IsLoan { get; set; }
        public abstract int LoanRate { get; set; }
        public Deposit IsDeposit { get; set; }
        public abstract int DepositRate { get; set; }
        public DepositType DepositType { get; set; }
        public uint DepositAmount { get; set; }
        public CreditScore CreditScore { get; set; }
        public abstract string Status { get; set; }

        protected Client(string name = "RandomClient")
        {
            Name = name;

            switch (randScore)                  // 33% probability is good credit score
            {
                case 0:
                case 1:
                    CreditScore = CreditScore.No;
                    break;
                default:
                    CreditScore = CreditScore.Yes;
                    LoanRate -= 3;              // extra rate to good clients
                    DepositRate += 3;
                    break;
            }

            Money = (uint)randCash;
        }
    }
}

