using ProductCatalogue.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ProductCatalogue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly IProductService _productService;
        public MainWindow(Models.IProductService productService)
        {
            InitializeComponent();
            _productService = productService;
            DataContext = new ObservableCollection<Product>(_productService.Products);
        }

        void AddButton_Click(object sender, RoutedEventArgs e)
        {
            FormWindow formWindow = new FormWindow((ObservableCollection<Product>)DataContext);
            formWindow.ShowDialog();
        }

        void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            var product = button?.CommandParameter as Product;

            if(product != null)
            {
                UpdateWindow updateWindow = new UpdateWindow();
                updateWindow.DataContext = product;
                updateWindow.ShowDialog();
            } 
        }

        void Remove_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            var product = button?.CommandParameter as Product;

            if (product != null)
            {
                if (_productService.Remove(product.productId))
                {
                    var observableProducts = DataContext as ObservableCollection<Product>;
                    observableProducts.Remove(product);
                }
            }            
        }
    }
}