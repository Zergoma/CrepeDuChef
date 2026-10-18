namespace CrepeDuChef.Application.DTOs
{
    public sealed record AddChefDto
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
    }
}
