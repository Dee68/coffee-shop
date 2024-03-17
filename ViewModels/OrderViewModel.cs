namespace Coffee_Shop.ViewModels
{
    public partial class OrderViewModel : ObservableObject
    {
        public event EventHandler<Product> CartItemRemoved;
        public event EventHandler<Product> CartItemUpdated;
        public event EventHandler CartCleared;
        public event EventHandler EmptyCart;

        //public static int currentNum = 100000;
        public ObservableCollection<Product> Items { get; set; } = new();

        [ObservableProperty]
        private ObservableCollection<Order> _orders;
        [ObservableProperty]
        private string _customerName;
        [ObservableProperty]
        private string _customerPhone;
        [ObservableProperty]
        private double _totalAmount;
      

        private readonly OrderSummaryViewModel _orderSummaryViewModel;

        public OrderViewModel(OrderSummaryViewModel orderSummaryViewModel)
        {
            _orderSummaryViewModel = orderSummaryViewModel;
           
            
        }


        private void RecalculateTotalAmount() => TotalAmount = Items.Sum(i => i.Amount);

      
        //private static string GenerateId()
        //{
        //    var ordNum = ++currentNum;
        //    return "N"+ordNum.ToString();
        //}
      
        private static string GenerateOrderNumber()
        {
            // Logic to generate a unique order number
            return Guid.NewGuid().ToString("N")[..8].ToUpper();
        }
       

        
        private ICollection<Product> GetCartItems()
        {
            var cartItems = new List<Product>();
            foreach (var item in Items)
            {
                cartItems.Add(item);
            }
            return cartItems;
        }
        public Order GetOrder()
        {
            var order = new Order(GetCartItems(), CustomerName, CustomerPhone, OrderViewModel.GenerateOrderNumber(), DateTime.Now.ToString("dd/MM/yy"));
            return order;

        }

        [RelayCommand]
        private void UpdateCart(Product product)
        {
            var item = Items.FirstOrDefault(i => i.Name == product.Name);
            if (item != null)
            {
                item.CartQuantity = product.CartQuantity;
            }
            else
            {
                Items.Add(product.Clone());
            }
            RecalculateTotalAmount();
        }

        [RelayCommand]
        private async void RemoveCartItem(string name)
        {
            var item = Items.FirstOrDefault(i => i.Name.Equals(name));
            if (item is not null)
            {
                if (await Shell.Current.DisplayAlert("Delete Item",$"Do you want to delete {item.Name} ?", "Yes","NO"))
                {
                    Items.Remove(item);
                    RecalculateTotalAmount();
                    CartItemRemoved?.Invoke(this, item);
                    await Toast.Make($"{item.Name} has been removed from cart", ToastDuration.Short).Show();
                }
                //Items.Remove(item);
                //RecalculateTotalAmount();
                //CartItemRemoved?.Invoke(this, item);
                //var snackBarOptions = new SnackbarOptions
                //{
                //    CornerRadius = 10,
                //    BackgroundColor = Colors.PaleGoldenrod
                //};
                //var snackbar = Snackbar.Make($"'{item.Name}' removed from cart",
                //    () =>
                //    {
                //        Items.Add(item);
                //        RecalculateTotalAmount();
                //        CartItemUpdated?.Invoke(this, item);
                //    }, "Undo", TimeSpan.FromSeconds(15), snackBarOptions);
                //await snackbar.Show();
            }

        }

        [RelayCommand]
        private async Task CartEmpty()
        {
            Items.Clear();
            CustomerName = null; CustomerPhone = null;
            RecalculateTotalAmount();
            CartCleared?.Invoke(this, EventArgs.Empty);
            await Toast.Make("Your order has been submitted", ToastDuration.Short).Show();
        }


        [RelayCommand]
        private async Task ClearCart()
        {
            if (await Shell.Current.DisplayAlert("Confirm Clear Cart?", "Do you want to clear  the cart items?", "Yes", "No"))
            {
                Items.Clear();
                RecalculateTotalAmount();
                CartCleared?.Invoke(this, EventArgs.Empty);
                await Toast.Make("Cart has been cleared", ToastDuration.Short).Show();
            }
        }

    }
}
