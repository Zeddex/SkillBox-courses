﻿using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Persistence.Models
{
    public interface IDataAccess
    {
        void AddTransaction(int clientId, string operation);
        public bool CheckConnection();
        public event Action<int, string>? Transaction;
        //ObservableCollection<Department> DepartmentsList();
        List<Department> DepartmentsList();
        int GetClientId(string name);
        void GetClientInfo(int clientId, out decimal clientFunds, out decimal clientLoan, out decimal clientDeposit, out string clientDepositType);
        string GetClientName(int clientId);
        int GetDepartmentId(string depName);
        void GetDepartmentInfo(int depId, out int departmentLoanRate, out int departmentDepositRate);
        decimal GetDepositAmount(int clientId);
        decimal GetFundsAmount(int clientId);
        decimal GetLoanAmount(int clientId);
        void SetDepositAsCapitalized(int clientId);
        void SetDepositAsSimle(int clientId);
        Dictionary<string, decimal> ShowClients(int depId);
        void UpdateDepositAmount(int clientId, decimal amount);
        void UpdateFundsAmount(int clientId, decimal amount);
        void UpdateLoanAmount(int clientId, decimal amount);
    }
}