using CrepeDuChef.Domain.Interfaces;

namespace CrepeDuChef.Domain.Entities
{
    public class CrepesParty : ISyncEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Date { get; set; }

        public int SessionNumber { get; set; }

        // Relation vers User (clé étrangère)
        public Guid UserId { get; set; }
        public User User { get; set; }

        // Champs de synchro
        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public Guid OriginDeviceId { get; set; }
    }
}
