namespace CrepeDuChef.Domain.Interfaces
{
    public interface ISyncEntity
    {
        Guid Id { get; set; }
        DateTime UpdatedAt { get; set; }
        DateTime? DeletedAt { get; set; }
        Guid OriginDeviceId { get; set; }
    }
}
