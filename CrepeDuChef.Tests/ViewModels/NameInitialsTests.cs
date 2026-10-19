using CrepeDuChef.ViewModels.Formatting;
using FluentAssertions;

namespace CrepeDuChef.Tests.ViewModels
{
    public class NameInitialsTests
    {
        [Theory]
        [InlineData("Alice Delta-Colli Loth", "AD")]
        [InlineData("john doe", "JD")]
        [InlineData("Cher", "C")]
        [InlineData("  Hugo   Bernard ", "HB")]
        [InlineData("", "")]
        [InlineData(null, "")]
        public void From_Should_Take_The_First_Letter_Of_The_First_Two_Words(string? name, string expected)
            => NameInitials.From(name).Should().Be(expected);
    }
}
