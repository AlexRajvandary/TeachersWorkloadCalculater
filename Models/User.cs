namespace StudingWorkloadCalculator.Models
{
    public class User
    {
        public User(int id, PermissionRights rights)
        {
            Id = id;
            Rights = rights;
        }

        public int Id { get; }

        public PermissionRights Rights { get; }
    }
}
