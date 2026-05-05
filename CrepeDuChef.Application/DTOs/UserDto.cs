namespace CrepeDuChef.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public override string ToString()
        {
            return FullName;
        }
    }
}
