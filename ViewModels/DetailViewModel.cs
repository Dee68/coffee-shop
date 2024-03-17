namespace Coffee_Shop.ViewModels
{
    [QueryProperty(nameof(Product), nameof(Product))]
    public partial class DetailViewModel : ObservableObject, IDisposable
    {
        private readonly OrderViewModel _orderViewModel;
        public DetailViewModel(OrderViewModel orderViewModel)
        {
            _orderViewModel = orderViewModel;
            _orderViewModel.CartCleared += OnCartCleared;
            _orderViewModel.CartItemRemoved += OnCartItemRemoved;
            _orderViewModel.CartItemUpdated += OnCartItemUpdated;
        }


        [ObservableProperty]
        private Product _product;

        private void OnCartCleared(Object? _, EventArgs e) => Product.CartQuantity = 0;

        private void OnCartItemUpdated(Object? _, Product p) => OnCartItemChanged(p, p.CartQuantity);

        private void OnCartItemRemoved(Object? _, Product p) => OnCartItemChanged(p, 0);

        private void OnCartItemChanged(Product p, int quantity)
        {
            if (p.Name == Product.Name)
            {
                Product.CartQuantity = quantity;
            }
        }

        [RelayCommand]
        private void AddToCart()
        {
            Product.CartQuantity++;
            _orderViewModel.UpdateCartCommand.Execute(Product);
        }
        [RelayCommand]
        private void RemoveFromCart()
        {
            if (Product.CartQuantity > 0)
            {
                Product.CartQuantity--;
                _orderViewModel.UpdateCartCommand.Execute(Product);
            }
        }

        [RelayCommand]
        private async Task ViewCart()
        {
            if (Product.CartQuantity > 0)
            {
                await Shell.Current.GoToAsync(nameof(OrderPage), animate: true);

            }
            else
            {
                await Toast.Make("Please use the plus sign button to add item to your cart", ToastDuration.Short).Show();
            }
        }

        public void Dispose()
        {
            _orderViewModel.CartCleared -= OnCartCleared;
            _orderViewModel.CartItemRemoved -= OnCartItemRemoved;
            _orderViewModel.CartItemUpdated -= OnCartItemUpdated;
        }
    }
}
