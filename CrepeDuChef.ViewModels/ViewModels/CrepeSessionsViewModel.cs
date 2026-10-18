using System.Collections.ObjectModel;
using System.Diagnostics;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Mappers;
using CrepeDuChef.Application.Models;
using CrepeDuChef.ViewModels.Presenters;

using AppDto = CrepeDuChef.Application.DTOs;
using AppModel = CrepeDuChef.Application.Models;
using AppRequests = CrepeDuChef.Application.Models.Requests;
using AppServices = CrepeDuChef.Application.Interfaces;
using AppValueObject = CrepeDuChef.Application.ValueObjects;
using DomainExceptions = CrepeDuChef.Domain.Exceptions;
using Loca = CrepeDuChef.Localization.Resources.languages;

namespace CrepeDuChef.ViewModels.ViewModels
{
    public partial class CrepeSessionsViewModel : ObservableObject
    {
        private bool _isInitialized = false;
        private readonly SemaphoreSlim _updateLock = new(1, 1);
        private readonly AppServices.IDeviceIdProvider _deviceId;
        private readonly AppServices.IDateTimeProvider _dateTimeProvider;
        private readonly AppServices.INetworkStatusService _network;


        public bool IsOnline => _network.IsOnline();


        private Guid DeviceId => _deviceId.DeviceId;

        #region Application Services
        private readonly AppServices.IChefRotationLocalService _localRotation;
        private readonly AppServices.ICrepePartyLocalService _crepePartyLocalService;
        private readonly AppServices.IChefLocalService _chefLocalService;
        #endregion

        #region Vm presenters
        private IDialogPresenter UserDialogService { get; }
        private IUserSelectionPresenter UserSelectService { get; }
        #endregion

        #region Traduction
        public string CrepeDuChef_title => Loca.Traduction.CrepeDuChef_title;
        public string WhoIsTheChef => Loca.Traduction.WhoIsTheChef;
        public string UsersAway => Loca.Traduction.UsersAway;
        public string NoSessionInDb => Loca.Traduction.NoSessionInDb;

        #endregion

        public CrepeSessionsViewModel(
            IDialogPresenter userDialogService,
            IUserSelectionPresenter userSelectService,
            AppServices.IDeviceIdProvider deviceId,
            AppServices.IDateTimeProvider dateTimeProvider,
            AppServices.IChefRotationLocalService localRotation,
            AppServices.INetworkStatusService network,
            AppServices.IChefLocalService chefLocalService,
            AppServices.ICrepePartyLocalService crepePartyLocalService)
        {
            UserDialogService = userDialogService;
            UserSelectService = userSelectService;
            _deviceId = deviceId;
            _dateTimeProvider = dateTimeProvider;
            _localRotation = localRotation;
            _network = network;
            _chefLocalService = chefLocalService;
            _crepePartyLocalService = crepePartyLocalService;
        }


