using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Homework_18.Infrastructure;
using Homework_18.Models;

namespace Homework_18.View
{
    #region HW18
    // Use Entity Framework
    #endregion

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Log _log = new();
        private readonly BankProvider _provider = new();
        private readonly Core _core = new();

        public MainWindow()
        {
            InitializeComponent();

            _provider.Transaction += Core_Transaction;
            //transList.ItemsSource = log.logFile;

        }

        private void Core_Transaction(int clientId, string message)
        {
            _log.AddToLog(message);
            _log.AddToDbLog(clientId, message);
        }

    }
}
