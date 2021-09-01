using System.Collections.ObjectModel;
using Homework_18.Models;

namespace Homework_18.Infrastructure
{
    public class Log
    {
        public ObservableCollection<string> logFile = new();
        private readonly BankProvider provider = new();

        /// <summary>
        /// Add message to log list
        /// </summary>
        /// <param name="msg"></param>
        public void AddToLog(string msg)
        {
            logFile.Add(msg);
        }

        public void AddToDbLog(int clientId, string message)
        {
            provider.AddTransaction(clientId, message);
        }
    }
}
