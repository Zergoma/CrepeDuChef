using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Domain.ValueObjects
{
    public record ChefSelection(Chef Chef, int SessionNumber);
}
