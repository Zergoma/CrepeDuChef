using CrepeDuChef.Localization.Resources;
using FluentAssertions;
using System.Collections;
using System.Globalization;
using System.Resources;

namespace CrepeDuChef.Tests.Localization
{
    public class ResourceTranslationTests
    {
        public static TheoryData<string> Resources => new()
        {
            nameof(UiResources),
            nameof(ValidationResources),
        };

        private static ResourceManager ManagerFor(string resource) => resource switch
        {
            nameof(UiResources) => UiResources.ResourceManager,
            nameof(ValidationResources) => ValidationResources.ResourceManager,
            _ => throw new ArgumentOutOfRangeException(nameof(resource)),
        };

        private static HashSet<string> KeysOf(ResourceManager manager, CultureInfo culture)
        {
            ResourceSet set = manager.GetResourceSet(culture, createIfNotExists: true, tryParents: false)
                ?? throw new InvalidOperationException($"No resources for culture '{culture.Name}'.");

            return [.. set.Cast<DictionaryEntry>().Select(e => (string)e.Key)];
        }

        [Theory]
        [MemberData(nameof(Resources))]
        public void French_Should_Translate_Exactly_The_Default_Keys(string resource)
        {
            // Arrange
            ResourceManager manager = ManagerFor(resource);

            // Act
            HashSet<string> defaultKeys = KeysOf(manager, CultureInfo.InvariantCulture);
            HashSet<string> frenchKeys = KeysOf(manager, new CultureInfo("fr"));

            // Assert
            frenchKeys.Should().BeEquivalentTo(defaultKeys);
        }
    }
}
