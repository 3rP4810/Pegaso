using ProductCatalogue.Models;
using System.Collections.ObjectModel;
using System.Globalization;
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
        public FormWindow(ObservableCollection<Product> products)
        {
            InitializeComponent();
            DataContext = products;
        }

        void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string productName = NomeTxt.Text;
            decimal price = decimal.Parse(PrezzoTxt.Text, CultureInfo.GetCultureInfo("en-US"));
            int quantity = int.Parse(QuantitaTxt.Text);
            (bool res, int productId) = productService.Add(productName, quantity, price);

            if (res)
            {
                var observableProducts = DataContext as ObservableCollection<Product>;
                observableProducts.Add(new Product { productId = productId, productName = productName, quantity = quantity, price = price });
            }
            this.Close();
        }

        void LimitCharacterCheck(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^.{0,100}$");
            var textBox = sender as TextBox;
            string app = textBox.Text + e.Text;
            if (!regex.IsMatch(app)) { e.Handled = true; }
        }

        void DecimalNumberCheck(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^\d{1,6}(\.\d{0,2})?$");
            var textBox = sender as TextBox;
            string app = textBox.Text + e.Text;
            if (!regex.IsMatch(app)) { e.Handled = true; }
        }

        void IntNumberCheck(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) { e.Handled = true; }
        }
    }
}
