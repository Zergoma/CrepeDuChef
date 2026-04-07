using CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.Application.Extensions
{
    public static class UserDtoExtension
    {
        extension(UserDto userDto)
        {
            public UserDtoAdd ToUserDtoAdd()
            {
                return new()
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                };
            }
        }

    }
}
