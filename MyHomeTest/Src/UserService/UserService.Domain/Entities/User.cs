namespace UserService.Domain.Entities
{
    // Represents the User domain model.
    // This is the core business entity stored in the UserService in-memory database.
    public class User
    {
        // Primary key used for identifying the user.
        public Guid Id { get; private set; }

        // User's full name. Basic validation is handled at service or DTO level.
        public string Name { get; private set; }

        // Email address for the user, assumed unique in a real system.
        public string Email { get; private set; }

        // Constructor initializes required fields.
        public User(string name, string email)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
        }
    }

}
