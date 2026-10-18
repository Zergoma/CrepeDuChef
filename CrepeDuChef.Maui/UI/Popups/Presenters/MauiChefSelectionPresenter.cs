using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Extensions;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Maui.UI.Popups.Core;
using CrepeDuChef.Maui.UI.Popups.Factories;
using CrepeDuChef.Maui.UI.Popups.Models;
using CrepeDuChef.ViewModels.Presenters;

namespace CrepeDuChef.Maui.UI.Popups.Presenters
{
    public class MauiChefSelectionPresenter : IChefSelectionPresenter
    {
        private readonly PopupCoordinator _coordinator;
        private readonly PopupOptionsFactory _options;
        private readonly IChefSelectionPopupFactory _factory;

        public MauiChefSelectionPresenter(
            PopupCoordinator coordinator,
            PopupOptionsFactory options,
            IChefSelectionPopupFactory factory)
        {
            _coordinator = coordinator;
            _options = options;
            _factory = factory;
        }

        public Task<IReadOnlyList<ChefDto>?> SelectChefsAsync(
            string title,
            string message,
            IReadOnlyList<ChefDto> allChefs,
            IReadOnlyList<ChefDto> selectedChefs)
        {
            return _coordinator.RunAsync(async () =>
            {
                var popup = _factory.CreateSelectionPopup(title, message, allChefs, selectedChefs);

                IPopupResult<ChefSelectionPopupResult?> result =
                    await Shell.Current.ShowPopupAsync<ChefSelectionPopupResult?>(popup, _options.Create());

                return result.Result?.Chefs;
            });
        }
    }
}
