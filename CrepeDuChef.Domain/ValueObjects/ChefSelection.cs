using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Domain.ValueObjects
{
    public record ChefSelection(User User, int SessionNumber);
}
