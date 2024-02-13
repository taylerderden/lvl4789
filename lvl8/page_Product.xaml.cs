using lvl8.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace lvl8
{
    /// <summary>
    /// Логика взаимодействия для page_Product.xaml
    /// </summary>
    public partial class page_Product : Page
    {
        Product product;
        public page_Product()
        {
            InitializeComponent();

            lvProduct.ItemsSource = CoreModel.init().Products.Include(x => x.ProductType).ToList();

            DataContext = product;

            cbSort.Items.Add("Дешевле");
            cbSort.Items.Add("Дороже");

            cbFiltr.Items.Add("Кровать");
            cbFiltr.Items.Add("Гарнитур");
            cbFiltr.Items.Add("Кресло");
            cbFiltr.Items.Add("Все");
        }
        private void Update()
        {
            lvProduct.ItemsSource = CoreModel.init().Products.Include(x => x.ProductType).ToList();            
        }
        private void UpdateSearch()
        {
            IEnumerable<Product> products = CoreModel.init().Products.Where(p => p.Title.Contains(tbSearch.Text));

            switch (cbSort.SelectedIndex)
            {
                case 0:
                    products = products.OrderBy(p => Convert.ToInt32(p.MinCostForAgent));
                    break;
                case 1:
                    products = products.OrderByDescending(p => Convert.ToInt32(p.MinCostForAgent));
                    break;
            }

            switch (cbFiltr.SelectedIndex)
            {
                case 0:
                    products = CoreModel.init().Products.Where(p => p.Title.Contains("Кровать"));
                    break;
                case 1:
                    products = CoreModel.init().Products.Where(p => p.Title.Contains("Гарнитур"));
                    break;
                case 2:
                    products = CoreModel.init().Products.Where(p => p.Title.Contains("Кресло"));
                    break;
                case 3:
                    products = CoreModel.init().Products.ToList();
                    break;               
            }

            lvProduct.ItemsSource = products.ToList();         
        }
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSearch();
        }

        private void cbSortChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSearch();
        }

        private void cbFiltrChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSearch();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProduct_Page(new Product()));
        }

        private void Button_DelClick(object sender, RoutedEventArgs e)
        {
            if (lvProduct.SelectedItems.Count > 1)
            {
                return;
            }

            Product prod = lvProduct.SelectedItem as Product;

            if (MessageBox.Show("Delete?", "Realy delete?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                CoreModel.init().Products.Remove(prod);
                CoreModel.init().SaveChanges();
                Update();
            }
        }

        private void Button_UpdateClick(object sender, RoutedEventArgs e)
        {
            Product productEdit= lvProduct.SelectedItem as Product;
            NavigationService.Navigate(new AddProduct_Page(productEdit));
        }

        private void AddProduct_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Update();
        }
    }
}
