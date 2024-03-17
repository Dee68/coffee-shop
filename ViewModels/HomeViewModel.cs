namespace Coffee_Shop.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly ProductService _productService;
        public HomeViewModel(ProductService productService)
        {
            _productService = productService;
            Products = new(_productService.GetPopularProducts());
        }

        public ObservableCollection<Product> Products { get; set; }

        [RelayCommand]
        private async Task GoToAllProducts()
        {
            Products = new(_productService.GetAllProducts());
            await Shell.Current.GoToAsync(nameof(AllProductPage), animate: true);
        }
        [RelayCommand]
        private async Task GoToFoodCategory()
        {
            Products = new(_productService.GetFoodCategory());
            await Shell.Current.GoToAsync(nameof(FoodPage), animate: true);
        }
        [RelayCommand]
        private async Task GoToHotDrinkCategory()
        {
            Products = new(_productService.GetHotDrinkCategory());
            await Shell.Current.GoToAsync(nameof(HotDrinkPage), animate: true);

        }
        [RelayCommand]
        private async Task GoToColdDrinkCategory()
        {
            Products = new(_productService.GetColdDrinkCategory());
            await Shell.Current.GoToAsync(nameof(ColdDrinkPage), animate: true);
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
