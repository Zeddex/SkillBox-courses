using Domain.Entities;
using Domain.Enums;
using Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Domain.Models
{
    public class BankProvider
    {
        public event Action<int, string> Transaction;

        public static bool CheckConnection()
        {
            AppContext context = new();
            return(context.Database.CanConnect());
        }

        public ObservableCollection<Department> DepartmentsList()
        {
            ObservableCollection<Department> dep = new();

            if (!CheckConnection())
            {
                throw new DbErrorConnection("DataBase Connection Error!");
            }

            using (AppContext context = new())
            {
                List<Department> depList = context.Departments.ToList();

                foreach (var item in depList)
                {
                    dep.Add(item);
                }
            }

            return dep;
        }

        public int GetDepartmentId(string depName)
        {
            int depId;

            using (AppContext context = new())
            {
                var dep = context.Departments.Single(d => d.DepartmentNameString == depName);
                depId = dep.Id;
            }
            return depId;
        }

        public Dictionary<string, decimal> ShowClients(int depId)
        {
            Dictionary<string, decimal> clientsList = new();

            using (AppContext context = new())
            {
                var clients = from client in context.Clients
                              join department in context.Departments on client.DepartmentRefId equals department.Id
                              join money in context.Money on client.Id equals money.ClientId
                              where department.Id == depId
                              select new
                              {
                                  Name = client.Name,
                                  Funds = money.Funds
                              };

                foreach (var client in clients)
                {
                    clientsList.Add(client.Name, client.Funds);
                }
            }
            return clientsList;
        }

        public int GetClientId(string name)
        {
            int id;

            using (AppContext context = new())
            {
                var client = context.Clients.First(c => c.Name == name);
                id = client.Id;
            }
            return id;
        }

        public string GetClientName(int clientId)
        {
            string name;

            using (AppContext context = new())
            {
                var client = context.Clients.Single(c => c.Id == clientId);
                name = client.Name;
            }
            return name;
        }

        public void GetClientInfo(int clientId, out decimal clientFunds, out decimal clientLoan, out decimal clientDeposit, out string clientDepositType)
        {

            using (AppContext context = new())
            {
                var result = context.Clients.Join(context.Money,
                    c => c.Id,
                    m => m.ClientId,
                    (c, m) => new
                    {
                        Id = m.ClientId,
                        Funds = m.Funds,
                        Loan = m.Loan,
                        Deposit = m.Deposit,
                        DepositType = m.DepositTypeString
                    });
                var client = result.Single(m => m.Id == clientId);

                clientFunds = client.Funds;
                clientLoan = client.Loan;
                clientDeposit = client.Deposit;
                clientDepositType = client.DepositType;
            }
        }

        public void GetDepartmentInfo(int depId, out int departmentLoanRate, out int departmentDepositRate)
        {
            using (AppContext context = new())
            {
                var result = context.Clients.Join(context.Departments,
                    c => c.Id,
                    d => d.Id,
                    (c, d) => new
                    {
                        Id = d.Id,
                        LoanRate = d.LoanRate,
                        DepositRate = d.DepositRate
                    });
                var dep = result.Single(d => d.Id == depId);

                departmentLoanRate = dep.LoanRate;
                departmentDepositRate = dep.DepositRate;
            }
        }

        public void MakeSimpleDeposit(int clientId, decimal amount)
        {
            decimal currentFunds = GetFundsAmount(clientId);
            decimal newFunds = currentFunds - amount;
            decimal currentDeposit = GetDepositAmount(clientId);
            decimal newDeposit = currentDeposit + amount;

            UpdateFundsAmount(clientId, newFunds);
            UpdateDepositAmount(clientId, newDeposit);
            SetDepositAsSimle(clientId);

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

        public decimal GetLoanAmount(int clientId)
        {
            decimal money;

            using (AppContext context = new())
            {
                var client = context.Money.First(c => c.ClientId == clientId);
                money = client.Loan;
            }
            return money;
        }

        public decimal GetDepositAmount(int clientId)
        {
            decimal money;

            using (AppContext context = new())
            {
                var client = context.Money.First(c => c.ClientId == clientId);
                money = client.Deposit;
            }
            return money;
        }

        public decimal GetFundsAmount(int clientId)
        {
            decimal money;

            using (AppContext context = new())
            {
                var client = context.Money.First(c => c.ClientId == clientId);
                money = client.Funds;
            }
            return money;
        }

        public void UpdateFundsAmount(int clientId, decimal amount)
        {
            using (AppContext context = new())
            {
                var client = context.Money.FirstOrDefault(c => c.ClientId == clientId);

                if (client != null)
                {
                    client.Funds = amount;
                    _ = context.SaveChanges();
                }
            }
        }

        public void UpdateLoanAmount(int clientId, decimal amount)
        {
            using (AppContext context = new())
            {
                var client = context.Money.FirstOrDefault(c => c.ClientId == clientId);

                if (client != null)
                {
                    client.Loan = amount;
                    _ = context.SaveChanges();
                }
            }
        }

        public void UpdateDepositAmount(int clientId, decimal amount)
        {
            using (AppContext context = new())
            {
                var client = context.Money.FirstOrDefault(c => c.ClientId == clientId);

                if (client != null)
                {
                    client.Deposit = amount;
                    _ = context.SaveChanges();
                }
            }
        }

        public void SetDepositAsSimle(int clientId)
        {
            using (AppContext context = new())
            {
                var client = context.Money.FirstOrDefault(c => c.ClientId == clientId);

                if (client != null)
                {
                    client.DepositType = DepositType.Simple;
                    _ = context.SaveChanges();
                }
            }
        }

        public void SetDepositAsCapitalized(int clientId)
        {
            using (AppContext context = new())
            {
                var client = context.Money.FirstOrDefault(c => c.ClientId == clientId);

                if (client != null)
                {
                    client.DepositType = DepositType.Capitalization;
                    _ = context.SaveChanges();
                }
            }
        }

        public void AddTransaction(int clientId, string operation)
        {
            using (AppContext context = new())
            {
                Transaction transaction = new()
                {
                    Operation = operation,
                    ClientRefId = clientId
                };

                _ = context.Transactions.Add(transaction);
                _ = context.SaveChanges();
            }
        }

        /// <summary>
        /// Calculate deposit monthly interest
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="depType"></param>
        /// <param name="depRate"></param>
        /// <returns></returns>
        public decimal[] DepositInfo(int clientId, string depType, int depRate)
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
            return months;
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
