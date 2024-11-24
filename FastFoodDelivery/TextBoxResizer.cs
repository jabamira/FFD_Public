using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Diagnostics;

namespace FastFoodDelivery
{
    public static class TextBoxResizer
    {
        public static void ResizeTextBox(TextBox textBox, Rectangle rectangle)
        {
            if (textBox != null) 
            {
                double textWidth = MeasureTextWidth(textBox.Text, textBox.FontFamily, textBox.FontSize) + 20;
                Debug.WriteLine(textWidth);

                rectangle.Width = textWidth;
            }
           
            
               
            
            
        }
        public static void ResizePasswordBox(PasswordBox Password_Box, Rectangle rectangle)
        {

            double textWidth = Password_Box.Password.Length * 10 + 20;
            Debug.WriteLine(textWidth);
            
                rectangle.Width = textWidth;
            



        }

        private static double MeasureTextWidth(string text, FontFamily fontFamily, double fontSize)
        {
            const double pixelsPerDip = 96.0; // Обычно стандартное DPI

            FormattedText formattedText = new FormattedText(
              text,
              CultureInfo.CurrentCulture,
              FlowDirection.LeftToRight,
              new Typeface(fontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal),
              fontSize,
              Brushes.Black,
              pixelsPerDip);

            return formattedText.Width;
        }

    }
}