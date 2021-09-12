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

        /// <summary>
        /// Make simple deposit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSimpDep_OnClick(object sender, RoutedEventArgs e)
        {
            decimal amountSimpDeposit;

            try
            {
                bool result = decimal.TryParse(amountSimpDepTextBox.Text, out amountSimpDeposit);
                _ = _provider.CheckWrongAmount(result);

                // check the client have enough money to make deposit
                bool checkFunds = _provider.CheckSuffAmount(_core.clientData.funds, amountSimpDeposit);
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
            _provider.MakeSimpleDeposit(_core.clientData.id, amountSimpDeposit);


            //pSimpDep.IsOpen = false;

            _ = MessageBox.Show("Success", "Simple deposit", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        /// <summary>
        /// Make capitalized deposit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemCapDep_OnClick(object sender, RoutedEventArgs e)
        {
            decimal amountCapDeposit;

            try
            {
                bool result = decimal.TryParse(amountCapDepTextBox.Text, out amountCapDeposit);
                _ = _provider.CheckWrongAmount(result);

                // check the client have enough money to make deposit
                bool checkFunds = _provider.CheckSuffAmount(_core.clientData.funds, amountCapDeposit);
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

            // make capitalized deposit
            _provider.MakeCapitalizedDeposit(_core.clientData.id, amountCapDeposit);


            //pCapDep.IsOpen = false;

            _ = MessageBox.Show("Success", "Capitalized deposit", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        /// <summary>
        /// Get a loan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemGetLoan_OnClick(object sender, RoutedEventArgs e)
        {
            //core.clientData.name = StringExtensions.ClientNameParse(ClientList.SelectedItem.ToString());

            // parse loan amount
            decimal amountLoan;

            try
            {
                bool result = decimal.TryParse(amountLoanTextBox.Text, out amountLoan);
                _ = _provider.CheckWrongAmount(result);
            }
            catch (WrongAmountException ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // get loan
            _provider.GetLoan(_core.clientData.id, amountLoan);


            //pLoan.IsOpen = false;

            _ = MessageBox.Show("Success", "Get loan", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        /// <summary>
        /// Transfer popup menu enable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemTransfer_OnClick(object sender, RoutedEventArgs e)
        {
            //pTransfer.IsOpen = true;
            //transferTo.ItemsSource = ClientList.ItemsSource;
        }

        /// <summary>
        /// Make a transfer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemMakeTransfer_OnClick(object sender, RoutedEventArgs e)
        {
            //string recipient = StringExtensions.ClientNameParse(transferTo.SelectedItem.ToString());
            //int recipientId = provider.GetClientId(recipient);

            //if (core.clientData.name == recipient)
            //{
            //    _ = MessageBox.Show("You cannot make a transfer to yourself", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //decimal amountTransfer;

            //try
            //{
            //    bool result = decimal.TryParse(amountTransferTextBox.Text, out amountTransfer);
            //    _ = provider.CheckWrongAmount(result);

            //    // check the sender have enough money to make transfer
            //    bool checkFunds = provider.CheckSuffAmount(core.clientData.funds, amountTransfer);
            //    _ = provider.CheckFundsPositive(checkFunds);
            //}
            //catch (InsufficientFundsException ex)
            //{
            //    _ = MessageBox.Show(ex.Message, "Insufficient funds", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //    return;
            //}
            //catch (WrongAmountException ex)
            //{
            //    _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //// transfer funds
            //provider.TransferFunds(core.clientData.id, recipientId, amountTransfer);

            //RefreshClientsList();

            //pTransfer.IsOpen = false;

            //_ = MessageBox.Show("Transfer completed", "Funds transfer", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }


        /// <summary>
        /// Show deposit info popup menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDepInfo_OnClick(object sender, RoutedEventArgs e)
        {
            //if (ClientList.SelectedItems.Count != 0)
            //{
            //    if (!provider.HasDeposit(core.clientData.id))
            //    {
            //        _ = MessageBox.Show("No information available", "Deposit information", MessageBoxButton.OK,
            //            MessageBoxImage.Exclamation);
            //        return;
            //    }

            //    MonthList.ItemsSource = core.MonthList();

            //    pDepInfo.IsOpen = true;
            //}
            //else
            //{
            //    _ = MessageBox.Show("Please select a client", "Clients information", MessageBoxButton.OK,
            //        MessageBoxImage.Exclamation);
            //}
        }
    }
}
