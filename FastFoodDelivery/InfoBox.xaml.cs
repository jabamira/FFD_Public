﻿using System;
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

namespace FastFoodDelivery
{
    /// <summary>
    /// Interaction logic for InfoBox.xaml
    /// </summary>
    public partial class InfoBox : Window
    {
        public InfoBox(string message)
        {
            
            InitializeComponent();
            Message_label.Text = message;
        }

        private void OK_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
