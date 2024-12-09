namespace ProductCatalogue.ViewModels
{
    public class MainWindowViewModels
    {
        private Models.IProductService _productsService = null;
        public MainWindowViewModels(Models.IProductService productService) { _productsService = productService; }
        
        public IList<Models.Product> Products => _productsService.Products;
    }
}
