namespace OrderService.Domain.Entities
{

    // Represents the Order domain model.
    // This entity is owned by the OrderService and stored in its database.
    public class Order
    {
        // Primary key for the order.
        public Guid Id { get; set; } = Guid.NewGuid();

        // ID of the user placing the order.
        // This links to the User microservice but is not enforced as a foreign key to keep services decoupled.
        public Guid UserId { get; set; }

        // Simple representation of order details.
        public string Product { get; set; }
        public int Quantity { get; set; }

        // Total amount for the order transaction.
        public decimal Price { get; set; }
                  
   
        // Constructor initializes the core business properties
        public Order(Guid userId, string product, int quantity, decimal price)
        {
            UserId = userId;
            Product = product;
            Quantity = quantity;
            Price = price;
        }
    }
}
