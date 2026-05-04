using CrepeDuChef.Application.Interfaces;

namespace CrepeDuChef.Infrastructure.Services
{
    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }

}
