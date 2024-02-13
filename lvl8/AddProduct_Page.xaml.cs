using lvl8.DbModel;
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

namespace lvl8
{
    /// <summary>
    /// Логика взаимодействия для AddProduct_Page.xaml
    /// </summary>
    public partial class AddProduct_Page : Page
    {
        Product product;
        public AddProduct_Page(Product prod)
        {
            InitializeComponent();
            this.product = prod;
            DataContext = product;
        }
        private void Button_SaveClick(object sender, RoutedEventArgs e)
        {
            if (product.Id == 0)
            {
                CoreModel.init().Products.Add(product);
            }

            CoreModel.init().SaveChanges();
            NavigationService.GoBack();
        }

        private void AddServices_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (product.Id != 0)
            {
                CoreModel.init().Entry(product).Reload();
            }
        }
    }
}
