using System;
using Domain.Entities;
using System.Collections.Generic;

namespace Application
{
    public interface IDataAccess
    {
        void AddTransaction(int clientId, string operation);
        public bool CheckConnection();
        List<Department> DepartmentsList();
        int GetClientId(string name);
        decimal GetClientFunds(int clientId);
        decimal GetClientLoan(int clientId);
        decimal GetClientDeposit(int clientId);
        string GetClientDepositType(int clientId);
        string GetClientName(int clientId);
        int GetDepartmentId(string depName);
        int GetDepartmentLoanRate(int depId);
        int GetDepartmentDepositRate(int depId);
        List<decimal> DepositInfo(int clientId, string depType, int depRate);
        decimal GetDepositAmount(int clientId);
        decimal GetFundsAmount(int clientId);
        decimal GetLoanAmount(int clientId);
        void SetDepositAsCapitalized(int clientId);
        void SetDepositAsSimple(int clientId);
        Dictionary<string, decimal> ShowClients(int depId);
        void UpdateDepositAmount(int clientId, decimal amount);
        void UpdateFundsAmount(int clientId, decimal amount);
        void UpdateLoanAmount(int clientId, decimal amount);
        void TransferFunds(int senderId, int recipientId, decimal amount);
        void GetLoan(int clientId, decimal amount);
        void MakeSimpleDeposit(int clientId, decimal amount);
        void MakeCapitalizedDeposit(int clientId, decimal amount);
    }
}