using System.Globalization;

namespace CrepeDuChef.Maui.Converters
{
    internal class IsTodayConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime dt)
            {
#if DEBUG
                return DateTime.Now - dt < TimeSpan.FromMinutes(2);
#else
                return dt.Date == DateTime.Now.Date;
#endif
            }
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
