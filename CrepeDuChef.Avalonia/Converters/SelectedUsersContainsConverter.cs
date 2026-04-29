using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Collections;
using System.Globalization;

namespace CrepeDuChef.Avalonia.Converters
{
    public class SelectedUsersContainsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is IList list && parameter != null)
                return list.Contains(parameter);

            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}
