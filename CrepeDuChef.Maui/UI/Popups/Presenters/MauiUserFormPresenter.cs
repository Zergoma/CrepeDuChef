using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Maui.UI.Popups.Core;
using CrepeDuChef.Maui.UI.Popups.Factories;
using CrepeDuChef.ViewModels.Models;
using CrepeDuChef.ViewModels.Presenters;

namespace CrepeDuChef.Maui.UI.Popups.Presenters
{
    public class MauiUserFormPresenter : IChefFormPresenter
    {
        private readonly PopupCoordinator _coordinator;
        private readonly PopupOptionsFactory _options;
        private readonly IUserFormPopupFactory _factory;

        public MauiUserFormPresenter(
            PopupCoordinator coordinator,
            PopupOptionsFactory options,
            IUserFormPopupFactory factory)
        {
            _coordinator = coordinator;
            _options = options;
            _factory = factory;
        }

        public Task<ChefFormData?> ShowAddChefFormAsync()
            => ShowFormAsync(_factory.CreateAddUserForm);

        public Task<ChefFormData?> ShowUpdateChefFormAsync(ChefDto chef)
            => ShowFormAsync(() => _factory.CreateUpdateUserForm(chef));

        private Task<ChefFormData?> ShowFormAsync(Func<Popup> createPopup)
        {
            return _coordinator.RunAsync(async () =>
            {
                IPopupResult<ChefFormData?> result =
                    await Shell.Current.ShowPopupAsync<ChefFormData?>(createPopup(), _options.Create());

                return result.Result;
            });
        }
    }
}
