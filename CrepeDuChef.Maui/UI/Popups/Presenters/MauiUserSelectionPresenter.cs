using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Extensions;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.ValueObjects;
using CrepeDuChef.Maui.UI.Popups.Core;
using CrepeDuChef.Maui.UI.Popups.Factories;
using VmPresenters = CrepeDuChef.ViewModels.Presenters;

namespace CrepeDuChef.Maui.UI.Popups.Presenters
{
    public class MauiUserSelectionPresenter : VmPresenters.IUserSelectionPresenter
    {
        private readonly PopupCoordinator _coordinator;
        private readonly PopupOptionsFactory _options;
        private readonly IUserSelectionPopupFactory _factory;

        public MauiUserSelectionPresenter(
            PopupCoordinator coordinator,
            PopupOptionsFactory options,
            IUserSelectionPopupFactory factory)
        {
            _coordinator = coordinator;
            _options = options;
            _factory = factory;
        }

        public Task<UserDto[]?> SelectUsersAsync(
            string title,
            string message,
            List<UserDto> allUsers,
            List<UserDto> selectedUsers)
        {
            return _coordinator.RunAsync(async () =>
            {
                var popup =
                    _factory.CreateSelectionPopup(
                        title,
                        message,
                        allUsers,
                        selectedUsers);

                IPopupResult<UserSelectionResult> resu =
                    await Shell.Current.ShowPopupAsync<UserSelectionResult>(
                        popup,
                        _options.Create());

                return resu?.Result?.Users;
            });
        }
    }
}
