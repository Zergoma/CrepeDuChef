using Microsoft.Extensions.Localization;
using System.Globalization;

namespace CrepeDuChef.Tests.Helpers
{
    public class FakeLocalizer<T> : IStringLocalizer<T>
    {
        public LocalizedString this[string name]
            => new(name, name);

        public LocalizedString this[string name, params object[] arguments]
            => new(name, string.Format(name, arguments));

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
            => Enumerable.Empty<LocalizedString>();

        public IStringLocalizer WithCulture(CultureInfo culture)
            => this;
    }

}
