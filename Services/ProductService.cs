namespace Coffee_Shop.Services
{
    public class ProductService
    {
        private readonly IEnumerable<Product> _products = new List<Product>
        {
            new Product
            {
                Name = "Hot Chocolate",
                Category = "Hot Drink",
                Description = " A thick, creamy drink made with chocolate and milk. It’s often served with whipped cream or marshmallows on top1",
                Image = "coffee_cup1.png",
                Price = 2.1
            },
            new Product
            {
                Name = "Cafe au lait",
                Category = "Hot Drink",
                Description = "A French method of serving coffee that’s smooth and satisfying, with the perfect ratio of rich milk to bitter coffee1",
                Image = "coffee_cup2.png",
                Price = 2.0
            },
            new Product
            {
                Name = "Coffee",
                Category = "Hot Drink",
                Description = "The absolute best hot chocolate is homemade in just 5 minutes. It’s creamy, rich, chocolaty, and tastes like a dream with a hint of cinnamon and vanilla on the finish",
                Image = "coffee_cup3.png",
                Price = 2.2
            },
            new Product
            {
                Name = "Burger",
                Category = "Food",
                Description = "A sandwich consisting of a cooked patty of ground meat, garnished with cheese, lettuce, tomato, onion, pickle, ketchup, mustard, mayonnaise, and other condiments.",
                Image = "burger1.png",
                Price = 3.3
            },
             new Product
            {
                Name = "Double Burger",
                Category = "Food",
                Description = "A sandwich consisting of a cooked patty of ground meat, garnished with cheese, lettuce, tomato, onion, pickle, ketchup, mustard, mayonnaise, and other condiments.",
                Image = "burger2.png",
                Price = 3.7
            },
              new Product
            {
                Name = "Tea",
                Category = "Cold Drink",
                Description = "An Indian spiced milk tea that’s popular all around the world. It’s made with creamy milk, subtly herbal tea, warm cinnamon and cardamom, and nuanced sweetness",
                Image = "iced_coffee.png",
                Price = 1.5
            },
               new Product
            {
                Name = "Lemonade",
                Category = "Cold Drink",
                Description = "This a description of the item",
                Image = "lemonade.png",
                Price = 2.0
            },
               new Product
            {
                Name = "soft1",
                Category = "Cold Drink",
                Description = "This a description of the item",
                Image = "soft_drink3.png",
                Price = 3.7
            },
                  new Product
            {
                Name = "soft2",
                Category = "Cold Drink",
                Description = "This a description of the item",
                Image = "soft_drinks5.png",
                Price = 3.7
            },


        };

        public IEnumerable<Product> GetAllProducts() => _products;
        public IEnumerable<Product> GetFoodCategory()
        {
            var products = new List<Product>();
            foreach (var product in _products)
            {
                if (product.Category == "Food")
                {
                    products.Add(product);
                }
            }
            return products;
        }
        public IEnumerable<Product> GetHotDrinkCategory()
        {
            var products = new List<Product>();
            foreach (var product in _products)
            {
                if (product.Category == "Hot Drink")
                {
                    products.Add(product);
                }
            }
            return products;
        }
        public IEnumerable<Product> GetColdDrinkCategory()
        {
            var products = new List<Product>();
            foreach (var product in _products)
            {
                if (product.Category == "Cold Drink")
                {
                    products.Add(product);
                }
            }
            return products;
        }

        public IEnumerable<Product> GetPopularProducts(int count = 6) => _products.OrderBy(p => Guid.NewGuid()).Take(count);
        // enable search functionality
        public IEnumerable<Product> GetProducts(string searchTerm) =>
            string.IsNullOrWhiteSpace(searchTerm)
            ? _products
            : _products.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
    }
}
