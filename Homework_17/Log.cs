using System;
using System.Collections.ObjectModel;

namespace Homework_17
{
    public class Log
    {
        public ObservableCollection<string> logFile = new ObservableCollection<string>();

        /// <summary>
        /// Add message to log list
        /// </summary>
        /// <param name="msg"></param>
        public void AddToListLog(string msg)
        {
            logFile.Add(msg);
        }

        public void AddToDbLog(int clientId, string message)
        {
            SqlQueries.AddTransaction(clientId, message);
        }
    }
}
