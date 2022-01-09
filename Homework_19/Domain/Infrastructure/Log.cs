﻿using System.Collections.ObjectModel;
using Domain.Models;

namespace Domain.Infrastructure
{
    public class Log
    {
        public ObservableCollection<string> logFile = new();
        private readonly BankProvider _provider = new();

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
            _provider.AddTransaction(clientId, message);
        }
    }
}
