using CrepeDuChef.Domain.Exceptions;
using CrepeDuChef.Domain.Interfaces;

namespace CrepeDuChef.Domain.Entities
{
    public class Chef : ISyncEntity
    {
        public const int NameMaxLength = 50;

        // Global ID, shared across devices for synchronization
        public Guid Id { get; set; }

        private string _firstName = string.Empty;
        public string FirstName
        {
            get => _firstName;
            set => _firstName = string.IsNullOrWhiteSpace(value)
                ? throw new EmptyFirstNameException()
                : value;
        }

        private string _lastName = string.Empty;
        public string LastName
        {
            get => _lastName;
            set => _lastName = string.IsNullOrWhiteSpace(value)
                ? throw new EmptyLastNameException()
                : value;
        }

        public string FullName => $"{FirstName} {LastName}";

        public List<CrepeParty> CrepeParties { get; set; } = [];

        // Synchronization
        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public Guid OriginDeviceId { get; set; }
    }
}
