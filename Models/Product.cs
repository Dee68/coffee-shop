namespace Coffee_Shop.Models
{
    public partial class Product : ObservableObject
    {
        public string Name  { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        [ObservableProperty, NotifyPropertyChangedFor(nameof(Amount))]
        private int _cartQuantity;

        public double Amount => Price * CartQuantity;

        public override string ToString()
        {
            return String.Format("Name:{0}, Price:{1}, Quantity:{2}", this.Name, this.Price, this.CartQuantity);
        }

        public Product Clone() => MemberwiseClone() as Product;
    }
}
