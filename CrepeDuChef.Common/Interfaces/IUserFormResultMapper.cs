using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Models;

namespace CrepeDuChef.Common.Interfaces
{
    public interface IUserFormResultMapper
    {
        UserDataResult Map(List<string?>? results);
        UserDataResult Map(UserDto? results);
    }
}
