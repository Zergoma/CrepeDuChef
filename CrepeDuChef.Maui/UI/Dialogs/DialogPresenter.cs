using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Maui.UI.Extentions;
using CrepeDuChef.Maui.UI.Popups.Views;
using Microsoft.Maui.Controls.Shapes;
using UraniumUI.Dialogs;

namespace CrepeDuChef.Maui.UI.Dialogs
{
    public class DialogPresenter : IDialogPresenter
    {
        private readonly int _delayBetweenPopup = 300;
        private readonly SemaphoreSlim _popupLock = new SemaphoreSlim(1, 1);

        private readonly IServiceProvider _provider;

        // Uranium dialog
        private IDialogService DialogService => _provider.GetRequiredService<IDialogService>();

        private static Color OverlayColor =>
           App.Current!.RequestedTheme == AppTheme.Dark
               ? Colors.Black.WithAlpha(0.5f)
               : Colors.White.WithAlpha(0.5f);

        private PopupOptions generateOptions()
        {
            return new PopupOptions
            {
                Shape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(12),
                    Stroke = Colors.Transparent,
                    StrokeThickness = 0
                },
                Shadow = null,
                PageOverlayColor = OverlayColor
            };
        }

        public DialogPresenter(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task ShowWarningAsync(string title, string message)
        {
            await _popupLock.WaitAsync();
            try
            {
                TitledMessagePopup messagePop =
                    new(
                        title: title,
                        message: message)
                    {
                        IconColor = Colors.Orange
                    };
                await Shell.Current.ShowPopupAsync(messagePop, generateOptions());
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
                TitledMessagePopup messagePop =
                    new(
                        title: title,
                        message: message)
                    {
                        IconColor = Colors.GreenYellow
                    };
                await Shell.Current.ShowPopupAsync(messagePop, generateOptions());
            }
            finally
            {
                await Task.Delay(_delayBetweenPopup);
                _popupLock.Release();
            }
        }

        public async Task<DialogResult<List<UserDto>>> SelectUsersAsync(
            string title,
            List<UserDto> allUsers,
            List<UserDto> selectedUsers,
            string? propertyToDisplay = null)
        {
            await _popupLock.WaitAsync();

            try
            {
                List<UserDto> resu =
                    [.. (await DialogService.DisplayCheckBoxPromptAsync(
                        title,
                        allUsers,
                        selectedUsers,
                        displayMember: propertyToDisplay))];

                return resu.ToResult();
            }
            finally
            {
                await Task.Delay(_delayBetweenPopup);
                _popupLock.Release();
            }
        }
    }
}
