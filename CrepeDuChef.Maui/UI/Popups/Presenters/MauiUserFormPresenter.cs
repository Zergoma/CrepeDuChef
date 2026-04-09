using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.ValueObjects;
using CrepeDuChef.Maui.UI.Popups.Core;
using CrepeDuChef.Maui.UI.Popups.Factories;
using SharedPresenters = CrepeDuChef.ViewModels.Presenters;

namespace CrepeDuChef.Maui.UI.Popups.Presenters
{
    public class MauiUserFormPresenter : SharedPresenters.IUserFormPresenter
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

        public Task<UserFormData?> ShowAddUserFormAsync()
            => ShowFormAsync(_factory.CreateAddUserForm);

        public Task<UserFormData?> ShowUpdateUserFormAsync(UserDto user)
            => ShowFormAsync(() => _factory.CreateUpdateUserForm(user));

        private Task<UserFormData?> ShowFormAsync(Func<Popup> createPopup)
        {
            return _coordinator.RunAsync(
                async () =>
                {
                    var popup = createPopup();

                    IPopupResult<UserFormData?> result = 
                        await Shell.Current.ShowPopupAsync<UserFormData?>(
                        popup,
                        _options.Create());

                    UserFormData? res =
                        result.Result;

                    return res;
                });
        }
    }

}
