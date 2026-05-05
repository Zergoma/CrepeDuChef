namespace CrepeDuChef.Application.Interfaces
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
