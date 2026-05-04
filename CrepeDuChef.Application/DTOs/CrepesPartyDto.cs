namespace CrepeDuChef.Application.DTOs
{
    public class CrepesPartyDto
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public int SessionNumber { get; set; }

        public Guid UserId { get; set; }
    }
}
