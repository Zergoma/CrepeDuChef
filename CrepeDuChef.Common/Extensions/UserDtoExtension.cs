using CrepeDuChef.Common.DTOs;

namespace CrepeDuChef.Common.Extensions
{
    public static class UserDtoExtension
    {
        extension(UserDto userDto)
        {
            public string FullName()
            {
                return $"{userDto.FirstName} {userDto.LastName}";
            }
        }
    }
}
