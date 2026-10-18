using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Tests.Helpers
{
    public static class TestData
    {
        public static List<Chef> ChefsWithIds(params int[] ids)
        {
            return [.. ids.Select(id => new Chef { Id = IntToGuid(id) })];
        }

        public static List<CrepeParty> CrepePartiesWithChefIds(params int[] ids)
        {
            return [..ids.Select(id => new CrepeParty { ChefId = IntToGuid(id) })];
        }


        public static Guid IntToGuid(int value)
        {
            // Formats the int as a 12-digit decimal string
            string decimalPart = value.ToString().PadLeft(12, '0');

            // Builds the full GUID
            string guidString = $"00000000-0000-0000-0000-{decimalPart}";

            return Guid.Parse(guidString);
        }
    }
}
