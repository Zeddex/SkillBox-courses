using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Homework_18
{
    public class Core
    {
        private readonly BankProvider provider = new();

        public (int id, string name, decimal funds, string department,
            decimal loan, decimal deposit, string depositType) clientData;
        public (int id, string name, int loanRate, int depositRate) departmentData;

        public void GetClientsInfo()
        {
            provider.GetClientId(clientData.name);
            provider.GetClientInfo(clientData.id, out clientData.funds, out clientData.loan, out clientData.deposit,
                out clientData.depositType);
            provider.GetDepartmentInfo(departmentData.id, out departmentData.loanRate, out departmentData.depositRate);
        }

        public void RefreshClientsInfo()
        {
            clientData.funds = provider.GetFundsAmount(clientData.id);
            clientData.loan = provider.GetLoanAmount(clientData.id);
            clientData.deposit = provider.GetDepositAmount(clientData.id);
        }

        public List<decimal> MonthList()
        {
            List<decimal> monthsList = provider.DepositInfo(clientData.id, clientData.depositType,
                departmentData.depositRate).ToList();

            return monthsList;
        }
    }
}
