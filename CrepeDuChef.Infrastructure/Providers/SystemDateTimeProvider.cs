using CrepeDuChef.Application.Interfaces;

namespace CrepeDuChef.Infrastructure.Providers
{
    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }

}
