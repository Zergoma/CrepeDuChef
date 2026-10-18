namespace CrepeDuChef.Application.DTOs
{
    public sealed record UpdateChefDto
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
    }
}
