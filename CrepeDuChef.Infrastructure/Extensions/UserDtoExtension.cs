using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Infrastructure.Entities;

namespace CrepeDuChef.Infrastructure.Extensions
{
    public static class UserDtoExtension
    {
        extension(UserDto userDto)
        {
            public User ToEntity()
            {
                return new()
                {
                    Id = userDto.Id,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                };
            }
        }
    }
}
