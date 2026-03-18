namespace CrepeDuChef.Infrastructure.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public List<CrepesParty> CrepesParties { get; set; } = new();
    }
}