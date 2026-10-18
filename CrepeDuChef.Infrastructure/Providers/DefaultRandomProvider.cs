using CrepeDuChef.Domain.Interfaces;

namespace CrepeDuChef.Infrastructure.Providers
{
    public class DefaultRandomProvider : IRandomProvider
    {
        private readonly Random _random = new();

        public int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }
    }
}
