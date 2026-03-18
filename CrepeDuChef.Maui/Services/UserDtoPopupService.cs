using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Interfaces;
using CrepeDuChef.Maui.Resources.languages;
using UXDivers.Popups.Maui.Controls;
using UXDivers.Popups.Services;

namespace CrepeDuChef.Maui.Services
{
    public class UserDtoPopupService : IUserDtoPopupService
    {
        private readonly int _delayBetweenPopup = 300;
        private readonly SemaphoreSlim _popupLock = new SemaphoreSlim(1, 1);

        private readonly IServiceProvider _provider;
        private readonly IUserFormResultMapper _mapper;
        private readonly IUserPopupFactory<FormPopup, UserDto> _popupFactory;

        // UXDivers.Popups
        private IPopupService PopupService => _provider.GetRequiredService<IPopupService>();

        public UserDtoPopupService(
            IServiceProvider provider,
            IUserFormResultMapper mapper,
            IUserPopupFactory<FormPopup, UserDto> popupFactory)
        {
            _provider = provider;
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
            Func<FormPopup> createPopup)
        {
            await _popupLock.WaitAsync();

            try
            {
                var popup = createPopup();

                var results = await PopupService.PushAsync(popup);

                return _mapper.Map(results);
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
                var floater =
                    new FloaterPopup
                    {
                        Title = Traduction.Updated,
                        Text = Traduction.InformationUpdated,
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

        public async Task ShowAbortedOperationAsync(string message)
        {
            await _popupLock.WaitAsync();

            try
            {
                var floatter =
                    new FloaterPopup
                    {
                        Title = Traduction.OperationCanceled,
                        Text = message,//Traduction.YouMustEnterFirstAndLastName,
                        IconColor = Colors.Orange
                    };
                await PopupService.PushAsync(floatter);

            }
            finally
            {
                await Task.Delay(_delayBetweenPopup);
                _popupLock.Release();
            }
        }
    }
}
