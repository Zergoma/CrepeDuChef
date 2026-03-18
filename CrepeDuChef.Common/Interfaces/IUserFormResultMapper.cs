using CrepeDuChef.Common.DTOs;

namespace CrepeDuChef.Common.Interfaces
{
    public interface IUserFormResultMapper
    {
        UserDataResult Map(List<string?>? results);
    }
}
