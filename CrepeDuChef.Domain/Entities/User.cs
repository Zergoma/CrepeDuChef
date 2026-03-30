using CrepeDuChef.Domain.Exceptions;

namespace CrepeDuChef.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

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

        public List<CrepesParty> CrepesParties { get; set; } = [];
    }
}