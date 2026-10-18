using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Mappers
{
    public static class ChefMapper
    {
        extension(Chef chef)
        {
            public ChefDto ToDto() => new()
            {
                Id = chef.Id,
                FirstName = chef.FirstName,
                LastName = chef.LastName,
            };
        }
    }
}
