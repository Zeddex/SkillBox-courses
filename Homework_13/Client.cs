using System;
using System.Collections.Generic;
using System.Text;

namespace Homework_13
{
    public abstract class Client
    {
        static Random rnd = new Random();
        int randScore = rnd.Next(0, 3);
        private int randCash = rnd.Next(0, 10000);

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

        public Client(string name = "RandomClient")
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

