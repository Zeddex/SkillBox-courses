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

        private static (int id, string name, decimal funds, string department,
            decimal loan, decimal deposit, string depositType) clientData;
        private static (int id, string name, int loanRate, int depositRate) departmentData;

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
            if (BankList.SelectedItems != null)
            {
                string depName = BankList.SelectedItem.ToString();

                // get department ID
                departmentData.id = provider.GetDepartmentId(depName);

                // show clients in department
                ClientList.ItemsSource = provider.ShowClients(departmentData.id);
            }
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
                clientData.name = ClientList.SelectedItem.ToString().TrimStart('[').Split(',')[0];

                // show clients info
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
            this.Close();
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("MyBank v.0.9", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
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
                ContextMenu cm = this.FindResource("CmButton") as ContextMenu;
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
                provider.CheckWrongAmount(result);

                // check the client have enough money to make deposit
                bool checkFunds = provider.CheckSuffAmount(clientData.funds, decimal.Parse(amountSimpDepTextBox.Text));
                provider.checkFundsPositive(checkFunds);
            }
            catch (InsufficientFundsException ex)
            {
                MessageBox.Show(ex.Message, "Insufficient funds", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            catch (WrongAmountException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // make simple deposit
            provider.MakeSimpleDeposit(clientData.id, amountSimpDeposit);

            RefreshClientsList();

            pSimpDep.IsOpen = false;

            MessageBox.Show("Success", "Simple deposit", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
                provider.CheckWrongAmount(result);

                // check the client have enough money to make deposit
                bool checkFunds = provider.CheckSuffAmount(clientData.funds, decimal.Parse(amountCapDepTextBox.Text));
                provider.checkFundsPositive(checkFunds);
            }
            catch (InsufficientFundsException ex)
            {
                MessageBox.Show(ex.Message, "Insufficient funds", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            catch (WrongAmountException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // make capitalized deposit
            provider.MakeCapitalizedDeposit(clientData.id, amountCapDeposit);

            RefreshClientsList();

            pCapDep.IsOpen = false;

            MessageBox.Show("Success", "Capitalized deposit", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
            clientData.name = ClientList.SelectedItem.ToString().TrimStart('[').Split(',')[0];

            // parse loan amount
            decimal amountLoan;

            try
            {
                bool result = decimal.TryParse(amountLoanTextBox.Text, out amountLoan);
                provider.CheckWrongAmount(result);
            }
            catch (WrongAmountException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            decimal newFunds = clientData.funds + amountLoan;
            decimal newLoan = clientData.loan + amountLoan;

            // get loan
            provider.GetLoan(clientData.id, amountLoan);

            RefreshClientsList();

            pLoan.IsOpen = false;

            MessageBox.Show("Success", "Get loan", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
            string recipient = transferTo.SelectedItem.ToString().TrimStart('[').Split(',')[0];
            int recipientId = provider.GetClientId(recipient);

            if (clientData.name == recipient)
            {
                MessageBox.Show("You cannot make a transfer to yourself", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            decimal amountTransfer;

            try
            {
                bool result = decimal.TryParse(amountTransferTextBox.Text, out amountTransfer);
                provider.CheckWrongAmount(result);

                // check the sender have enough money to make transfer
                bool checkFunds = provider.CheckSuffAmount(clientData.funds, UInt32.Parse(amountTransferTextBox.Text));
                provider.checkFundsPositive(checkFunds);
            }
            catch (InsufficientFundsException ex)
            {
                MessageBox.Show(ex.Message, "Insufficient funds", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            catch (WrongAmountException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // transfer funds
            provider.TransferFunds(clientData.id, recipientId, amountTransfer);

            RefreshClientsList();

            pTransfer.IsOpen = false;

            MessageBox.Show("Transfer completed", "Funds transfer", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
                if (!provider.HasDeposit(clientData.id))
                {
                    MessageBox.Show("No information available", "Deposit information", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    return;
                }

                decimal[] months =
                    provider.DepositInfo(clientData.id, clientData.depositType, departmentData.depositRate);

                month1.Text = months[0].ToString();
                month2.Text = months[1].ToString();
                month3.Text = months[2].ToString();
                month4.Text = months[3].ToString();
                month5.Text = months[4].ToString();
                month6.Text = months[5].ToString();
                month7.Text = months[6].ToString();
                month8.Text = months[7].ToString();
                month9.Text = months[8].ToString();
                month10.Text = months[9].ToString();
                month11.Text = months[10].ToString();
                month12.Text = months[11].ToString();

                pDepInfo.IsOpen = true;
            }
            else
            {
                MessageBox.Show("Please select a client", "Clients information", MessageBoxButton.OK,
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

        private void ShowClientsInfo()
        {
            clientData.id = provider.GetClientId(clientData.name);
            ClientNameInfo.Text = clientData.name;

            provider.GetClientInfo(clientData.id, out clientData.funds, out clientData.loan, out clientData.deposit,
                out clientData.depositType);

            provider.GetDepartmentInfo(departmentData.id, out departmentData.loanRate, out departmentData.depositRate);

            FundsInfo.Text = clientData.funds.ToString();
            LoanInfo.Text = clientData.loan.ToString();
            DepositInfo.Text = clientData.deposit.ToString();
            DepTypeInfo.Text = clientData.depositType;
            LoanRateInfo.Text = departmentData.loanRate.ToString();
            DepRateInfo.Text = departmentData.depositRate.ToString();
        }

        private void RefreshClientsList()
        {
            ClientNameInfo.Text = clientData.name;

            clientData.funds = provider.GetFundsAmount(clientData.id);
            FundsInfo.Text = clientData.funds.ToString();

            clientData.loan = provider.GetLoanAmount(clientData.id);
            LoanInfo.Text = clientData.loan.ToString();

            clientData.deposit = provider.GetDepositAmount(clientData.id);
            DepositInfo.Text = clientData.deposit.ToString();

            ClientList.ItemsSource = provider.ShowClients(departmentData.id);
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
            this.Direction = dir;
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
