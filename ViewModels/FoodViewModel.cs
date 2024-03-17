namespace Coffee_Shop.ViewModels
{
    public partial class FoodViewModel : ObservableObject
    {
        private readonly ProductService _productService;
        public FoodViewModel(ProductService productService)
        {
            _productService = productService;
            Products = new(_productService.GetFoodCategory());
        }
        public ObservableCollection<Product> Products { get; set; }

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
