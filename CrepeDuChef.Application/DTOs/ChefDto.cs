namespace CrepeDuChef.Application.DTOs
{
    public sealed record ChefDto
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";
    }
}
