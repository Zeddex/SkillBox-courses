using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_13
{
    internal class Core
    {
        public ObservableCollection<BankDep> bank;
        Random rnd = new Random();

        /// <summary>
        /// Create bank structure with 3 departments
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<BankDep> CreateBank()
        {
            bank = new ObservableCollection<BankDep>();

            // create 3 main departments
            bank.Add(new IndividBank());
            bank.Add(new BusinessBank());
            bank.Add(new VipBank());

            AddClientsToBank(rnd.Next(10, 30), rnd.Next(10, 30), rnd.Next(10, 30));

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
                bank[0].Clients.Add(new Individual());
            }

            // add business clients to business department
            for (int i = 0; i < business; i++)
            {
                bank[1].Clients.Add(new Business());
            }

            // add vip clients to vip department
            for (int i = 0; i < vip; i++)
            {
                bank[2].Clients.Add(new Vip());
            }
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
            sender.Money -= amount;
            recipient.Money += amount;
        }

        /// <summary>
        /// Make simple deposit without capitalization
        /// </summary>
        /// <param name="client"></param>
        /// <param name="amount"></param>
        public void MakeSimpleDeposit(Client client, uint amount)
        {
            client.Money -= amount;
        }

        /// <summary>
        /// Make deposit with capitalization
        /// </summary>
        /// <param name="client"></param>
        /// <param name="amount"></param>
        public void MakeCapitalizedDeposit(Client client, uint amount)
        {
            client.Money -= amount;
        }

        /// <summary>
        /// Get loan
        /// </summary>
        /// <param name="client"></param>
        /// <param name="amount"></param>
        public void GetLoan(Client client, uint amount)
        {
            client.Money += amount;
        }
    }
}
