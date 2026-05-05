using CrepeDuChef.Domain.Exceptions;
using CrepeDuChef.Domain.Interfaces;

namespace CrepeDuChef.Domain.Entities
{
    public class User : ISyncEntity
    {
        // 🔑 Global ID for synchro
        public Guid Id { get; set; } = Guid.NewGuid();

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

        // 🔁 Relation
        public List<CrepesParty> CrepesParties { get; set; } = [];

        // 🕒 Synchro
        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public Guid OriginDeviceId { get; set; }
    }
}