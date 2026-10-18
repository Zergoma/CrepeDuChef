using Avalonia.Data.Converters;
using System;
using System.Globalization;
using System.Linq;

namespace CrepeDuChef.Avalonia.Converters
{
    /// <summary>
    /// "Alice Delta-Colli Loth" -> "AD": first letter of the first two words, for avatars.
    /// </summary>
    public class InitialsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not string name || string.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }

            return string.Concat(name
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Take(2)
                .Select(word => char.ToUpper(word[0], culture)));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}
