﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ClientLibrary;

namespace Homework_15
{
    internal class Core
    {
        public event Action<string> Transaction;

        public ObservableCollection<BankDep> bank;
        readonly Random rnd = new Random();

        /// <summary>
        /// Create bank structure with 3 departments
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<BankDep> CreateBank()
        {
            int individClientsAmount = rnd.Next(10, 30);
            int businessClientsAmount = rnd.Next(10, 30);
            int vipClientsAmount = rnd.Next(10, 30);

            bank = new ObservableCollection<BankDep>
            {

                // create 3 main departments
                new IndividBank(),
                new BusinessBank(),
                new VipBank()
            };

            AddClientsToBank(individClientsAmount, businessClientsAmount, vipClientsAmount);

            return bank;
        }

        /// <summary>
        /// Add clients to bank departments
        /// </summary>
        /// <param name="individ">amount of individual clients</param>
        /// <param name="business">amount of business clients</param>
        /// <param name="vip">amount of vip clients</param>
        void AddClientsToBank(int individ, int business, int vip)
        {
            // add individual clients to individual department
            for (int i = 0; i < individ; i++)
            {
                CreateClientsCollection<Individual>((int)BankDepartment.IndividualBank);
            }

            // add business clients to business department
            for (int i = 0; i < business; i++)
            {
                CreateClientsCollection<Business>((int)BankDepartment.BusinessBank);
            }

            // add vip clients to vip department
            for (int i = 0; i < vip; i++)
            {
                CreateClientsCollection<Vip>((int)BankDepartment.VipBank);
            }
        }

        /// <summary>
        /// Add clients to bank departments
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bankDep"></param>
        void CreateClientsCollection<T>(int bankDep) where T : Client, new()
        {
            bank[bankDep].Clients.Add(new T());
        }

        /// <summary>
        /// Check the sender have enough money to make transfer
        /// </summary>
        /// <param name="client"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool CheckSuffAmount(Client client, uint amount)
        {
            bool result = client.Money >= amount;
            return result;
        }

        /// <summary>
        /// Transfer money from one client to another
        /// </summary>
        /// <param name="client"></param>
        /// <param name="amount"></param>
        public void TransferFunds(Client sender, Client recipient, uint amount)
        {
            sender.DeductMoney(amount);
            recipient.AddMoney(amount);
            Transaction?.Invoke($"Transferred ${amount} from {sender.Name} to {recipient.Name}");
        }

        /// <summary>
        /// Make simple deposit without capitalization
        /// </summary>
        /// <param name="client"></param>
        /// <param name="amount"></param>
        public void MakeSimpleDeposit(Client client, uint amount)
        {
            client.MakeDeposit(amount);
            client.DepositType = DepositType.Simple;
            client.IsDeposit = Deposit.Yes;
            Transaction?.Invoke($"Simple deposit ${amount} was made by {client.Name}");
        }

        /// <summary>
        /// Make deposit with capitalization
        /// </summary>
        /// <param name="client"></param>
        /// <param name="amount"></param>
        public void MakeCapitalizedDeposit(Client client, uint amount)
        {
            client.MakeDeposit(amount);
            client.DepositType = DepositType.Capitalization;
            client.IsDeposit = Deposit.Yes;
            Transaction?.Invoke($"Capitalized deposit ${amount} was made by {client.Name}");
        }

        /// <summary>
        /// Get loan
        /// </summary>
        /// <param name="client"></param>
        /// <param name="amount"></param>
        public void GetLoan(Client client, uint amount)
        {
            client.AddMoney(amount);
            client.IsLoan = Loan.Yes;
            Transaction?.Invoke($"{client.Name} got a ${amount} loan");
        }

        public bool checkFundsPositive(bool result)
        {
            if (!result)
            {
                throw new InsufficientFundsException("Insufficient Funds!");
            }
            else
                return true;
        }

        public bool checkWrongAmount(bool result)
        {
            if (!result)
            {
                throw new WrongAmountException("Wrong Amount!");
            }
            else
                return true;
        }

        /// <summary>
        /// Calculate deposit monthly interest
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public double[] DepositInfo(Client client)
        {
            int deposit = (int) client.DepositAmount;
            double[] months = new double[12];
            int rate = client.DepositRate;

            // simple interest
            if (client.DepositType == DepositType.Simple)
            {
                for (int i = 0; i < months.Length; i++)
                {
                    if (i == 0)
                    {
                        months[i] = (((double)deposit / 100 * rate) / 12) + deposit;
                        Math.Round(months[i], 2);
                        months[i] = Math.Round(months[i], 2);
                        continue;
                    }

                    months[i] = (((double)deposit / 100 * rate) / 12) + months[i-1];
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
                        months[i] = (((double)deposit / 100 * rate) / 12) + deposit;
                        Math.Round(months[i], 2);
                        months[i] = Math.Round(months[i], 2);
                        continue;
                    }

                    months[i] = ((months[i-1] / 100 * rate) / 12) + months[i-1];
                    months[i] = Math.Round(months[i], 2);
                }
            }

            return months;
        }
    }
}
