using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Interfaces;
using CrepeDuChef.Common.Models;
using CrepeDuChef.Maui.PopupElements;
using CrepeDuChef.Maui.Resources.languages;
using Microsoft.Maui.Controls.Shapes;

namespace CrepeDuChef.Maui.Services
{
    public class UserDtoPopupService : IUserDtoPopupService
    {
        private readonly int _delayBetweenPopup = 300;
        private readonly SemaphoreSlim _popupLock = new SemaphoreSlim(1, 1);

        private readonly IUserFormResultMapper _mapper;
        private readonly IUserPopupFactory<Popup, UserDto> _popupFactory;

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

        public UserDtoPopupService(
            IUserFormResultMapper mapper,
            IUserPopupFactory<Popup, UserDto> popupFactory)
        {
            _mapper = mapper;
            _popupFactory = popupFactory;
        }

        public async Task<UserDataResult> ShowAddUserFormAsync()
        {
            return await ShowUserFormAsync(_popupFactory.CreateAddUserForm);
        }

        public async Task<UserDataResult> ShowUpdateUserFormAsync(UserDto usr)
        {
            return await ShowUserFormAsync(() => _popupFactory.CreateUpdateUserForm(usr));
        }

        private async Task<UserDataResult> ShowUserFormAsync(
            Func<Popup> createPopup)
        {
            await _popupLock.WaitAsync();

            try
            {
                Popup popup =
                    createPopup();

                IPopupResult<UserDto?> results = await Shell.Current.ShowPopupAsync<UserDto?>(popup, generateOptions());


                return _mapper.Map(results.Result);
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
                await Shell.Current.ShowPopupAsync(messagePop, generateOptions());

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
                await Shell.Current.ShowPopupAsync(messagePop, generateOptions());

            }
            finally
            {
                await Task.Delay(_delayBetweenPopup);
                _popupLock.Release();
            }
        }
    }
}
