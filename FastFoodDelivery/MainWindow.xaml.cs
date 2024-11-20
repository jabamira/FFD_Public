using System.Windows;

namespace FastFoodDelivery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /*
            
            */

            //MainFrame.Navigate(new LoginPage());
            //ShopPage();
            ItemPage();



        }
        public static void ShopPage() 
        {
            var mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.Navigate(new ShopPage());
        }
        public static void ItemPage()
        {
            var mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.Navigate(new ItemPage());
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }


}