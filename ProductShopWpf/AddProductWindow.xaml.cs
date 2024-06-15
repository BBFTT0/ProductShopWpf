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
using System.Windows.Shapes;

namespace ProductShopWpf
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        DatabaseManager dbManager;
        public AddProductWindow(DatabaseManager manager)
        {
            InitializeComponent();
            dbManager = manager;
        }
        public void SaveProductButton_Click(object sender, RoutedEventArgs e)
        {
            decimal price;
            if (!decimal.TryParse(priceTextBox.Text, out price) || price <= 0)
            {
                MessageBox.Show("Пожалуйста введите правильный формат цены"); // Показать сообщение об ошибке
                return;
            }

            Product newProduct = new Product
            {

                Title = titleTextBox.Text,
                Discription = discriptionTextBox.Text,
                Price = price,
            };
            dbManager.AddProduct(newProduct);
            this.Close();
        }
        private void BackButton_Click( object sender, RoutedEventArgs e )
        {//Закрывается текущее окно
            this.Close();
        }
    }
}
