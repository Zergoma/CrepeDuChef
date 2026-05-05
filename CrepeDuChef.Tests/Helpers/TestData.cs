using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Tests.Helpers
{
    public static class TestData
    {
        public static List<User> UsersWithIds(params int[] ids)
        {
            return [.. ids.Select(id => new User { Id = IntToGuid(id) })];
        }

        public static List<CrepesParty> CrepePartiesWithUserIds(params int[] ids)
        {
            return [..ids.Select(id => new CrepesParty { UserId = IntToGuid(id) })];
        }


        public static Guid IntToGuid(int value)
        {
            // Convertit l'int en chaîne décimale sur 12 chiffres
            string decimalPart = value.ToString().PadLeft(12, '0');

            // Construit le GUID complet
            string guidString = $"00000000-0000-0000-0000-{decimalPart}";

            return Guid.Parse(guidString);
        }
    }
}
