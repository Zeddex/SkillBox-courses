using Domain.Entities;
using System.Collections.Generic;

namespace Persistence.Models
{
    public interface IDbContext
    {
        void AddTransaction(int clientId, string operation);
        List<Department> DepartmentsList();
        decimal GetClientDeposit(int clientId);
        string GetClientDepositType(int clientId);
        decimal GetClientFunds(int clientId);
        int GetClientId(string name);
        decimal GetClientLoan(int clientId);
        string GetClientName(int clientId);
        int GetDepartmentDepositRate(int depId);
        int GetDepartmentId(string depName);
        int GetDepartmentLoanRate(int depId);
        decimal GetDepositAmount(int clientId);
        decimal GetFundsAmount(int clientId);
        void GetLoan(int clientId, decimal amount);
        decimal GetLoanAmount(int clientId);
        void SetDepositAsCapitalized(int clientId);
        void SetDepositAsSimple(int clientId);
        Dictionary<string, decimal> ShowClients(int depId);
        void UpdateDepositAmount(int clientId, decimal amount);
        void UpdateFundsAmount(int clientId, decimal amount);
        void UpdateLoanAmount(int clientId, decimal amount);
    }
}