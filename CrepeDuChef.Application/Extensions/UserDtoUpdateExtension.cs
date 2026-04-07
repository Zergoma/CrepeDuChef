using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Extensions
{
    public static class UserDtoUpdateExtension
    {
        extension(UserDtoUpdate userDataUpdate)
        {
            public UserDto ToDto()
            {
                return new UserDto()
                {
                    Id = userDataUpdate.Id,
                    FirstName = userDataUpdate.FirstName,
                    LastName = userDataUpdate.LastName,
                };
            }
        }
    }
}
