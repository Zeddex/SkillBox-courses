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
        private GridViewColumnHeader listViewSortCol;
        private SortAdorner listViewSortAdorner;
        private readonly Core core = new();
        private readonly Log log = new();

        public MainWindow()
        {
            InitializeComponent();


            core.Transaction += Core_Transaction;

            transList.ItemsSource = log.logFile;

            // show list of departments
            //var depList = (from dep in DB.table.AsEnumerable()
            //               select new { Department = dep["Department"] }).Distinct();

            //bankList.ItemsSource = depList;
        }

        /// <summary>
        /// Show clients in current bank department
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BankList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bankList.SelectedItems != null)
            {
                var dep = (e.OriginalSource as ListBox).SelectedItem;

                // parse department's name from object
                //string depName = Extesions.GetValueFromObj<string>(dep, "Department");

                //var clients = from client in DB.table.AsEnumerable()
                //              where (string)client["Department"] == depName
                //              select new
                //              {
                //                  ClientId = client["ClientId"],
                //                  Client = client["Client"],
                //                  Funds = client["Funds"],
                //                  Loan = client["Loan"],
                //                  Deposit = client["Deposit"],
                //                  DepositType = client["DepositType"]
                //              };

                //clientList.ItemsSource = clients;
            }
        }

        /// <summary>
        /// Show current client info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientInfo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (clientList.SelectedItems.Count != 0 && clientList.SelectedItems != null)
            {
                var currClient = (e.OriginalSource as ListBox).SelectedItem;

                // parse client's ID from object
                //int clientId = Convert.ToInt32(Extesions.GetValueFromObj<string>(currClient, "ClientId"));

                //var clientData = from client in DB.table.AsEnumerable()
                //                 where (int)client["ClientId"] == clientId
                //                 select new
                //                 {
                //                     ClientId = client["ClientId"],
                //                     Client = client["Client"],
                //                     Funds = client["Funds"],
                //                     Department = client["Department"],
                //                     LoanRate = client["LoanRate"],
                //                     DepositRate = client["DepositRate"],
                //                     DepositType = client["DepositType"]
                //                 };

                //clientInfo.ItemsSource = clientData;
            }
        }

        private void Core_Transaction(string message)
        {
            log.AddToLog(message);
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
            //Client currentClient = clientList.SelectedItem as Client;
            //uint amountSimpDeposit;

            //try
            //{
            //    bool result = UInt32.TryParse(amountSimpDepTextBox.Text, out amountSimpDeposit);
            //    core.checkWrongAmount(result);

            //    // check the client have enough money to make deposit
            //    bool checkFunds = core.CheckSuffAmount(currentClient, UInt32.Parse(amountSimpDepTextBox.Text));
            //    core.checkFundsPositive(checkFunds);
            //}
            //catch (InsufficientFundsException ex)
            //{
            //    MessageBox.Show(ex.Message, "Insufficient funds", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //    return;
            //}
            //catch (WrongAmountException ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //// make simple deposit
            //core.MakeSimpleDeposit(currentClient, amountSimpDeposit);

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
            //Client currentClient = clientList.SelectedItem as Client;
            //uint amountCapDeposit;

            //try
            //{
            //    bool result = UInt32.TryParse(amountCapDepTextBox.Text, out amountCapDeposit);
            //    core.checkWrongAmount(result);

            //    // check the client have enough money to make deposit
            //    bool checkFunds = core.CheckSuffAmount(currentClient, UInt32.Parse(amountCapDepTextBox.Text));
            //    core.checkFundsPositive(checkFunds);
            //}
            //catch (InsufficientFundsException ex)
            //{
            //    MessageBox.Show(ex.Message, "Insufficient funds", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //    return;
            //}
            //catch (WrongAmountException ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //// make capitalized deposit
            //core.MakeCapitalizedDeposit(currentClient, amountCapDeposit);

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
            var currClient = clientList.SelectedItem;

            //int clientId = Convert.ToInt32(Extesions.GetValueFromObj<string>(currClient, "ClientId"));
            //int clientFunds = Convert.ToInt32(Extesions.GetValueFromObj<string>(currClient, "Funds"));
            //int currentLoan = Convert.ToInt32(Extesions.GetValueFromObj<string>(currClient, "Loan"));

            // parse loan amount
            uint amountLoan;
            try
            {
                bool result = uint.TryParse(amountLoanTextBox.Text, out amountLoan);
                core.checkWrongAmount(result);
            }
            catch (WrongAmountException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //int newFunds = clientFunds + (int)(amountLoan);
            //int newLoan = currentLoan + (int)amountLoan;


            //var clientData = from client in DB.table.AsEnumerable()
            //                 where (int)client["ClientId"] == clientId
            //                 select new
            //                 {
            //                     ClientId = client["ClientId"],
            //                     Funds = newFunds,
            //                     Loan = newLoan,
            //                     Deposit = client["Deposit"],
            //                     DepositType = client["DepositType"]
            //                 };


            //clientInfo.ItemsSource = clientData;

            //// DataRow
            //var clientData1 = (from client in DB.table.AsEnumerable()
            //                   where (int)client["ClientId"] == clientId
            //                   select client).FirstOrDefault();

            //if (clientData != null)
            //{
            //    DB.table.Rows.Add(clientData1.ItemArray);
            //}

            //// convert DataRow to DataRowView
            //DB.row = DB.table.DefaultView[DB.table.Rows.IndexOf(clientData1)];

            //var datarowview = DB.row;

            //DB.row.BeginEdit();
            //DB.row.EndEdit();
            //DB.adapter.Update(DB.table);

            // get loan
            //core.GetLoan(currentClient, amountLoan);

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
            transferTo.ItemsSource = clientList.ItemsSource;
        }

        /// <summary>
        /// Make a transfer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemMakeTransfer_OnClick(object sender, RoutedEventArgs e)
        {
            //Client currentClient = clientList.SelectedItem as Client;
            //Client recipient = transferTo.SelectedItem as Client;

            //if (currentClient == transferTo.SelectedItem)
            //{
            //    MessageBox.Show("You cannot make a transfer to yourself", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //uint amountTransfer;

            //try
            //{
            //    bool result = UInt32.TryParse(amountTransferTextBox.Text, out amountTransfer);
            //    core.checkWrongAmount(result);

            //    // check the sender have enough money to make transfer
            //    bool checkFunds = core.CheckSuffAmount(currentClient, UInt32.Parse(amountTransferTextBox.Text));
            //    core.checkFundsPositive(checkFunds);
            //}
            //catch (InsufficientFundsException ex)
            //{
            //    MessageBox.Show(ex.Message, "Insufficient funds", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //    return;
            //}
            //catch (WrongAmountException ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //// transfer funds
            //core.TransferFunds(currentClient, recipient, amountTransfer);

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
            //Client currentClient = clientList.SelectedItem as Client;

            //if (currentClient.IsDeposit == Deposit.No)
            //{
            //    MessageBox.Show("No information available", "Deposit information", MessageBoxButton.OK,
            //        MessageBoxImage.Exclamation);
            //    return;
            //}

            //double[] months = core.DepositInfo(currentClient);

            //month1.Text = months[0].ToString();
            //month2.Text = months[1].ToString();
            //month3.Text = months[2].ToString();
            //month4.Text = months[3].ToString();
            //month5.Text = months[4].ToString();
            //month6.Text = months[5].ToString();
            //month7.Text = months[6].ToString();
            //month8.Text = months[7].ToString();
            //month9.Text = months[8].ToString();
            //month10.Text = months[9].ToString();
            //month11.Text = months[10].ToString();
            //month12.Text = months[11].ToString();

            pDepInfo.IsOpen = true;
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

        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {
            core.SaveData();
        }

        private void UsersColumnHeader_OnClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                clientList.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            clientList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
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
