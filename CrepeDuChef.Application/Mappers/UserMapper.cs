using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Mappers
{
    public static class UserMapper
    {
        extension(User user)
        {
            public string AllToString()
            {
                return user.FirstName + " " + user.LastName + " ID: " + user.Id;
            }

            public UserDto ToDto()
            {
                return new UserDto()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };
            }
        }

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
