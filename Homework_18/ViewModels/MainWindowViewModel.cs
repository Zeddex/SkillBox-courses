using System;
using System.Collections.Generic;
using Homework_18.Entities;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Homework_18.Models;
using Homework_18.Infrastructure;

namespace Homework_18.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly BankProvider _provider = new();

        public MainWindowViewModel()
        {
            Departments = _provider.DepartmentsList();
        }

        public ObservableCollection<Department> Departments { get; set; }
        public ObservableCollection<Transaction> Transactions { get; set; }
        public Dictionary<string, decimal> ClientsList { get; set; }
        public List<decimal> MonthsDepositList { get; set; }
        public string ClientsName { get; set; }
        public string FundsInfo { get; set; }
        public string LoanInfo { get; set; }
        public string DepositInfo { get; set; }
        public string DepTypeInfo { get; set; }
        public string LoanRateInfo { get; set; }
        public string DepRateInfo { get; set; }
        public string TransferTo { get; set; }
        public decimal AmountTransfer { get; set; }

        /* ------------------------------------------------------------------*/

        private Department _selectedDepartment;
        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                Set(ref _selectedDepartment, value);

                SelectClients(SelectedDepartment.DepartmentNameString);

                OnPropertyChanged(nameof(ClientsList));
            }
        }

        private string _selectedClient;
        public string SelectedClient
        {
            get => _selectedClient;
            set
            {
                Set(ref _selectedClient, value);

                if (!string.IsNullOrEmpty(SelectedClient))
                {
                    ShowClientsInfo(SelectedClient);
                }

                OnPropertyChanged(nameof(ClientsName));
                OnPropertyChanged(nameof(FundsInfo));
                OnPropertyChanged(nameof(LoanInfo));
                OnPropertyChanged(nameof(DepositInfo));
                OnPropertyChanged(nameof(DepTypeInfo));
                OnPropertyChanged(nameof(LoanRateInfo));
                OnPropertyChanged(nameof(DepRateInfo));
            }
        }

        private bool _popupContextMenu;
        public bool PopupContextMenu
        {
            get => _popupContextMenu;
            set
            {
                Set(ref _popupContextMenu, value);
            }
        }

        private bool _popupTransfer;
        public bool PopupTransfer
        {
            get => _popupTransfer;
            set
            {
                if (_popupTransfer == value) 
                    return;
                _popupTransfer = value;
                OnPropertyChanged(nameof(PopupTransfer));
            }
        }

        private bool _popupLoan;
        public bool PopupLoan
        {
            get => _popupLoan;
            set
            {
                Set(ref _popupLoan, value);
            }
        }

        private bool _popupSimpDep;
        public bool PopupSimpDep
        {
            get => _popupSimpDep;
            set
            {
                Set(ref _popupSimpDep, value);
            }
        }

        private bool _popupCapDep;
        public bool PopupCapDep
        {
            get => _popupCapDep;
            set
            {
                Set(ref _popupCapDep, value);
            }
        }

        private bool _popupDepInfo;
        public bool PopupDepInfo
        {
            get => _popupDepInfo;
            set
            {
                Set(ref _popupDepInfo, value);
            }
        }


        /* ------------------------------------------------------------------*/

        #region Commands

        private ICommand _depositInfoCommand;
        public ICommand DepositInfoCommand => _depositInfoCommand ?? new RelayCommand(ShowDepositInfo, null);

        /// <summary>
        /// Close application
        /// </summary>
        private ICommand _closeApplicationCommand;
        public ICommand CloseApplicationCommand => (_closeApplicationCommand ?? new RelayCommand
            (() => Application.Current.Shutdown()));

        /// <summary>
        /// About program message
        /// </summary>
        private ICommand _aboutProgramCommand;
        public ICommand AboutProgramCommand => _aboutProgramCommand ?? new RelayCommand
            (() => MessageBox.Show("MyBank v.0.9", Application.Current.MainWindow.Title, MessageBoxButton.OK, MessageBoxImage.Information));

        /// <summary>
        /// Debug mode
        /// </summary>
        private RelayCommand _pauseProgramCommand;
        public RelayCommand PauseProgramCommand
        {
            get
            {
                return _pauseProgramCommand ??= new RelayCommand(() =>
                { });
            }
        }

        #endregion

        /* ------------------------------------------------------------------*/

        #region Methods

        /// <summary>
        /// Select clients from specified department
        /// </summary>
        /// <param name="departmentName"></param>
        private void SelectClients(string departmentName)
        {
            int depId = _provider.GetDepartmentId(departmentName);
            ClientsList = _provider.ShowClients(depId);
        }

        private void ShowClientsInfo(string clientData)
        {
            string clientName = StringExtensions.ClientNameParse(clientData);
            int clientId = _provider.GetClientId(clientName);
            int departmentId = _provider.GetDepartmentId(SelectedDepartment.DepartmentNameString);

            _provider.GetClientInfo(clientId, out var clientFunds, out var clientLoan, out var clientDeposit,
                out string clientDepositType);

            _provider.GetDepartmentInfo(departmentId, out var departmentLoanRate, out var departmentDepositRate);

            ClientsName = clientName;
            FundsInfo = clientFunds.ToString();
            LoanInfo = clientLoan.ToString();
            DepositInfo = clientDeposit.ToString();
            DepTypeInfo = clientDepositType;
            LoanRateInfo = departmentLoanRate.ToString();
            DepRateInfo = departmentDepositRate.ToString();
        }

        private void ShowDepositInfo()
        {
            if (string.IsNullOrEmpty(ClientsName))
            {
                MessageBox.Show("Please select a client", "Clients information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (decimal.Parse(DepositInfo) == 0)
                {
                    MessageBox.Show("No information available", "Deposit information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                MonthList();
                MessageBox.Show("Info is here", "Deposit information", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                void MonthList()
                {
                    int clientId = _provider.GetClientId(ClientsName);
                    MonthsDepositList = _provider.DepositInfo(clientId, DepTypeInfo,
                        int.Parse(DepRateInfo)).ToList();

                }
            }
        }


        #endregion
    }
}
