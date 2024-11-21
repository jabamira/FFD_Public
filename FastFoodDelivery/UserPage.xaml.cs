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

namespace FastFoodDelivery
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
        }

        //KEYS
        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            PageFunc.KeysBack(sender, e, this.NavigationService);
        }

        //CLICK
        private void Shop_btn_Click(object sender, RoutedEventArgs e)
        {
            PageFunc.OpenShopPage();
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            PageFunc.GoBack(this.NavigationService);
        }
    }
}
