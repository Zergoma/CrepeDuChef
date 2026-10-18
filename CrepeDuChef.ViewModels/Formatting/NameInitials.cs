namespace CrepeDuChef.ViewModels.Formatting
{
    public static class NameInitials
    {
        /// <summary>
        /// "Alice Delta-Colli Loth" -> "AD": first letter of the first two words, for avatars.
        /// </summary>
        public static string From(string? fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return string.Empty;
            }

            return string.Concat(fullName
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Take(2)
                .Select(word => char.ToUpperInvariant(word[0])));
        }
    }
}
