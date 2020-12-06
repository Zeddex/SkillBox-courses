using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Homework_12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region HW12

        // Добавить применение интерфейсов;
        // Добавить возможность:
        // — изменения
        // — удаления
        // — редактирования
        // — сортировки экземпляров Worker и Department

        #endregion

        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private Core core = new Core();
        private InOut inout = new InOut();

        public MainWindow()
        {
            InitializeComponent();
            CompanyList.Items.Add(CreateTreeItem(core.CreateOrg(5)[0]));        // Create organization with 5 sub departments
        }

        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_OnClick_Debug(object sender, RoutedEventArgs e) { }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("MyOrganization v.0.2", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
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
            CompanyList.Items.Add(CreateTreeItem(inout.LoadData()[0]));
        }

        private void MenuItem_OnClick_Save(object sender, RoutedEventArgs e)
        {
            inout.SaveData();
        }

        private void CompanyList_OnExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            if (item.Items.IsEmpty)
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

        /// <summary>
        /// Show workers in current department
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_Selected(object sender, RoutedEventArgs e)
        {
            var dep = (e.OriginalSource as TreeViewItem).Tag as Organisation;
            empList.ItemsSource = dep.Employees;
        }

        private void UsersColumnHeader_OnClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                empList.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            empList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

        /// <summary>
        /// Right button menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmpList_OnPreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                ContextMenu cm = this.FindResource("cmButton") as ContextMenu;
                cm.PlacementTarget = sender as Button;
                cm.IsOpen = true;
            }
        }

        /// <summary>
        /// Popup menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemEdit_OnClick(object sender, RoutedEventArgs e)
        {
            pEdit.IsOpen = true;

            Employee currentEmp = empList.SelectedItem as Employee;

            nameTextBox.Text = currentEmp.Name;
            ageTextBox.Text = currentEmp.Age.ToString();
            projectTextBox.Text = currentEmp.Projects.ToString();
        }

        /// <summary>
        /// Delete entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDelete_OnClick(object sender, RoutedEventArgs e)
        {
            Employee currentEmp = empList.SelectedItem as Employee;             // get current employee
            TreeViewItem tviOrg = (TreeViewItem)CompanyList.SelectedItem;       // get current org department
            Organisation currentOrg = tviOrg.Tag as Organisation;

            if (currentEmp.Position == "Administrator")
            {
                MessageBox.Show("Administrator cannot be deleted", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else if (currentEmp.Position == "CEO")
            {
                MessageBox.Show("Boss cannot be deleted", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                int elementIndex = 0;                                   // searching for employee's index in department
                foreach (var emp in currentOrg.Employees)
                {
                    if (currentEmp.Id == emp.Id)
                    {
                        break;
                    }

                    elementIndex++;
                }

                var confirm = MessageBox.Show("Are you sure you want to delete this worker?", "Delete entry", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirm == MessageBoxResult.Yes)
                {
                    core.RecalculateSalary(currentOrg.Id-1, elementIndex);     // salary recalculation and employee erasing

                    // refresh list of workers
                    empList.ItemsSource = (currentOrg.Employees).Where(x => x != null);
                }
            }
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            byte ageNum, projNum;

            Employee currentEmp = empList.SelectedItem as Employee;

            bool rightData = Byte.TryParse(ageTextBox.Text, out ageNum) &
                             Byte.TryParse(projectTextBox.Text, out projNum);
            if (rightData && ageNum > 0 && ageNum < 80 && projNum > 0 && projNum < 255)
            {
                currentEmp.Age = ageNum;
                currentEmp.Projects = projNum;
            }
            else
            {
                MessageBox.Show("Incorrect values", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                pEdit.IsOpen = false;
            }

            currentEmp.Name = nameTextBox.Text;

            pEdit.IsOpen = false;       // close popup window

            // refresh list of workers
            TreeViewItem tviOrg = (TreeViewItem)CompanyList.SelectedItem;
            Organisation currentOrg = tviOrg.Tag as Organisation;
            empList.ItemsSource = (currentOrg.Employees).Where(x => x != null);
        }
    }

    /// <summary>
    /// Column sorting
    /// </summary>
    public class SortAdorner : Adorner
    {
        private static Geometry ascGeometry =
            Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

        private static Geometry descGeometry =
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
