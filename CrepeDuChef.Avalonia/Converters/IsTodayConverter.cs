using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace CrepeDuChef.Avalonia.Converters
{
    /// <summary>
    /// True when the (local) date is today, to highlight today's crepe party.
    /// </summary>
    public class IsTodayConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
            => value is DateTime date && date.Date == DateTime.Now.Date;

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}
