using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Homework_18
{
    #region HW18
    // Use Entity Framework
    #endregion

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GridViewColumnHeader listViewSortCol;
        private SortAdorner listViewSortAdorner;
        private readonly Log log = new();
        private readonly BankProvider provider = new();
        private readonly Core core = new();

        public MainWindow()
        {
            InitializeComponent();

            provider.Transaction += Core_Transaction;
            transList.ItemsSource = log.logFile;

            // show list of departments
            BankList.ItemsSource = provider.ShowDepartments();
        }

        /// <summary>
        /// Show clients in current bank department
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BankList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string depName = BankList.SelectedItem.ToString();

            // get department ID
            core.departmentData.id = provider.GetDepartmentId(depName);

            // show clients in department
            ClientList.ItemsSource = provider.ShowClients(core.departmentData.id);
        }

        /// <summary>
        /// Show current client info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientInfo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientList.SelectedItems.Count != 0)
            {
                core.clientData.name = StringExtensions.ClientNameParse(ClientList.SelectedItem.ToString());

                ShowClientsInfo();
            }
        }

        private void Core_Transaction(int clientId, string message)
        {
            log.AddToLog(message);
            log.AddToDbLog(clientId, message);
        }

        private void MenuItem_OnClick_Debug(object sender, RoutedEventArgs e) { }

        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            _ = MessageBox.Show("MyBank v.0.9", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Right button menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientList_OnPreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                ContextMenu cm = FindResource("CmButton") as ContextMenu;
                cm.PlacementTarget = sender as Button;
                cm.IsOpen = true;
            }
        }

        /// <summary>
        /// Simple deposit popup menu enable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSimpleDeposit_OnClick(object sender, RoutedEventArgs e)
        {
            pSimpDep.IsOpen = true;
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
                _ = provider.CheckWrongAmount(result);

                // check the client have enough money to make deposit
                bool checkFunds = provider.CheckSuffAmount(core.clientData.funds, amountSimpDeposit);
                _ = provider.CheckFundsPositive(checkFunds);
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
            provider.MakeSimpleDeposit(core.clientData.id, amountSimpDeposit);

            RefreshClientsList();

            pSimpDep.IsOpen = false;

            _ = MessageBox.Show("Success", "Simple deposit", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        /// <summary>
        /// Capitalized deposit popup menu enable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemCapitalizedDeposit_OnClick(object sender, RoutedEventArgs e)
        {
            pCapDep.IsOpen = true;
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
                _ = provider.CheckWrongAmount(result);

                // check the client have enough money to make deposit
                bool checkFunds = provider.CheckSuffAmount(core.clientData.funds, amountCapDeposit);
                _ = provider.CheckFundsPositive(checkFunds);
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
            provider.MakeCapitalizedDeposit(core.clientData.id, amountCapDeposit);

            RefreshClientsList();

            pCapDep.IsOpen = false;

            _ = MessageBox.Show("Success", "Capitalized deposit", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        /// <summary>
        /// Loan popup menu enable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemLoan_OnClick(object sender, RoutedEventArgs e)
        {
            pLoan.IsOpen = true;
        }

        /// <summary>
        /// Get a loan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemGetLoan_OnClick(object sender, RoutedEventArgs e)
        {
            core.clientData.name = StringExtensions.ClientNameParse(ClientList.SelectedItem.ToString());

            // parse loan amount
            decimal amountLoan;

            try
            {
                bool result = decimal.TryParse(amountLoanTextBox.Text, out amountLoan);
                _ = provider.CheckWrongAmount(result);
            }
            catch (WrongAmountException ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // get loan
            provider.GetLoan(core.clientData.id, amountLoan);

            RefreshClientsList();

            pLoan.IsOpen = false;

            _ = MessageBox.Show("Success", "Get loan", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        /// <summary>
        /// Transfer popup menu enable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemTransfer_OnClick(object sender, RoutedEventArgs e)
        {
            pTransfer.IsOpen = true;
            transferTo.ItemsSource = ClientList.ItemsSource;
        }

        /// <summary>
        /// Make a transfer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemMakeTransfer_OnClick(object sender, RoutedEventArgs e)
        {
            string recipient = StringExtensions.ClientNameParse(transferTo.SelectedItem.ToString());
            int recipientId = provider.GetClientId(recipient);

            if (core.clientData.name == recipient)
            {
                _ = MessageBox.Show("You cannot make a transfer to yourself", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            decimal amountTransfer;

            try
            {
                bool result = decimal.TryParse(amountTransferTextBox.Text, out amountTransfer);
                _ = provider.CheckWrongAmount(result);

                // check the sender have enough money to make transfer
                bool checkFunds = provider.CheckSuffAmount(core.clientData.funds, amountTransfer);
                _ = provider.CheckFundsPositive(checkFunds);
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
            provider.TransferFunds(core.clientData.id, recipientId, amountTransfer);

            RefreshClientsList();

            pTransfer.IsOpen = false;

            _ = MessageBox.Show("Transfer completed", "Funds transfer", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void ShowClientsInfo()
        {
            core.clientData.id = provider.GetClientId(core.clientData.name);
            ClientNameInfo.Text = core.clientData.name;

            provider.GetClientInfo(core.clientData.id, out core.clientData.funds, out core.clientData.loan, out core.clientData.deposit,
                out core.clientData.depositType);

            provider.GetDepartmentInfo(core.departmentData.id, out core.departmentData.loanRate, out core.departmentData.depositRate);

            FundsInfo.Text = core.clientData.funds.ToString();
            LoanInfo.Text = core.clientData.loan.ToString();
            DepositInfo.Text = core.clientData.deposit.ToString();
            DepTypeInfo.Text = core.clientData.depositType;
            LoanRateInfo.Text = core.departmentData.loanRate.ToString();
            DepRateInfo.Text = core.departmentData.depositRate.ToString();
        }

        private void RefreshClientsList()
        {
            core.RefreshClientsInfo();

            ClientNameInfo.Text = core.clientData.name;

            FundsInfo.Text = core.clientData.funds.ToString();
            LoanInfo.Text = core.clientData.loan.ToString();
            DepositInfo.Text = core.clientData.deposit.ToString();

            ClientList.ItemsSource = provider.ShowClients(core.departmentData.id);
        }


        /// <summary>
        /// Show deposit info popup menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDepInfo_OnClick(object sender, RoutedEventArgs e)
        {
            if (ClientList.SelectedItems.Count != 0)
            {
                if (!provider.HasDeposit(core.clientData.id))
                {
                    _ = MessageBox.Show("No information available", "Deposit information", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    return;
                }

                MonthList.ItemsSource = core.MonthList();

                pDepInfo.IsOpen = true;
            }
            else
            {
                _ = MessageBox.Show("Please select a client", "Clients information", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }
        }

        private void MenuItemDepInfo_OnClick(object sender, RoutedEventArgs e)
        {
            pDepInfo.IsOpen = false;
        }

        private void UsersColumnHeader_OnClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                ClientList.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            ClientList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }
    }

    public class SortAdorner : Adorner
    {
        private static readonly Geometry ascGeometry =
            Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

        private static readonly Geometry descGeometry =
            Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

        public ListSortDirection Direction { get; private set; }

        public SortAdorner(UIElement element, ListSortDirection dir)
            : base(element)
        {
            Direction = dir;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (AdornedElement.RenderSize.Width < 20)
                return;

            TranslateTransform transform = new TranslateTransform
            (
                AdornedElement.RenderSize.Width - 15,
                (AdornedElement.RenderSize.Height - 5) / 2
            );
            drawingContext.PushTransform(transform);

            Geometry geometry = ascGeometry;
            if (this.Direction == ListSortDirection.Descending)
                geometry = descGeometry;
            drawingContext.DrawGeometry(Brushes.Black, null, geometry);

            drawingContext.Pop();
        }
    }
}
