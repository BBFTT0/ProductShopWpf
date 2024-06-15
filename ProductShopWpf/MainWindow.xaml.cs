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

namespace ProductShopWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseManager dbManager;
        public MainWindow()
        {
            InitializeComponent();
            string server = "NOUT";
            string database = "ProductShopWpf";
            string username = "NAMI";
            string password = "namissms";
            dbManager = new DatabaseManager(server,database,username,password);
        }
        public void LoginButton_Click (object sender, RoutedEventArgs e)
        {
            string username=usernameTextBox.Text;
            string password = passwordBox.Password;
            if (dbManager.ValidateUser(username, password))
            {
                ProductStorageWindow productStorageWindow= new ProductStorageWindow();
                productStorageWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Логин или пароль введены неправильно");
            }
        }
    }
}
