using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using Rectangle = System.Windows.Shapes.Rectangle;


namespace FastFoodDelivery
{
    public partial class LoginPage : Page

    {
        Rectangle Line_login;
        Rectangle Background_login_log;

        //Password_container
        PasswordBox Password_box;
        Rectangle Line_password;
        TextBox Text_Password_box;


        //Reply_password_container
        TextBox Text_reply_box;
        PasswordBox Password_reply_box;
        Rectangle Line_password_reply;

        //Login_btn
        Label Login_label;
        Rectangle Line_log_btn;

        bool register = false;
        public LoginPage()

        {
            InitializeComponent();
            



        }
        public void SetCenter(InfoBox messageBox) 
        {
            var currentWindow = Window.GetWindow(this);
            if (currentWindow != null)
            {
                messageBox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                messageBox.Owner = currentWindow;
            }
            messageBox.ShowDialog();
        }


        //Local operation
        public async void Login_local() 
        {
            string login = Login_box.Text.Trim();
            string password = Text_Password_box.Text.Trim();
            Debug.WriteLine("Login try");
            Login_btn.IsEnabled = false;
            donutSpinner.Visibility = Visibility.Visible;
            byte result = await Task.Run(() => DataBaseHelper.Login(login, password));
            donutSpinner.Visibility = Visibility.Collapsed;
            if (result == 1)
            {
                var messageBox = new InfoBox("Login is Successful");
                SetCenter(messageBox);
                MainWindow.ShopPage();



            }
            if (result == 2)
            {

                var messageBox = new InfoBox("User not found!");
                SetCenter(messageBox);

            }
            if (result == 52)
            {
                var messageBox = new InfoBox("Invalid password!");
                SetCenter(messageBox);
            }
            Login_btn.IsEnabled = true;
            

        }
        public async void Registration_local() 
        {
            string login = Login_box.Text.Trim();
            string password = Text_Password_box.Text.Trim();
            DateTime moscowTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time"));
            Login_btn.IsEnabled = false;
            donutSpinner.Visibility = Visibility.Visible;
            bool isRegistered = await Task.Run(() => DataBaseHelper.Registration(login, password, moscowTime));
            donutSpinner.Visibility = Visibility.Collapsed;
            if (isRegistered)
            {
                var messageBox = new InfoBox("Registration is Successful\nNow you can Login");
                SetCenter(messageBox);
                


            } 
            else
            {
                var messageBox = new InfoBox("Registration is Failed\nBecause: User already exist");
                SetCenter(messageBox);
               
            }
            
            Login_btn.IsEnabled = true;

        }


        //CHECKBOX

        private void Password_check_box_Checked(object sender, RoutedEventArgs e)
        {
            // Показать пароль
            Text_Password_box.Text = Password_box.Password;
            //первое поле пароля
            Text_Password_box.Visibility = Visibility.Visible;
            Password_box.Visibility = Visibility.Hidden;


            // Если пароль пустой, показываем "Enter Password"
            if (Text_Password_box.Text == "")
            {
                Text_Password_box.Text = "Enter Password";
            }

            TextBoxResizer.ResizeTextBox(Text_Password_box, Line_password);

            //второе поле пароля
            if (register)
            {
                Text_reply_box.Text = Password_reply_box.Password;
                Text_reply_box.Visibility = Visibility.Visible;
                Password_reply_box.Visibility = Visibility.Hidden;

                if (Text_reply_box.Text == "")
                {
                    Text_reply_box.Text = "Reply Password";
                }

                TextBoxResizer.ResizeTextBox(Text_reply_box, Line_password_reply);
            }

        }

        private void Password_check_box_Unchecked(object sender, RoutedEventArgs e)
        {
            // Скрыть пароль
            if (Text_Password_box.Text != "Enter Password")
            {
                Password_box.Password = Text_Password_box.Text;
            }

            Text_Password_box.Visibility = Visibility.Hidden;
            Password_box.Visibility = Visibility.Visible;

            TextBoxResizer.ResizePasswordBox(Password_box, Line_password);
            // второй 
            if (register)
            {
                if (Text_reply_box.Text != "Reply Password")
                {
                    Password_reply_box.Password = Text_reply_box.Text;
                }
                Text_reply_box.Visibility = Visibility.Hidden;
                Password_reply_box.Visibility = Visibility.Visible;

                TextBoxResizer.ResizePasswordBox(Password_reply_box, Line_password_reply);
            }
        }

