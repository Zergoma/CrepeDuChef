using Avalonia.Data.Converters;
using CrepeDuChef.ViewModels.Formatting;
using System;
using System.Globalization;

namespace CrepeDuChef.Avalonia.Converters
{
    /// <summary>
    /// Full name -> initials, for avatars.
    /// </summary>
    public class InitialsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
            => NameInitials.From(value as string);

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}
