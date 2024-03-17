namespace Coffee_Shop.ViewModels
{
    [QueryProperty(nameof(Order), nameof(Order))]
    public partial class OrderSummaryViewModel : ObservableObject
    {
        public event EventHandler<Product> CartItemRemoved;
        //public event EventHandler<Product> CartItemUpdated;
        public event EventHandler CartCleared;


        public ObservableCollection<Order> Orders { get; set; }

        [ObservableProperty]
        private string _customerName;
        [ObservableProperty]
        private string _customerPhone;
        [ObservableProperty]
        private double _totalAmount;

        

        public ObservableCollection<Product> Items { get; set; } = new();

        [ObservableProperty]
        private Order _order;

       
        private void RecalculateTotalAmount() => TotalAmount = Items.Sum(i => i.Amount);



        public static ICollection<Order> DailyOrder()
        {
            //declare empty list of orders
            var orders = new List<Order>();
            try
            {
                // Get the public documents folder path
                string publicPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);

                // Combine the public documents folder path with a file name
                string filePath = Path.Combine(publicPath, "Orders.json");
                // read the existing json file
                var jsonData = File.ReadAllText(filePath);
                //Deserialize the json to a list of order objects
                var orderList = JsonConvert.DeserializeObject<List<Order>>(jsonData);
                
                foreach (var order in orderList)
                {
                    if (order.OrderDate == DateTime.Now.ToString("dd/MM/yy"))
                    {
                        orders.Add(order);
                    }
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                Toast.Make($"Error: {ex.Message}", ToastDuration.Short).Show();
            }

            return orders;
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
                if (await Shell.Current.DisplayAlert("Delete Item", $"Do you want to delete {item.Name} ?", "Yes", "NO"))
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

        [RelayCommand]
        private async Task GetDailyOrders()
        {
            var orders = OrderSummaryViewModel.DailyOrder();
            if (orders is null)
            {
                await Toast.Make("No orders yet today", ToastDuration.Short).Show();
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(DailyOrder), animate: true);
            }

        }

        [RelayCommand]
        private async Task CheckOut()
        {
            await Shell.Current.GoToAsync(nameof(CheckoutPage), animate: true);
        }
       

    }
}
