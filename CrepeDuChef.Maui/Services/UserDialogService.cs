using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Interfaces;
using UraniumUI.Dialogs;
using UXDivers.Popups.Maui.Controls;
using UXDivers.Popups.Services;

namespace CrepeDuChef.Maui.Services
{
    public class UserDialogService : IUserDialogService
    {
        private readonly int _delayBetweenPopup = 300;
        private readonly SemaphoreSlim _popupLock = new SemaphoreSlim(1, 1);

        private readonly IServiceProvider _provider;

        // Uranium dialog
        private IDialogService DialogService => _provider.GetRequiredService<IDialogService>();

        // UXDivers.Popups
        private IPopupService PopupService => _provider.GetRequiredService<IPopupService>();

        public UserDialogService(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task ShowWarningAsync(string title, string message)
        {
            await _popupLock.WaitAsync();
            try
            {
                var floater =
                    new FloaterPopup
                    {
                        Title = title,
                        Text = message,
                        IconColor = Colors.Orange
                    };

                await PopupService.PushAsync(floater);
            }
            finally
            {
                await Task.Delay(_delayBetweenPopup);
                _popupLock.Release();
            }
        }

        public async Task ShowMessageAsync(string title, string message)
        {
            await _popupLock.WaitAsync();
            try
            {
                var floater =
                    new FloaterPopup
                    {
                        Title = title,
                        Text = message,
                        IconColor = Colors.GreenYellow
                    };

                await PopupService.PushAsync(floater);
            }
            finally
            {
                await Task.Delay(_delayBetweenPopup);
                _popupLock.Release();
            }
        }

        public async Task<IEnumerable<UserDto>?> SelectUsersAsync(
            string title,
            IEnumerable<UserDto> allUsers,
            IEnumerable<UserDto> selectedUsers,
            string propertyToDisplay)
        {
            await _popupLock.WaitAsync();

            try
            {
                return await DialogService.DisplayCheckBoxPromptAsync(
                    title,
                    allUsers,
                    selectedUsers,
                    displayMember: propertyToDisplay);
            }
            finally
            {
                await Task.Delay(_delayBetweenPopup);
                _popupLock.Release();
            }
        }
    }
}
