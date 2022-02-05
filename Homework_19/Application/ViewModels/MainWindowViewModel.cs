using System;
using Domain.Entities;
using Domain.Ext;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Application.Queries;
using Application.Commands;
using MediatR;
using Persistence.Models;

namespace Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly BankProvider _provider = new();
        private readonly IMediator _mediator;
        private static Log _log;
        //private static readonly Log _log = new Log(_mediator);
        //private static readonly Log _log = new();

        public List<Department> Departments { get; set; }
        public List<string> Transactions { get; set; }
        public Dictionary<string, decimal> ClientsList { get; set; }
        public string ClientsName { get; set; }
        public string Recipient { get; set; }
        public string FundsInfo { get; set; }
        public string LoanInfo { get; set; }
        public string DepositInfo { get; set; }
        public string DepTypeInfo { get; set; }
        public string LoanRateInfo { get; set; }
        public string DepRateInfo { get; set; }
        public string AmountTransfer { get; set; }
        public string LoanAmount { get; set; }
        public string SimpleDepositAmount { get; set; }
        public string CapDepositAmount { get; set; }
        private Department _selectedDepartment;

        public MainWindowViewModel(IMediator mediator)
        {
            _mediator = mediator;
            _log = new Log(mediator);
            Transactions = _log.logFile;

            if (_provider.CheckConnection())
            {
                Departments = _mediator.Send(new GetDepartmentsList.Query()).Result;
                _provider.Transaction += Core_Transaction;      // TODO не работает событие
            }

            else
            {
                MessageBox.Show("DataBase Connection Error!", "DataBase Connection Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Core_Transaction(int clientId, string message)
        {
            _log.AddToLog(message);
            _log.AddToDbLog(clientId, message);
        }

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

        private List<decimal> _monthsDepositList;
        public List<decimal> MonthsDepositList
        {
            get => _monthsDepositList;
            set
            {
                Set(ref _monthsDepositList, value);
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

        private bool _popupTransfer;
        public bool PopupTransfer
        {
            get => _popupTransfer;
            set
            {
                if (_popupTransfer == value)
                {
                    return;
                }

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

        #region Commands

        private ICommand _depositInfoCommand;
        public ICommand DepositInfoCommand => _depositInfoCommand ?? new RelayCommand(ShowDepositInfo);

        private ICommand _closeDepInfoCommand;
        public ICommand CloseDepInfoCommand => _closeDepInfoCommand ?? new RelayCommand(() => PopupDepInfo = false);

        /// <summary>
        /// Close application
        /// </summary>
        private ICommand _closeApplicationCommand;
        public ICommand CloseApplicationCommand => _closeApplicationCommand ?? new RelayCommand(() => System.Windows.Application.Current.Shutdown());

        /// <summary>
        /// About program message
        /// </summary>
        private ICommand _aboutProgramCommand;
        public ICommand AboutProgramCommand => _aboutProgramCommand ?? new RelayCommand
            (() => MessageBox.Show("MyBank v.1.0", System.Windows.Application.Current.MainWindow.Title, MessageBoxButton.OK, MessageBoxImage.Information));

        private ICommand _popupTransferMenuCommand;
        public ICommand PopupTransferMenuCommand => _popupTransferMenuCommand ?? new RelayCommand(() => PopupTransfer = true);

        private ICommand _popupSimpDepMenuCommand;
        public ICommand PopupSimpDepMenuCommand => _popupSimpDepMenuCommand ?? new RelayCommand(() => PopupSimpDep = true);

        private ICommand _popupCapDepMenuCommand;
        public ICommand PopupCapDepMenuCommand => _popupCapDepMenuCommand ?? new RelayCommand(() => PopupCapDep = true);

        private ICommand _popupLoanMenuCommand;
        public ICommand PopupLoanMenuCommand => _popupLoanMenuCommand ?? new RelayCommand(() => PopupLoan = true);

        private ICommand _transferCommand;
        public ICommand TransferCommand => _transferCommand ?? new RelayCommand(MakeTransfer);

        private ICommand _getLoanCommand;
        public ICommand GetLoanCommand => _getLoanCommand ?? new RelayCommand(GetLoan);

        private ICommand _simpleDepositCommand;
        public ICommand SimpleDepositCommand => _simpleDepositCommand ?? new RelayCommand(MakeSimpleDeposit);

        private ICommand _capDepositCommand;
        public ICommand CapDepositCommand => _capDepositCommand ?? new RelayCommand(MakeCapDeposit);

        /// <summary>
        /// Debug mode
        /// </summary>
        private RelayCommand _pauseProgramCommand;

        public RelayCommand PauseProgramCommand => _pauseProgramCommand ??= new RelayCommand(() => { });
        #endregion

        #region Methods

        /// <summary>
        /// Select clients from specified department
        /// </summary>
        /// <param name="departmentName"></param>
        private void SelectClients(string departmentName)
        {
            int depId = _mediator.Send(new GetDepartmentId.Query(departmentName)).Result;

            ClientsList = _mediator.Send(new GetClientsByDepId.Query(depId)).Result;
        }

        private void ShowClientsInfo(string clientData)
        {
            string clientName = StringExtensions.ClientNameParse(clientData);
            int clientId = _mediator.Send(new GetClientIdByName.Query(clientName)).Result;
            int departmentId = _mediator.Send(new GetDepartmentId.Query(SelectedDepartment.DepartmentNameString)).Result;

            decimal clientFunds = _mediator.Send(new GetClientFunds.Query(clientId)).Result;
            decimal clientLoan = _mediator.Send(new GetClientLoan.Query(clientId)).Result;
            decimal clientDeposit = _mediator.Send(new GetClientDeposit.Query(clientId)).Result;
            string clientDepositType = _mediator.Send(new GetClientDepositType.Query(clientId)).Result;
            int departmentLoanRate = _mediator.Send(new GetDepartmentLoanRate.Query(departmentId)).Result;
            int departmentDepositRate = _mediator.Send(new GetDepartmentDepositRate.Query(departmentId)).Result;

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
                PopupDepInfo = true;

                void MonthList()
                {
                    int clientId = _mediator.Send(new GetClientIdByName.Query(ClientsName)).Result;
                    MonthsDepositList = _mediator.Send(new GetDepositAmount.Query(clientId, DepTypeInfo, int.Parse(DepRateInfo))).Result;
                }
            }
        }

        private void MakeTransfer()
        {
            string recipient = StringExtensions.ClientNameParse(Recipient);
            int recipientId = _mediator.Send(new GetClientIdByName.Query(recipient)).Result;
            int clientId = _mediator.Send(new GetClientIdByName.Query(ClientsName)).Result;
            decimal amountTransfer;
            decimal clientsFunds = decimal.Parse(FundsInfo);

            if (recipient == ClientsName)
            {
                _ = MessageBox.Show("You cannot make a transfer to yourself", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                bool result = decimal.TryParse(AmountTransfer, out amountTransfer);
                _ = _provider.CheckWrongAmount(result);

                // check the sender have enough money to make transfer
                bool checkFunds = _provider.CheckSuffAmount(clientsFunds, amountTransfer);
                _ = _provider.CheckFundsPositive(checkFunds);
            }
            catch (InsufficientFundsException ex)
            {
                _ = MessageBox.Show(ex.Message, "Insufficient funds", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            catch (WrongAmountException ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // transfer funds
            _mediator.Send(new TransferFunds.Command(clientId, recipientId, amountTransfer));

            RefreshView();      // TODO refresh data

            // close popup window
            PopupTransfer = false;

            _ = MessageBox.Show("Transfer completed", "Funds transfer", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void GetLoan()
        {
            string clientName = StringExtensions.ClientNameParse(SelectedClient);
            int clientId = _mediator.Send(new GetClientIdByName.Query(clientName)).Result;
            decimal amountLoan;

            try
            {
                bool result = decimal.TryParse(LoanAmount, out amountLoan);
                _ = _provider.CheckWrongAmount(result);
            }
            catch (WrongAmountException ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // get loan
            _mediator.Send(new GetLoan.Command(clientId, amountLoan));

            RefreshView();

            // close popup window
            PopupLoan = false;

            _ = MessageBox.Show("Success", "Get loan", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void MakeSimpleDeposit()
        {
            string clientName = StringExtensions.ClientNameParse(SelectedClient);
            int clientId = _mediator.Send(new GetClientIdByName.Query(clientName)).Result;
            decimal amountSimpDeposit;
            decimal clientsFunds = decimal.Parse(FundsInfo);

            try
            {
                bool result = decimal.TryParse(SimpleDepositAmount, out amountSimpDeposit);
                _ = _provider.CheckWrongAmount(result);

                // check the client have enough money to make deposit
                bool checkFunds = _provider.CheckSuffAmount(clientsFunds, amountSimpDeposit);
                _ = _provider.CheckFundsPositive(checkFunds);
            }
            catch (InsufficientFundsException ex)
            {
                _ = MessageBox.Show(ex.Message, "Insufficient funds", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            catch (WrongAmountException ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // make simple deposit
            _mediator.Send(new MakeSimpleDeposit.Command(clientId, amountSimpDeposit));

            RefreshView();

            // close popup window
            PopupSimpDep = false;

            _ = MessageBox.Show("Success", "Simple deposit", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void MakeCapDeposit()
        {
            string clientName = StringExtensions.ClientNameParse(SelectedClient);
            int clientId = _mediator.Send(new GetClientIdByName.Query(clientName)).Result;
            decimal amountCapDeposit;
            decimal clientsFunds = decimal.Parse(FundsInfo);

            try
            {
                bool result = decimal.TryParse(CapDepositAmount, out amountCapDeposit);
                _ = _provider.CheckWrongAmount(result);

                // check the client have enough money to make deposit
                bool checkFunds = _provider.CheckSuffAmount(clientsFunds, amountCapDeposit);
                _ = _provider.CheckFundsPositive(checkFunds);
            }
            catch (InsufficientFundsException ex)
            {
                _ = MessageBox.Show(ex.Message, "Insufficient funds", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            catch (WrongAmountException ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // make simple deposit
            _mediator.Send(new MakeCapitalizedDeposit.Command(clientId, amountCapDeposit));

            RefreshView();

            // close popup window
            PopupCapDep = false;

            _ = MessageBox.Show("Success", "Capitalized deposit", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void RefreshView()
        {
            ShowClientsInfo(SelectedClient);
            SelectClients(SelectedDepartment.DepartmentNameString);

            OnPropertyChanged(nameof(ClientsList));
            OnPropertyChanged(nameof(SelectedClient));
            OnPropertyChanged(nameof(FundsInfo));
            OnPropertyChanged(nameof(LoanInfo));
            OnPropertyChanged(nameof(DepositInfo));
            OnPropertyChanged(nameof(DepTypeInfo));
        }

        #endregion
    }
}
