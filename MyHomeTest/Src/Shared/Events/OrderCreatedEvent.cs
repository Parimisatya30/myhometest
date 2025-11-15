namespace Shared.Events
{
    public class OrderCreatedEvent
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public OrderCreatedEvent(Guid id, Guid userId, string product, int quantity, decimal price)
        {
            Id = id;
            UserId = userId;
            Product = product;
            Quantity = quantity;
            Price = price;
        }
    }
}
