namespace Coffee_Shop.ViewModels
{
    public partial class AllProductViewModel : ObservableObject
    {
        private readonly ProductService _productService;
        public AllProductViewModel(ProductService productService)
        {
            _productService = productService;
            Products = new(_productService.GetAllProducts());
        }

        public ObservableCollection<Product> Products { get; set; }

        [ObservableProperty]
        private bool _searching;

        [RelayCommand]
        private async Task SearchProducts(string searchText)
        {
            Products.Clear();
            Searching = true;
            await Task.Delay(1000);
            foreach (var product in _productService.GetProducts(searchText))
            {
                Products.Add(product);
            }
            Searching = false;
        }

        [RelayCommand]
        private async Task GoToDetailPage(Product product)
        {
            var parameters = new Dictionary<string, object>
            {
                [nameof(DetailViewModel.Product)] = product
            };
            await Shell.Current.GoToAsync(nameof(DetailPage), animate: true, parameters);
        }
    }
}
