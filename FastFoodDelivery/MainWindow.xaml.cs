﻿using System.Data;
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
            
            MainFrame.Navigate(new LoginPage());
            
           
            //ItemPage();



        }
       

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }


}