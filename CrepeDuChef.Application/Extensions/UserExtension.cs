using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Extensions
{
    public static class UserExtension
    {
        extension(User user)
        {
            public string AllToString()
            {
                return user.FirstName + " " + user.LastName + " ID: " + user.Id;
            }

            public string FullName()
            {
                return $"{user.FirstName} {user.LastName}";
            }
        }
    }
}
