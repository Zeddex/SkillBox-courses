using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Data;


namespace Homework_17
{
    internal class Core
    {
        public event Action<int, string> Transaction;

        /// <summary>
        /// Check the sender have enough money to make transfer
        /// </summary>
        /// <param name="client"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool CheckSuffAmount(double clientFunds, double amount)
        {
            bool result = clientFunds >= amount;
            return result;
        }

        /// <summary>
        /// Transfer money from one client to another
        /// </summary>
        /// <param name="client"></param>
        /// <param name="amount"></param>
        public void TransferFunds(int senderId, string recipient, double amount)
        {
            int recipientId = SqlQueries.GetClientId(recipient);
            double recipientFunds = SqlQueries.GetFundsAmount(recipientId);
            double senderFunds = SqlQueries.GetFundsAmount(senderId);
            double senderNewFunds = senderFunds - amount;
            double recipientNewFunds = recipientFunds + amount;

            SqlQueries.ChangeFundsAmount(senderId, senderNewFunds);
            SqlQueries.ChangeFundsAmount(recipientId, recipientNewFunds);

            Transaction?.Invoke(senderId, $"Transfer of ${amount} was made from {GetClientNameById(senderId)} to {recipient}");
        }

        /// <summary>
        /// Make simple deposit without capitalization
        /// </summary>
        /// <param name="client"></param>
        /// <param name="amount"></param>
        public void MakeSimpleDeposit(int clientId, double amount)
        {
            double fundsAvailable = SqlQueries.GetFundsAmount(clientId);
            double newAmount = fundsAvailable - amount;
            int simpleDep = 1;

            SqlQueries.ChangeFundsAmount(clientId, newAmount);
            SqlQueries.MakeDeposit(clientId, amount);
            SqlQueries.ChangeDepositType(clientId, simpleDep);

            Transaction?.Invoke(clientId, $"Simple deposit ${amount} was made by {GetClientNameById(clientId)}");
        }

        /// <summary>
        /// Make deposit with capitalization
        /// </summary>
        /// <param name="client"></param>
        /// <param name="amount"></param>
        public void MakeCapitalizedDeposit(int clientId, double amount)
        {
            double fundsAvailable = SqlQueries.GetFundsAmount(clientId);
            double newAmount = fundsAvailable - amount;
            int capDep = 2;

            SqlQueries.ChangeFundsAmount(clientId, newAmount);
            SqlQueries.MakeDeposit(clientId, amount);
            SqlQueries.ChangeDepositType(clientId, capDep);

            Transaction?.Invoke(clientId, $"Capitalized deposit ${amount} was made by {GetClientNameById(clientId)}");
        }

        /// <summary>
        /// Get loan
        /// </summary>
        /// <param name="client"></param>
        /// <param name="amount"></param>
        public void GetLoan(int clientId, double amount)
        {
            double fundsAvailable = SqlQueries.GetFundsAmount(clientId);

            double loanAmount = SqlQueries.GetLoanAmount(clientId);
            double newAmount = fundsAvailable + amount;

            SqlQueries.ChangeFundsAmount(clientId, newAmount);
            SqlQueries.GetLoan(clientId, amount+loanAmount);

            Transaction?.Invoke(clientId, $"{GetClientNameById(clientId)} got a ${amount} loan");
        }

        public bool checkFundsPositive(bool result)
        {
            return !result ? throw new InsufficientFundsException("Insufficient Funds!") : true;
        }

        public bool checkWrongAmount(bool result)
        {
            return !result ? throw new WrongAmountException("Wrong Amount!") : true;
        }

        private string GetClientNameById(int Id)
        {
            return SqlQueries.GetClientNameById(Id);
        }

        public bool HasDeposit(int clientId)
        {
            double result = SqlQueries.GetDepositAmount(clientId);

            return result != 0;
        }

        public bool HasLoan(int clientId)
        {
            double result = SqlQueries.GetLoanAmount(clientId);

            return result != 0;
        }



        /// <summary>
        /// Calculate deposit monthly interest
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public double[] DepositInfo(int clientId, string depType, int depRate)
        {
            double[] months = new double[12];

            double deposit = SqlQueries.GetDepositAmount(clientId);

            // simple interest
            if (depType == "Simple")
            {
                for (int i = 0; i < months.Length; i++)
                {
                    if (i == 0)
                    {
                        months[i] = (((double)deposit / 100 * depRate) / 12) + deposit;
                        Math.Round(months[i], 2);
                        months[i] = Math.Round(months[i], 2);
                        continue;
                    }

                    months[i] = (((double)deposit / 100 * depRate) / 12) + months[i - 1];
                    Math.Round(months[i], 2);
                    months[i] = Math.Round(months[i], 2);
                }
            }

            // capitalized interest
            else
            {
                for (int i = 0; i < months.Length; i++)
                {
                    if (i == 0)
                    {
                        months[i] = (((double)deposit / 100 * depRate) / 12) + deposit;
                        Math.Round(months[i], 2);
                        months[i] = Math.Round(months[i], 2);
                        continue;
                    }

                    months[i] = ((months[i - 1] / 100 * depRate) / 12) + months[i - 1];
                    months[i] = Math.Round(months[i], 2);
                }
            }

            return months;
        }

    }
}
