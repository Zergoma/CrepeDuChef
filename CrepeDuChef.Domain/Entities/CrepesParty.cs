namespace CrepeDuChef.Domain.Entities
{
    public class CrepesParty
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int SessionNumber { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = new();
    }
}
