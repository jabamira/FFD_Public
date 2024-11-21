using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace FastFoodDelivery
{
    public static class PageFunc
    {

        public static void OpenPage(Page page )
        {
            var mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.Navigate(page);
        }

        public static void OpenShopPage()
        {
            OpenPage(new ShopPage());
        }
        public static void OpenItemPage()
        {
            OpenPage(new ItemPage());
        }

        public static void GoBack(NavigationService navigationService)
        {
            if (navigationService.CanGoBack)
            {
                navigationService.GoBack();
            }
        }

        public static void KeysBack(object sender, KeyEventArgs e, NavigationService navigationService)
        {
            // Проверяем нажатие клавиши Esc
            if (e.Key == Key.Escape)
            {
                Debug.WriteLine(1);
                GoBack(navigationService);
            }
            // Проверяем нажатие Ctrl + Z
            else if (e.Key == Key.Z && Keyboard.Modifiers == ModifierKeys.Control)
            {
                GoBack(navigationService);
            }
        }
    }
}
