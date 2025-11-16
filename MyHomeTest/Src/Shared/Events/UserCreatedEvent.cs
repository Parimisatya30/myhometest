namespace Shared.Events
{
    /// <summary>
    /// Event published to Kafka whenever a new user is successfully created.
    /// 
    /// Other microservices (e.g., OrderService) can consume this event
    /// to update their internal state or trigger downstream processes.
    /// This promotes loose coupling between services and asynchronous communication.
    /// </summary>
    public class UserCreatedEvent
    {
        /// <summary>
        /// Unique identifier of the newly created user.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Full name of the newly created user.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Email address of the newly created user.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Constructor to initialize all properties of the user event.
        /// </summary>
        public UserCreatedEvent(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