        //CLICK
        private void Login_btn_Click(object sender, RoutedEventArgs e)
        {
            if (register) 
            {
               
                  if (Password_check_box.IsChecked == true)
                  {
                    if (Login_box.Text.Trim() != "Enter Login" && Text_Password_box.Text.Trim() != "Enter Password" && Text_reply_box.Text.Trim() != "Reply Password")
                    {
                        if (Login_box.Text.Trim() != "" && Text_Password_box.Text.Trim() != "" && Text_reply_box.Text.Trim() != "")
                        {
                            if (Text_Password_box.Text.Trim() == Text_reply_box.Text.Trim())
                            {
                                Registration_local();
                            }
                            else 
                            {
                                Debug.WriteLine("Passwords doesn't match");
                            }
                            
                        }
                    }
                    else 
                    {
                        Debug.WriteLine("Empty boxes");
                    }
                  }
                  else
                  {
                    if (Login_box.Text.Trim() != "Enter Login" && Text_Password_box.Text.Trim() != "Enter Password" && Text_reply_box.Text.Trim() != "Reply Password")
                    {
                        if (Login_box.Text.Trim() != "" && Password_box.Password.Trim() != "" && Password_reply_box.Password.Trim() != "")
                        {
                            if (Password_box.Password.Trim() == Password_reply_box.Password.Trim())
                            {
                                Registration_local();
                            }
                            else
                            {
                                Debug.WriteLine("Passwords doesn't match");
                            }
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Empty boxes");
                    }
                  }
                
                
            }
            else
            {
                if (Password_check_box.IsChecked == true)
                {
                    if (Login_box.Text.Trim() != "Enter Login" && Text_Password_box.Text.Trim() != "Enter Password")
                    {
                        if (Login_box.Text.Trim() != "" && Text_Password_box.Text.Trim() != "")
                        {
                            Login_local();
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Empty boxes");
                    }
                }
                else
                {
                    if (Login_box.Text.Trim() != "Enter Login" && Text_Password_box.Text.Trim() != "Enter Password")
                    {
                        if (Login_box.Text.Trim() != "" && Password_box.Password.Trim() != "")
                        {
                            Login_local();
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Empty boxes");
                    }
                }
            }
        }

        private void Not_reg_label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (register == false)
            {
                //register
                Login_label.Content = "Complete Registration";
                Line_log_btn.Width = 225;
                Not_reg_label.Content = "Go to login";
                Not_reg_label.Width = 90;
                Reply_password_container.Visibility = Visibility.Visible;
                if (Password_check_box.IsChecked == true)
                {
                    Text_reply_box.Visibility = Visibility.Visible;
                    Password_reply_box.Visibility = Visibility.Hidden;
                }
                else
                {
                    Password_reply_box.Visibility = Visibility.Visible;
                    if (Password_reply_box.Password != "") Text_reply_box.Visibility = Visibility.Hidden;
                    if (Text_reply_box.Text != "Reply Password" && Text_reply_box.Text != "")
                    {
                        Password_reply_box.Password = Text_reply_box.Text;
                        Text_reply_box.Visibility = Visibility.Hidden;
                    }
                }

                Grid.SetRow(Login_btn, Grid.GetRow(Login_btn) + 1);
                register = true;
            }
            else
            {
                //auth
                Login_label.Content = "Login";
                Line_log_btn.Width = 65;
                Not_reg_label.Content = "Not register yet";
                Not_reg_label.Width = 110;
                Reply_password_container.Visibility = Visibility.Hidden;
                Match_label.Visibility = Visibility.Hidden;
                if (Password_check_box.IsChecked == true)
                {
                    Text_reply_box.Visibility = Visibility.Visible;
                    Password_reply_box.Visibility = Visibility.Hidden;
                }

                Grid.SetRow(Login_btn, Grid.GetRow(Login_btn) - 1);
                register = false;
            }






        }


        //LOST GOT FOCUS


        //Login box
        private void Login_box_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Login_box.Text == "Enter Login")
            {
                Login_box.Text = "";
            }

        }
        private void Login_box_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Login_box.Text.Length == 0)
            {
                Login_box.Text = "Enter Login";

            }


        }

        //Password Box
        private void Password_box_GotFocus(object sender, RoutedEventArgs e)
        {
            Text_Password_box.Visibility = Visibility.Hidden;
        }

        private void Password_box_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Password_box.Password.Length == 0)
            {
                if (Password_box.Password.Length > 0)
                {
                    Text_Password_box.Text = Password_box.Password;
                }
                else
                {
                    Text_Password_box.Text = "Enter Password";
                }

                Text_Password_box.Visibility = Visibility.Visible;
                Line_password.Width = 125;
            }



        }
        // textPassword Box
        private void Text_Password_box_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Text_Password_box.Text == "") Text_Password_box.Text = "Enter Password";
        }
        private void Text_Password_box_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Text_Password_box.Text == "Enter Password") Text_Password_box.Text = "";
        }

        //Reply Password box text
        private void Reply_password_box_text_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Text_reply_box.Text == "Reply Password") Text_reply_box.Text = "";
        }
        private void Reply_password_box_text_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Text_reply_box.Text == "") Text_reply_box.Text = "Reply Password";
            TextBoxResizer.ResizeTextBox(Text_reply_box, Line_password);
        }

        //Reply Password password
        private void Password_reply_box_GotFocus(object sender, RoutedEventArgs e)
        {
            Text_reply_box.Visibility = Visibility.Hidden;
        }

        private void Password_reply_box_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Password_reply_box.Password.Length == 0)
            {
                if (Password_reply_box.Password.Length > 0)
                {
                    Text_reply_box.Text = Password_reply_box.Password;
                }
                else
                {
                    Text_reply_box.Text = "Reply Password";
                }

                Text_reply_box.Visibility = Visibility.Visible;
            }
        }




        //CHANGE TEXT
        
        
        //Change login
        private void Login_box_TextChanged(object sender, TextChangedEventArgs e)

        {
            if (Line_login != null)
            {
                TextBoxResizer.ResizeTextBox(Login_box, Line_login);
            }
            else
            {
                Debug.WriteLine("null Line_login");
            }
        }
        //Change passwords
        private void Password_box_PasswordChanged(object sender, RoutedEventArgs e)
        {

            Text_Password_box.Text = Password_box.Password;
            TextBoxResizer.ResizePasswordBox(Password_box, Line_password);
            if (register == true)
            {
                if (Password_box.Password != Password_reply_box.Password) Match_label.Visibility = Visibility.Visible;
                else Match_label.Visibility = Visibility.Hidden;
            }

        }

        private void Text_Password_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxResizer.ResizeTextBox(Text_Password_box, Line_password);
            if (register == true)
            {
                if (Text_Password_box.Text != Text_reply_box.Text && (Text_Password_box.Text != "Enter Password" && Text_reply_box.Text != "Reply Password")) Match_label.Visibility = Visibility.Visible;
                else Match_label.Visibility = Visibility.Hidden;
            }

        }

        //Reply passwords boxes 
        private void Text_reply_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxResizer.ResizeTextBox(Text_reply_box, Line_password_reply);
            if (register == true)
            {
                if (Text_Password_box.Text != Text_reply_box.Text && (Text_Password_box.Text != "Enter Password" && Text_reply_box.Text != "Reply Password"))
                {

                    Match_label.Visibility = Visibility.Visible;
                }
                else Match_label.Visibility = Visibility.Hidden;
            }

        }

        private void Password_reply_box_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Password_reply_box != null && Line_password_reply != null)
            {
                Text_reply_box.Text = Password_reply_box.Password;
                TextBoxResizer.ResizePasswordBox(Password_reply_box, Line_password_reply);

            }
            else Debug.WriteLine("Password_reply_box null or Line_password_reply null");

            if (register == true)
            {
                if (Password_box.Password != Password_reply_box.Password) Match_label.Visibility = Visibility.Visible;
                else Match_label.Visibility = Visibility.Hidden;
            }


        }

        //LOADERS

        private void Password_container_Loaded(object sender, RoutedEventArgs e)
        {
            var template = Password_container.Template as ControlTemplate;
            Password_box = template?.FindName("Password_box", Password_container) as PasswordBox;
            Text_Password_box = template?.FindName("Text_Password_box", Password_container) as TextBox;
            Line_password = template?.FindName("Line_password", Password_container) as Rectangle;
            

        }
        private void Login_box_Loaded(object sender, RoutedEventArgs e)
        {
            var template = Login_box.Template as ControlTemplate;

            Background_login_log = template?.FindName("Background_login_log", Login_box) as Rectangle;
            Line_login = template?.FindName("Line_login", Login_box) as Rectangle;

            template = Reply_password_container.Template as ControlTemplate;
            Text_reply_box = template?.FindName("Text_reply_box", Reply_password_container) as TextBox;
            Password_reply_box = template?.FindName("Password_reply_box", Reply_password_container) as PasswordBox;
            Line_password_reply = template?.FindName("Line_password_reply", Reply_password_container) as Rectangle;
            Login_box.Text = "Artemka";
            

            TextBoxResizer.ResizeTextBox(Login_box, Line_login);
            TextBoxResizer.ResizeTextBox(Text_reply_box, Line_password_reply);


        }
        private void Login_btn_Loaded(object sender, RoutedEventArgs e)
        {
            var template = Login_btn.Template as ControlTemplate;
            Login_label = template?.FindName("Login_label", Login_btn) as Label;
            Line_log_btn = template?.FindName("Line_log_btn", Login_btn) as Rectangle;
        }

    }
}