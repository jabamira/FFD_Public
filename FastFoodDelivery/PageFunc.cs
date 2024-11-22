using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace FastFoodDelivery
{
    public static class PageFunc
    {

        public static void OpenPage(Page page, UserAuth _user, NavigationService nav)
        {
            
            Task<bool> isAuthTask = Task.Run(() => DataBaseHelper.CheckAuth(_user.AccessToken));
            var mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.Navigate(page);
            DataBaseHelper.CheckAuthLocal(isAuthTask, nav);
        }



        public static void GoBack(NavigationService navigationService)
        {
            if (navigationService.CanGoBack)
            {
                navigationService.GoBack();
            }
        }
        public static void GoToFirstPage(NavigationService navigationService)
        {
            
            while (navigationService.CanGoBack)
            {
                navigationService.GoBack();
            }

            // Если нужно сделать переход на первую страницу:
            // navigationService.Navigate(new MainPage());
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