        [ObservableProperty]
        public partial IReadOnlyList<AppModel.CrepePartySession> Sessions { get; private set; } = Array.Empty<AppModel.CrepePartySession>();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TextSwitchMessage))]
        public partial bool ShowOnlyCurrent { get; set; } = true;


        public string OnContentOnlyCurrentSession => Loca.Traduction.ShowOnlyCurrentSession;
        public string OffOnContentAllSession => Loca.Traduction.ShowAllSession;
        public string TextSwitchMessage =>
            ShowOnlyCurrent
                ? OnContentOnlyCurrentSession
                : OffOnContentAllSession;

        private List<AppModel.CrepePartySession> _allSessions = [];

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanUpdate))]
        public partial bool IsUpdating { get; private set; }

        public bool CanUpdate => !IsUpdating;

        [ObservableProperty]
        private partial ObservableCollection<AppDto.UserDto> ChefsAvailable { get; set; } = [];

        partial void OnChefsAvailableChanged(ObservableCollection<AppDto.UserDto> value)
        {
            GetNewChefCommand.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private partial ObservableCollection<AppDto.UserDto> AllChefs { get; set; } = [];

        partial void OnAllChefsChanged(ObservableCollection<AppDto.UserDto> value)
        {
            SelectAvailableUsersCommand.NotifyCanExecuteChanged();
            OnPropertyChanged(nameof(IsMoreThanOneChef));
        }

        public async Task OnAppearingAsync()
        {
            if (_isInitialized)
            {
                return;
            }

            await InitializeAsync();
            _isInitialized = true;

            await RefreshSessionsAsync();
            ApplyFilter();
        }

        private async Task InitializeAsync()
        {
            AllChefs =
                new ObservableCollection<AppDto.UserDto>(
                    (await _chefLocalService.GetAllChefsAsync())
                    .Select(u => u.ToDto()));
            
            ChefsAvailable =
                new ObservableCollection<AppDto.UserDto>(AllChefs);
        }

        private async Task RefreshSessionsAsync()
        {
            if (!await _updateLock.WaitAsync(0))
            {
                return;
            }

            IsUpdating = true;

            try
            {
                List<CrepePartySession> sessions =
                    await _crepePartyLocalService.GetSessionsAsync();

                _allSessions = sessions
                    .OrderByDescending(s => s.SessionNumber)
                    .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsUpdating = false;
                _updateLock.Release();
            }
        }

        partial void OnShowOnlyCurrentChanged(bool value)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            IEnumerable<AppModel.CrepePartySession> filtered =
                ShowOnlyCurrent
                    ? _allSessions.Take(1)
                    : _allSessions;

            Sessions = filtered.ToList();
        }

        private bool CanGetNewChef() => ChefsAvailable?.Count > 0;

        [RelayCommand(CanExecute = nameof(CanGetNewChef))]
        public async Task GetNewChef()
        {
            try
            {
                List<AppDto.UserDto> chefsCopy = [.. ChefsAvailable];

                AppValueObject.ChefSelectionResult nextChefResult = await _localRotation.GetNextChefAsync(chefsCopy);

                AppDto.AddCrepePartyDto addCrepe =
                    new(
                            Date: _dateTimeProvider.UtcNow,
                            SessionNumber: nextChefResult.SessionNumber,
                            UserId: nextChefResult.User.Id
                        );

                await _crepePartyLocalService.AddCrepePartyAsync(addCrepe, DeviceId);

                await RefreshSessionsAsync();
                ApplyFilter();

                await UserDialogService.ShowSuccessAsync(
                    title: Loca.Traduction.Today_s_Chef,
                    message: $"{nextChefResult.User.FullName}");
            }
            catch (DomainExceptions.NoChefException)
            {
                await UserDialogService.ShowWarningAsync(
                    title: Loca.Traduction.NoChefInDB,
                    message: Loca.Traduction.NeedAtLeastOneChef);
            }
            catch (DomainExceptions.NoChefSelectionException)
            {
                await UserDialogService.ShowWarningAsync(
                    title: Loca.Traduction.Error,
                    message: Loca.Traduction.NoChefSelectionExceptionMessage);
            }
            catch (Exception ex)
            {
                await UserDialogService.ShowWarningAsync(
                    title: Loca.Traduction.Error,
                    message: $"{ex.Message}");
            }
        }

        public bool IsMoreThanOneChef => AllChefs?.Count > 1;


        private bool CanSelectAvailable() => IsMoreThanOneChef;

        [RelayCommand(CanExecute = nameof(CanSelectAvailable))]
        public async Task SelectAvailableUsers()
        {
            try
            {
                if (AllChefs == null || AllChefs.Count == 0)
                {
                    await UserDialogService.ShowWarningAsync(
                        title: Loca.Traduction.NoChefInDB,
                        message: Loca.Traduction.NeedAtLeastOneChef);

                    return;
                }

                ChefsAvailable ??= [.. AllChefs];

                HashSet<Guid> availableIds = [.. ChefsAvailable.Select(c => c.Id)];
                List<AppDto.UserDto> availableChefs = [.. AllChefs.Where(c => availableIds.Contains(c.Id))];

                AppDto.UserDto[]? dialogResult =
                        await UserSelectService.SelectUsersAsync(
                            title: Loca.Traduction.ChefsPresent,
                            message: Loca.Traduction.SelectAvailableChefs,
                            allUsers: [.. AllChefs],
                            selectedUsers: availableChefs);

                if (dialogResult is null)
                {
                    return;
                }

                List<AppDto.UserDto> selectedUsers = [.. dialogResult];

                if (!selectedUsers.Any())
                {
                    await UserDialogService.ShowWarningAsync(
                        title: Loca.Traduction.NoChefSelected,
                        message: Loca.Traduction.SelectAtLeastOneChef);
                    return;
                }

                ChefsAvailable = [.. selectedUsers];
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{Loca.Traduction.UnexpectedError}: {ex}");
            }
        }

        [RelayCommand]
        public async Task ToggleSwitch()
        {
            ShowOnlyCurrent = !ShowOnlyCurrent;
        }
    }
}
