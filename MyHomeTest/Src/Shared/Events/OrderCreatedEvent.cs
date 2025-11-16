namespace Shared.Events
{
    /// <summary>
    /// Event raised whenever a new order is created successfully.
    /// 
    /// This event is published to Kafka to allow other microservices
    /// (e.g., UserService, NotificationService, AnalyticsService) 
    /// to react to order creation in an asynchronous, decoupled manner.
    /// </summary>
    public class OrderCreatedEvent
    {
        /// <summary>
        /// Unique identifier for the order.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID of the user who placed the order.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Name of the product ordered.
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// Quantity of the product in the order.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Total price of the order.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Constructor to initialize all properties of the order event.
        /// </summary>
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
