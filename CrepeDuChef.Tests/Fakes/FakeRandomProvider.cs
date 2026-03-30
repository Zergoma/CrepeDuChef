using CrepeDuChef.Domain.Interfaces;

namespace CrepeDuChef.Tests.Fakes
{
    public class FakeRandomProvider : IRandomProvider
    {
        private readonly int _value;

        public FakeRandomProvider(int value)
        {
            _value = value;
        }

        public int Next(int maxValue)
        {
            return _value;
        }
    }
}
