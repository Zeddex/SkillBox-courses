using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Homework_15
{
    #region HW15

    // Доработать приложение 14 модуля.
    // Создать собственные исключения и добавить их обработку в предыдущий проект.
    // Подумать над использованием методов-расширений и перегрузках операций.
    // Вынести основную логику в отдельную(ые) библиотеку.

    #endregion

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private Core core = new Core();
        private Log log = new Log();

        public MainWindow()
        {
            InitializeComponent();

            core.Transaction += Core_Transaction;
            bankList.ItemsSource = core.CreateBank();
            transList.ItemsSource = log.logFile;
        }

        private void Core_Transaction(string message)
        {
            log.AddToLog(message);
        }

        private void MenuItem_OnClick_Debug(object sender, RoutedEventArgs e) { }

        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("MyBank v.0.2", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
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
        /// Refresh client's data
        /// </summary>
        void RefreshList()
        {
            // refresh clients list
            var bankDep = bankList.SelectedItem as BankDep;
            clientList.ItemsSource = (bankDep.Clients).Where(x => x != null);

            // refresh clients info list
            clientInfo.ItemsSource = clientList.SelectedItems;
            CollectionViewSource.GetDefaultView(clientList.SelectedItems).Refresh();
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
            Client currentClient = clientList.SelectedItem as Client;
            uint amountSimpDeposit;

            try
            {
                bool result = UInt32.TryParse(amountSimpDepTextBox.Text, out amountSimpDeposit);
                core.checkWrongAmount(result);

                // check the client have enough money to make deposit
                bool checkFunds = core.CheckSuffAmount(currentClient, UInt32.Parse(amountSimpDepTextBox.Text));
                core.checkFundsPositive(checkFunds);
            }
            catch (InsufficientFundsException)
            {
                MessageBox.Show("Insufficient funds", "Insufficient funds", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            catch(WrongAmountException)
            {
                MessageBox.Show("Wrong amount", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // make simple deposit
            core.MakeSimpleDeposit(currentClient, amountSimpDeposit);

            pSimpDep.IsOpen = false;

            RefreshList();

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
            Client currentClient = clientList.SelectedItem as Client;
            uint amountCapDeposit;

            try
            {
                bool result = UInt32.TryParse(amountCapDepTextBox.Text, out amountCapDeposit);
                core.checkWrongAmount(result);

                // check the client have enough money to make deposit
                bool checkFunds = core.CheckSuffAmount(currentClient, UInt32.Parse(amountCapDepTextBox.Text));
                core.checkFundsPositive(checkFunds);
            }
            catch (InsufficientFundsException)
            {
                MessageBox.Show("Insufficient funds", "Insufficient funds", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            catch (WrongAmountException)
            {
                MessageBox.Show("Wrong amount", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // make capitalized deposit
            core.MakeCapitalizedDeposit(currentClient, amountCapDeposit);

            pCapDep.IsOpen = false;

            RefreshList();

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
            Client currentClient = clientList.SelectedItem as Client;
            uint amountLoan;

            try
            {
                bool result = UInt32.TryParse(amountLoanTextBox.Text, out amountLoan);
                core.checkWrongAmount(result);
            }
            catch (WrongAmountException)
            {
                MessageBox.Show("Wrong amount", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // get loan
            core.GetLoan(currentClient, amountLoan);

            pLoan.IsOpen = false;

            RefreshList();

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
            Client currentClient = clientList.SelectedItem as Client;
            Client recipient = transferTo.SelectedItem as Client;

            if (currentClient == transferTo.SelectedItem)
            {
                MessageBox.Show("You cannot make a transfer to yourself", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            uint amountTransfer;

            try
            {
                bool result = UInt32.TryParse(amountTransferTextBox.Text, out amountTransfer);
                core.checkWrongAmount(result);

                // check the sender have enough money to make transfer
                bool checkFunds = core.CheckSuffAmount(currentClient, UInt32.Parse(amountTransferTextBox.Text));
                core.checkFundsPositive(checkFunds);
            }
            catch (InsufficientFundsException)
            {
                MessageBox.Show("Insufficient funds", "Insufficient funds", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            catch (WrongAmountException)
            {
                MessageBox.Show("Wrong amount", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // transfer funds
            core.TransferFunds(currentClient, recipient, amountTransfer);

            pTransfer.IsOpen = false;

            RefreshList();

            MessageBox.Show("Transfer completed", "Funds transfer", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        /// <summary>
        /// Show deposit info popup menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDepInfo_OnClick(object sender, RoutedEventArgs e)
        {
            Client currentClient = clientList.SelectedItem as Client;

            if (currentClient.IsDeposit == Deposit.No)
            {
                MessageBox.Show("No information available", "Deposit information", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return;
            }

            double[] months = core.DepositInfo(currentClient);

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

        private void MenuItemDepInfo_OnClick(object sender, RoutedEventArgs e)
        {
            pDepInfo.IsOpen = false;
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
                var clients = (e.OriginalSource as ListBox).SelectedItem as BankDep;
                clientList.ItemsSource = clients.Clients;
            }
        }

        /// <summary>
        /// Show current client info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientInfo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (clientList.SelectedItems != null)
            {
                var client = (e.OriginalSource as ListBox).SelectedItems;
                clientInfo.ItemsSource = client;
            }
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
