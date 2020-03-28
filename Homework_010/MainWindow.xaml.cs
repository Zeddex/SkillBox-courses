using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Homework_10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BotCore client;
        public MainWindow()
        {
            InitializeComponent();
            client = new BotCore(this);
            logList.ItemsSource = client.BotMessageLog;
        }

        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {
            client.SaveFile();
        }

        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sender/receiver bot v.0.1", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_Click_Clear(object sender, RoutedEventArgs e)
        {
            client.ClearHistory();
        }
    }
}
