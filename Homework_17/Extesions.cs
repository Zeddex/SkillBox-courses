using System;

namespace Homework_17
{
    public static class Extesions
    {
        /// <summary>
        /// Add money to specified client
        /// </summary>
        /// <param name="client"></param>
        /// <param name="amount"></param>
        public static void AddMoney(this Client client, uint amount)
        {
            client.Money += amount;
        }

        /// <summary>
        /// Deduct money from specified client
        /// </summary>
        /// <param name="client"></param>
        /// <param name="amount"></param>
        public static void DeductMoney(this Client client, uint amount)
        {
            client.Money -= amount;
        }

        /// <summary>
        /// Add money to deposit account and deduct from client
        /// </summary>
        /// <param name="client"></param>
        /// <param name="amount"></param>
        public static void MakeDeposit(this Client client, uint amount)
        {
            client.Money -= amount;
            client.DepositAmount += amount;
        }
    }
}
