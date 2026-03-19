using System.Globalization;

namespace CrepeDuChef.Maui.Converters
{
    internal class IsTodayConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime dt)
            {
                return DateTime.Now - dt < TimeSpan.FromMinutes(2);

                //return dt.Date == DateTime.Now.Date;
            }
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
