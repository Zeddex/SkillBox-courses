using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

namespace Homework_11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region HW11

        //Спроектировать информационную систему позволяющей работать со следующей структурой:
        //Организация, в которой есть департаменты и сотрудники.
        //Наполнение деталями предлагается реализовать самостоятельно
        //Наполнение сотрудниками и департаментами происходит автоматически из файла *.txt, 
        //                                                           предпочтительнее *.xml или *.json 
        //
        // Сотрудники делятся на несколько групп, разделенных должностями и оплатой труда
        // Есть 
        //   сотрудники - управленцы (например: директор, Первый заместитель директора, начальник ведомства, 
        //                                      начальник департамента)
        // У интернов оплата труда фиксированная и определяется при приёме (например $500 в месяц)
        // У сотрудников - рабочих оплата труда почасовая и определяется при приёме (например $12 в час)
        // У сотрудников - управленцев оплата труда составляет 15% от общей выплаченной суммы всем сотрудникам 
        // числящихся в его отделе, но не менее $1300. 
        //
        // Структура организации следующая:
        // Организация, состоит из ведомств в которые включены департаменты
        // У каждого ведомства и департамента есть свой начальник.
        // Директор руководит Организацией
        // 
        // Реализовать и продемонстрировать работу информационной системы
        // В консоли или с использованием UI

        #endregion

        private Core core = new Core();
        
        public MainWindow()
        {
            InitializeComponent();

            CompanyList.Items.Add(CreateTreeItem(core.CreateOrg(5)[0]));
        }

        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_OnClick_Debug(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("MyOrganisation v.0.1", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ClearData()
        {
            core.ClearData();
            empList.ItemsSource = null;
            CompanyList.ItemsSource = null;
            empList.Items.Clear();
            CompanyList.Items.Clear();
        }

        private void MenuItem_OnClick_Generate(object sender, RoutedEventArgs e)
        {
            ClearData();
            CompanyList.Items.Add(CreateTreeItem(core.CreateOrg(5)[0]));
        }

        private void MenuItem_OnClick_Clear(object sender, RoutedEventArgs e)
        {
            ClearData();
        }

        private void MenuItem_OnClick_Load(object sender, RoutedEventArgs e)
        {
            ClearData();
            CompanyList.Items.Add(CreateTreeItem(core.LoadData()[0]));
        }

        private void MenuItem_OnClick_Save(object sender, RoutedEventArgs e)
        {
            core.SaveData();
        }

        private void CompanyList_OnExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            if (item.Items[0] != null)
                return;
            item.Items.Clear();
            var d = item.Tag as Organisation;
            var subDepartments = core.GetSubDepts(d.Id);
            
            foreach (Organisation dep in subDepartments)
                item.Items.Add(CreateTreeItem(dep));
        }

        private TreeViewItem CreateTreeItem(Organisation dept)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = dept.Title;
            item.Tag = dept;
            var subDept = core.GetSubDepts(dept.Id);
            if (subDept.Count > 0)
            {
                item.Items.Add(null);
            }
            item.Selected += Item_Selected;
            return item;
        }

        private void Item_Selected(object sender, RoutedEventArgs e)
        {
            var dep = (e.OriginalSource as TreeViewItem).Tag as Organisation;
            empList.ItemsSource = dep.Employees;
        }
    }
}
