using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Data.SqlClient;

namespace Homework_17
{
    #region HW17
    // Use ADO.NET
    #endregion

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GridViewColumnHeader _listViewSortCol;
        private SortAdorner _listViewSortAdorner;
        private readonly Core core = new();
        private readonly Log log = new();

        public static (int id, string name, double funds, string department,
            double loan, double deposit, string depositType) clientData;

        public static (int id, string name, int loanRate, int depositRate) departmentData;

        public MainWindow()
        {
            InitializeComponent();

            core.Transaction += Core_Transaction;

            transList.ItemsSource = log.logFile;

            // show list of departments
            BankList.ItemsSource = SqlQueries.DepartmentsList();
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

                // show clients in department
                ClientList.ItemsSource = SqlQueries.ClientsList(depName);
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
            log.AddToListLog(message);
            log.AddToDbLog(clientId, message);
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
            double amountSimpDeposit;

            try
            {
                bool result = double.TryParse(amountSimpDepTextBox.Text, out amountSimpDeposit);
                core.checkWrongAmount(result);

                // check the client have enough money to make deposit
                bool checkFunds = core.CheckSuffAmount(clientData.funds, double.Parse(amountSimpDepTextBox.Text));
                core.checkFundsPositive(checkFunds);
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
            core.MakeSimpleDeposit(clientData.id, amountSimpDeposit);

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
            double amountCapDeposit;

            try
            {
                bool result = double.TryParse(amountCapDepTextBox.Text, out amountCapDeposit);
                core.checkWrongAmount(result);

                // check the client have enough money to make deposit
                bool checkFunds = core.CheckSuffAmount(clientData.funds, double.Parse(amountCapDepTextBox.Text));
                core.checkFundsPositive(checkFunds);
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
            core.MakeCapitalizedDeposit(clientData.id, amountCapDeposit);

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
            double amountLoan;
            try
            {
                bool result = double.TryParse(amountLoanTextBox.Text, out amountLoan);
                core.checkWrongAmount(result);
            }
            catch (WrongAmountException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double newFunds = clientData.funds + amountLoan;
            double newLoan = clientData.loan + amountLoan;

            clientData.funds = newFunds;
            clientData.loan = newLoan;

            ShowClientsInfo();

            // get loan
            core.GetLoan(clientData.id, amountLoan);

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

            if (clientData.name == recipient)
            {
                MessageBox.Show("You cannot make a transfer to yourself", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double amountTransfer;

            try
            {
                bool result = double.TryParse(amountTransferTextBox.Text, out amountTransfer);
                core.checkWrongAmount(result);

                // check the sender have enough money to make transfer
                bool checkFunds = core.CheckSuffAmount(clientData.funds, double.Parse(amountTransferTextBox.Text));
                core.checkFundsPositive(checkFunds);
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
            core.TransferFunds(clientData.id, recipient, amountTransfer);

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
            if (!core.HasDeposit(clientData.id))
            {
                MessageBox.Show("No information available", "Deposit information", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return;
            }

            double[] months = core.DepositInfo(clientData.id, clientData.depositType, departmentData.depositRate);

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

        private void ShowClientsInfo()
        {
            clientData.id = SqlQueries.GetClientId(clientData.name);
            ClientNameInfo.Text = clientData.name;
            clientData.funds = SqlQueries.GetFundsAmount(clientData.id);
            FundsInfo.Text = clientData.funds.ToString();
            DepInfo.Text = clientData.department = SqlQueries.GetClientDepName(clientData.id);
            LoanRateInfo.Text = SqlQueries.GetLoanRate(clientData.id).ToString();
            clientData.loan = SqlQueries.GetLoanAmount(clientData.id);
            LoanInfo.Text = clientData.loan.ToString();
            DepRateInfo.Text = SqlQueries.GetDepositRate(clientData.id).ToString();
            DepTypeInfo.Text = clientData.depositType;

            departmentData.loanRate = SqlQueries.GetLoanRate(clientData.id);
            departmentData.depositRate = SqlQueries.GetDepositRate(clientData.id);
        }

        private void RefreshClientsList()
        {
            ClientNameInfo.Text = clientData.name;
            clientData.funds = SqlQueries.GetFundsAmount(clientData.id);
            FundsInfo.Text = clientData.funds.ToString();
            clientData.loan = SqlQueries.GetLoanAmount(clientData.id);
            LoanInfo.Text = clientData.loan.ToString();
            ClientList.ItemsSource = SqlQueries.ClientsList(clientData.department);
        }


        private void MenuItemDepInfo_OnClick(object sender, RoutedEventArgs e)
        {
            pDepInfo.IsOpen = false;
        }

        private void MenuItem_OnClick_Debug(object sender, RoutedEventArgs e) { }

        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("MyBank v.0.4", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UsersColumnHeader_OnClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (_listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(_listViewSortCol).Remove(_listViewSortAdorner);
                ClientList.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (_listViewSortCol == column && _listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            _listViewSortCol = column;
            _listViewSortAdorner = new SortAdorner(_listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(_listViewSortCol).Add(_listViewSortAdorner);
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
            if (Direction == ListSortDirection.Descending)
                geometry = descGeometry;
            drawingContext.DrawGeometry(Brushes.Black, null, geometry);

            drawingContext.Pop();
        }
    }
}
