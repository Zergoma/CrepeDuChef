using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.ValueObjects;
using CrepeDuChef.Maui.Resources.languages;
using CrepeDuChef.Maui.UI.Popups.Factories;
using CrepeDuChef.Maui.UI.Popups.Views;
using Microsoft.Maui.Controls.Shapes;

namespace CrepeDuChef.Maui.UI.Popups.Presenters
{
    public class UserPopupPresenter : IUserPopupPresenter
    {
        private const int _delayBetweenPopup = 300;
        private readonly SemaphoreSlim _popupLock = new SemaphoreSlim(1, 1);

        private readonly IUserFormPopupFactory _popupFactory;

        private static Color OverlayColor =>
            App.Current!.RequestedTheme == AppTheme.Dark
                ? Colors.Black.WithAlpha(0.5f)
                : Colors.White.WithAlpha(0.5f);

        private PopupOptions GenerateOptions()
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

        public UserPopupPresenter(
            IUserFormPopupFactory popupFactory)
        {
            _popupFactory = popupFactory;
        }

        public async Task<UserFormData?> ShowAddUserFormAsync()
        {
            return await ShowUserFormAsync(_popupFactory.CreateAddUserForm);
        }

        public async Task<UserFormData?> ShowUpdateUserFormAsync(UserDto usr)
        {
            return await ShowUserFormAsync(() => _popupFactory.CreateUpdateUserForm(usr));
        }

        private async Task<UserFormData?> ShowUserFormAsync(
            Func<Popup> createPopup)
        {
            await _popupLock.WaitAsync();

            try
            {
                Popup popup =
                    createPopup();

                IPopupResult<UserFormData?> results =
                    await Shell.Current.ShowPopupAsync<UserFormData?>(popup, GenerateOptions());

                return results.Result;
            }
            finally
            {
                await Task.Delay(_delayBetweenPopup);
                _popupLock.Release();
            }
        }

        public async Task ShowUpdateSuccessedOperationAsync()
        {
            await _popupLock.WaitAsync();

            try
            {
                TitledMessagePopup messagePop =
                    new (
                        title:Traduction.Updated,
                        message: Traduction.InformationUpdated)
                    {
                        IconColor = Colors.GreenYellow
                    };
                await Shell.Current.ShowPopupAsync(messagePop, GenerateOptions());

            }
            finally
            {
                await Task.Delay(_delayBetweenPopup);
                _popupLock.Release();
            }
        }

        public async Task ShowAbortedOperationAsync(string message)
        {
            await _popupLock.WaitAsync();

            try
            {
                TitledMessagePopup messagePop =
                    new (
                        title: Traduction.OperationCanceled,
                        message: message)
                    {
                        IconColor = Colors.Orange
                    };
                await Shell.Current.ShowPopupAsync(messagePop, GenerateOptions());

            }
            finally
            {
                await Task.Delay(_delayBetweenPopup);
                _popupLock.Release();
            }
        }
    }
}
