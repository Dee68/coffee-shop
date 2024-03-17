namespace Coffee_Shop.Models
{
    public partial class Order : ObservableObject
    {
        public IEnumerable<Product> CartItems { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public double TotalAmount => CalculateTotalAmount();

        public Order(IEnumerable<Product> products, string customerName, string customerPhone, string orderNumber, string dt)
        {
            CartItems = products;
            CustomerName = customerName;
            CustomerPhone = customerPhone;
            OrderNumber = orderNumber;
            OrderDate = dt;
        }

        public Order()
        {

        }
        private double CalculateTotalAmount()
        {
            // Calculate total amount based on the prices of products in CartItems
            if (CartItems == null)
                return 0;

            return CartItems.Sum(product => product.Amount);
        }

        public Order Clone() => MemberwiseClone() as Order;
    }
}
