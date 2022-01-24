using System.Collections.ObjectModel;

namespace Persistence.Models
{
    public class Log
    {
        public ObservableCollection<string> logFile = new();
        private readonly IDataAccess _provider = new BankProvider();

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
