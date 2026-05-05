namespace CrepeDuChef.Application.DTOs
{
    public class UserDtoUpdate
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty; 
        public string LastName { get; set; } = string.Empty;
    }
}
