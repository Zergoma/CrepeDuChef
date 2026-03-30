using CrepeDuChef.Domain.Interfaces;

namespace CrepeDuChef.Infrastructure.Extensions
{
    public static class RandomExtensions
    {
        public static T PickRandom<T>(
            this IEnumerable<T> source,
            IRandomProvider random)
        {
            var list = source.ToList();

            if (list.Count == 0)
            {
                throw new InvalidOperationException();
            }

            int index = random.Next(list.Count);

            return list[index];
        }
    }
}
