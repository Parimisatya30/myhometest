namespace OrderService.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Order(Guid userId, string product, int quantity, decimal price)
        {
            UserId = userId;
            Product = product;
            Quantity = quantity;
            Price = price;
        }
    }
}
