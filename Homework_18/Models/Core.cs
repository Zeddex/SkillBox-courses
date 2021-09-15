using System.Collections.Generic;
using System.Linq;
using Homework_18.Infrastructure;

namespace Homework_18.Models
{
    public class Core
    {
        public Core()
        {
            _provider.Transaction += Core_Transaction;
        }

        private readonly Log log = new();
        private readonly BankProvider _provider = new();

        private void Core_Transaction(int clientId, string message)
        {
            log.AddToLog(message);
            log.AddToDbLog(clientId, message);
        }
    }
}
