using Application;
using Domain.Entities;
using Domain.Ext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.Models
{
    public class BankProvider : IDataAccess
    {
        private readonly IDbContext _db = new AppContext();

        public static event Action<int, string>? Transaction;

        public List<string> TransactionsList()
        {
            return _db.TransactionsList();
        }

        public bool CheckConnection()
        {
            AppContext context = new();
            return (context.Database.CanConnect());
        }

        public List<Department> DepartmentsList()
        {
            return _db.DepartmentsList();
        }

        public int GetDepartmentId(string depName)
        {
            return _db.GetDepartmentId(depName);
        }

        public Dictionary<string, decimal> ShowClients(int depId)
        {
            return _db.ShowClients(depId);
        }

        public int GetClientId(string name)
        {
            return _db.GetClientId(name);
        }

        public string GetClientName(int clientId)
        {
            return _db.GetClientName(clientId);
        }

        public decimal GetClientFunds(int clientId)
        {
            return _db.GetClientFunds(clientId);
        }

        public decimal GetClientLoan(int clientId)
        {
            return _db.GetClientLoan(clientId);
        }

        public decimal GetClientDeposit(int clientId)
        {
            return _db.GetClientDeposit(clientId);
        }

        public string GetClientDepositType(int clientId)
        {
            return _db.GetClientDepositType(clientId);
        }

        public int GetDepartmentLoanRate(int depId)
        {
            return _db.GetDepartmentLoanRate(depId);
        }

        public int GetDepartmentDepositRate(int depId)
        {
            return _db.GetDepartmentDepositRate(depId);
        }

        public decimal GetLoanAmount(int clientId)
        {
            return _db.GetLoanAmount(clientId);
        }

        public decimal GetDepositAmount(int clientId)
        {
            return _db.GetDepositAmount(clientId);
        }

        public decimal GetFundsAmount(int clientId)
        {
            return _db.GetFundsAmount(clientId);
        }

        public void UpdateFundsAmount(int clientId, decimal amount)
        {
            _db.UpdateFundsAmount(clientId, amount);
        }

        public void UpdateLoanAmount(int clientId, decimal amount)
        {
            _db.UpdateLoanAmount(clientId, amount);
        }

        public void UpdateDepositAmount(int clientId, decimal amount)
        {
            _db.UpdateDepositAmount(clientId, amount);
        }

        public void SetDepositAsSimple(int clientId)
        {
            _db.SetDepositAsSimple(clientId);
        }

        public void SetDepositAsCapitalized(int clientId)
        {
            _db.SetDepositAsCapitalized(clientId);
        }

        public void AddTransaction(int clientId, string operation)
        {
            _db.AddTransaction(clientId, operation);
        }

        public void MakeSimpleDeposit(int clientId, decimal amount)
        {
            decimal currentFunds = GetFundsAmount(clientId);
            decimal newFunds = currentFunds - amount;
            decimal currentDeposit = GetDepositAmount(clientId);
            decimal newDeposit = currentDeposit + amount;

            UpdateFundsAmount(clientId, newFunds);
            UpdateDepositAmount(clientId, newDeposit);
            SetDepositAsSimple(clientId);

            Transaction?.Invoke(clientId, $"Simple deposit ${amount} was made by {GetClientName(clientId)}");
        }

        public void MakeCapitalizedDeposit(int clientId, decimal amount)
        {
            decimal currentFunds = GetFundsAmount(clientId);
            decimal newFunds = currentFunds - amount;
            decimal currentDeposit = GetDepositAmount(clientId);
            decimal newDeposit = currentDeposit + amount;

            UpdateFundsAmount(clientId, newFunds);
            UpdateDepositAmount(clientId, newDeposit);
            SetDepositAsCapitalized(clientId);

            Transaction?.Invoke(clientId, $"Capitalized deposit ${amount} was made by {GetClientName(clientId)}");
        }

        public void TransferFunds(int senderId, int recipientId, decimal amount)
        {
            decimal senderFundsAmount = GetFundsAmount(senderId);
            decimal recipientFundsAmount = GetFundsAmount(recipientId);
            decimal senderNewFunds = senderFundsAmount - amount;
            decimal recipientNewFunds = recipientFundsAmount + amount;

            UpdateFundsAmount(senderId, senderNewFunds);
            UpdateFundsAmount(recipientId, recipientNewFunds);

            Transaction?.Invoke(senderId, $"Transferred ${amount} from {GetClientName(senderId)} to {GetClientName(recipientId)}");
        }

        public void GetLoan(int clientId, decimal amount)
        {
            decimal currentLoan = GetLoanAmount(clientId);
            decimal currentFunds = GetFundsAmount(clientId);
            decimal newLoan = currentLoan + amount;
            decimal newFunds = currentFunds + amount;

            UpdateLoanAmount(clientId, newLoan);
            UpdateFundsAmount(clientId, newFunds);

            Transaction?.Invoke(clientId, $"{GetClientName(clientId)} got a ${amount} loan");
        }

        /// <summary>
        /// Calculate deposit monthly interest
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="depType"></param>
        /// <param name="depRate"></param>
        /// <returns></returns>
        public List<decimal> DepositInfo(int clientId, string depType, int depRate)
        {
            decimal[] months = new decimal[12];

            decimal deposit = GetDepositAmount(clientId);

            // simple interest
            if (depType == "Simple")
            {
                for (int i = 0; i < months.Length; i++)
                {
                    if (i == 0)
                    {
                        months[i] = (deposit / 100 * depRate / 12) + deposit;
                        _ = Math.Round(months[i], 2);
                        months[i] = Math.Round(months[i], 2);
                        continue;
                    }

                    months[i] = (deposit / 100 * depRate / 12) + months[i - 1];
                    _ = Math.Round(months[i], 2);
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
                        months[i] = (deposit / 100 * depRate / 12) + deposit;
                        _ = Math.Round(months[i], 2);
                        months[i] = Math.Round(months[i], 2);
                        continue;
                    }

                    months[i] = (months[i - 1] / 100 * depRate / 12) + months[i - 1];
                    months[i] = Math.Round(months[i], 2);
                }
            }

            return months.ToList();
        }

        /// <summary>
        /// Check the sender have enough money to make transfer
        /// </summary>
        /// <param name="client"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool CheckSuffAmount(decimal clientFunds, decimal amount)
        {
            return clientFunds >= amount;
        }

        public bool CheckFundsPositive(bool result)
        {
            return !result ? throw new InsufficientFundsException("Insufficient Funds!") : true;
        }

        public bool CheckWrongAmount(bool result)
        {
            return !result ? throw new WrongAmountException("Wrong Amount!") : true;
        }
    }
}
