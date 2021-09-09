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

        public (int id, string name, decimal funds, string department,
            decimal loan, decimal deposit, string depositType) clientData;
        public (int id, string name, int loanRate, int depositRate) departmentData;


        //public List<decimal> MonthList()
        //{
        //    List<decimal> monthsList = _provider.DepositInfo(clientData.id, clientData.depositType,
        //        departmentData.depositRate).ToList();

        //    return monthsList;
        //}

        private void Core_Transaction(int clientId, string message)
        {
            log.AddToLog(message);
            log.AddToDbLog(clientId, message);
        }
    }
}
