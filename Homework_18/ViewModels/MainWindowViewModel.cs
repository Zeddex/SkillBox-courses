using System.Collections.Generic;
using Homework_18.Entities;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Homework_18.Models;

namespace Homework_18.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly BankProvider _provider = new();

        public ObservableCollection<Department> Departments { get; set; }
        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<Money> ClientsData { get; set; }
        public ObservableCollection<Transaction> Transactions { get; set; }
        //public int DepartmentId { get; }
        public Dictionary<string, decimal> ClientsList { get; set; }
        //private readonly ClientHandler _clientHandler;



        public MainWindowViewModel()
        {
            Departments = _provider.DepartmentsList();
        }

        private Department _selectedDepartment;
        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                Set(ref _selectedDepartment, value);

                SelectClients(SelectedDepartment.DepartmentNameString.ToString());
            }
        }

        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set => Set(ref _selectedClient, value);
        }

        #region Commands

        private ICommand _testCommand;
        public ICommand TestCommand => _testCommand ?? new RelayCommand
            (delegate { MessageBox.Show("Test", "Test", MessageBoxButton.OK, MessageBoxImage.Exclamation); }, null);

        /// <summary>
        /// Close application
        /// </summary>
        private ICommand _closeApplicationCommand;
        public ICommand CloseApplicationCommand => _closeApplicationCommand ?? new RelayCommand
            (delegate { Application.Current.Shutdown(); });

        /// <summary>
        /// About program message
        /// </summary>
        private ICommand _aboutProgramCommand;

        public ICommand AboutProgramCommand => _aboutProgramCommand ?? new RelayCommand
            (delegate { MessageBox.Show("MyBank v.0.9", Application.Current.MainWindow.Title, MessageBoxButton.OK, MessageBoxImage.Information); });

        /// <summary>
        /// Debug mode
        /// </summary>
        private RelayCommand _pauseProgramCommand;
        public RelayCommand PauseProgramCommand
        {
            get
            {
                return _pauseProgramCommand ??= new RelayCommand(obj =>
                { });
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Select clients from specified department
        /// </summary>
        /// <param name="departmentName"></param>
        private void SelectClients(string departmentName)
        {
            int depId = _provider.GetDepartmentId(departmentName);
            ClientsList = _provider.ShowClients(depId);
            MessageBox.Show(SelectedDepartment.DepartmentNameString.ToString(), "Test", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        #endregion
    }
}
