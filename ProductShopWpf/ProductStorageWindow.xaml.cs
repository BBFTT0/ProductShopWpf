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
    /// Логика взаимодействия для ProductStorageWindow.xaml
    /// </summary>
    public partial class ProductStorageWindow : Window
    {
        private List<Product> products;
        private DatabaseManager dbManager;
        public ProductStorageWindow()
        {
            InitializeComponent();
            dbManager = new DatabaseManager("NOUT", "ProductShopWpf", "NAMI", "namissms");
            LoadProduct();
        }
        private void LoadProduct()
        {//Получение товара из базы данных с помощью DatabaseManager
            products =dbManager.GetProducts();
            productListBox.ItemsSource=products;
        }
        private void ProductListBox_SelectedChanged(object sender, EventArgs e)
        {
            deleteProductButton.IsEnabled= productListBox.SelectedItem!=null;
        }
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow addProductWindow = new AddProductWindow(dbManager);
            addProductWindow.ShowDialog();
            //Перезагрузка списка товаров после добавления
            LoadProduct();
        }
        private void EditButton_Click(Object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Редактирование данных товара");
        }
        private void DeleteProductButton_Click(Object obj, RoutedEventArgs e)
        {//Получение модификатора выбранного товара
            int selectedProductId = ((Product)productListBox.SelectedItem).ProductId;
            //Удаление выбранного товара из базы данных
            dbManager.DeleteProduct(selectedProductId);
            //Перезагрузка списка товаров после удаления
            LoadProduct();
        }

    }
}
