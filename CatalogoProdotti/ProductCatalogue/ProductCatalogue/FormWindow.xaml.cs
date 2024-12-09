using ProductCatalogue.Models;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProductCatalogue
{
    /// <summary>
    /// Interaction logic for FormWindow.xaml
    /// </summary>
    public partial class FormWindow : Window
    {
        ProductService productService = new ProductService();
        public FormWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            var product = button?.CommandParameter as Product;

            if (product != null)
            {
                productService.Add(product);
                
                this.Close();
            }
        }

        private void DecimalNumberCheck(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^\d{1,6}(\.\d{0,2})?$");
            var textBox = sender as TextBox;
            string app = textBox.Text + e.Text;
            if (!regex.IsMatch(app)) { e.Handled = true; }
        }

        private void IntNumberCheck(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) { e.Handled = true; }
        }
    }
}
