namespace Coffee_Shop.ViewModels
{
    public partial class DailyOrderViewModel : ObservableObject
    {

        [ObservableProperty]
        private string _customerName;
        [ObservableProperty]
        private string _customerPhone;
        [ObservableProperty]
        private double _totalAmount;

        public ObservableCollection<Product> Items { get; set; } = new();

        [ObservableProperty]
        private Order _order;

        public ICollection<Order> Orders { get; set; }

        private readonly OrderSummaryViewModel _orderSummary;
        public DailyOrderViewModel(OrderSummaryViewModel orderSummaryView)
        {
            _orderSummary = orderSummaryView;
            Orders = OrderSummaryViewModel.DailyOrder();


        }

        

    }
}
