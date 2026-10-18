using CrepeDuChef.Domain.Interfaces;

namespace CrepeDuChef.Domain.Entities
{
    public class CrepeParty : ISyncEntity
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public int SessionNumber { get; set; }

        public Guid ChefId { get; set; }
        public Chef? Chef { get; set; }

        // Synchronization
        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public Guid OriginDeviceId { get; set; }
    }
}
