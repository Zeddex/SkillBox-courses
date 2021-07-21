using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Homework_18
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
