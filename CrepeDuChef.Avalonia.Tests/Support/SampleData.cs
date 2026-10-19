using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.ViewModels.Presenters;
using CrepeDuChef.ViewModels.ViewModels;
using NSubstitute;

namespace CrepeDuChef.Avalonia.Tests.Support
{
    /// <summary>
    /// View models fed with fake services and realistic data, for rendering screens.
    /// </summary>
    public static class SampleData
    {
        public static readonly IReadOnlyList<ChefDto> Chefs =
        [
            new() { Id = Guid.NewGuid(), FirstName = "Guillaume", LastName = "Delta-Colli" },
            new() { Id = Guid.NewGuid(), FirstName = "Alice", LastName = "Delta-Colli Loth" },
            new() { Id = Guid.NewGuid(), FirstName = "Camille", LastName = "Martin" },
            new() { Id = Guid.NewGuid(), FirstName = "Hugo", LastName = "Bernard" },
        ];

        public static IReadOnlyList<CrepePartySessionDto> Sessions()
        {
            DateTime now = DateTime.UtcNow;

            CrepePartyEntryDto Entry(int chef, double daysAgo) => new()
            {
                ChefId = Chefs[chef].Id,
                ChefName = Chefs[chef].FullName,
                DateUtc = now.AddDays(-daysAgo),
            };

            return
            [
                new() { SessionNumber = 15, Entries = [Entry(1, 0), Entry(0, 7)] },
                new() { SessionNumber = 14, Entries = [Entry(3, 14), Entry(2, 21), Entry(1, 28), Entry(0, 35)] },
            ];
        }

        public static CrepeSessionsViewModel CrepeSessionsViewModel(bool empty = false)
        {
            IChefService chefService = Substitute.For<IChefService>();
            chefService.GetAllAsync().Returns(empty ? [] : Chefs);

            ICrepePartyService crepePartyService = Substitute.For<ICrepePartyService>();
            crepePartyService.GetSessionsAsync().Returns(empty ? [] : Sessions());

            return new CrepeSessionsViewModel(
                chefService,
                Substitute.For<IChefRotationService>(),
                crepePartyService,
                Substitute.For<IDialogPresenter>(),
                Substitute.For<IChefSelectionPresenter>());
        }

        public static ChefSettingsViewModel ChefSettingsViewModel(bool empty = false)
        {
            IChefService chefService = Substitute.For<IChefService>();
            chefService.GetAllAsync().Returns(empty ? [] : Chefs);

            return new ChefSettingsViewModel(
                chefService,
                Substitute.For<IChefFormPresenter>(),
                Substitute.For<IDialogPresenter>());
        }
    }
}
