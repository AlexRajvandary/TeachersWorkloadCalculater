namespace StudingWorkloadCalculator.Models
{
    public class User
    {
        public User(int id, Rights rights)
        {
            Id = id;
            Rights = rights;
        }

        public int Id { get; set; }

        public Rights Rights { get; set; }
    }
}
