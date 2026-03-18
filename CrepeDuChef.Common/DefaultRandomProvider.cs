using CrepeDuChef.Common.Interfaces;

namespace CrepeDuChef.Common
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
