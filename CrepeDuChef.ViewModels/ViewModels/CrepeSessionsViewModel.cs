using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AppServices = CrepeDuChef.Application.Interfaces;
using AppDto = CrepeDuChef.Application.DTOs;
using AppModel = CrepeDuChef.Application.Models;
using DomainExceptions = CrepeDuChef.Domain.Exceptions;
using Loca = CrepeDuChef.Localization.Resources.languages;
using CrepeDuChef.ViewModels.Presenters;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CrepeDuChef.ViewModels.ViewModels
{
    public partial class CrepeSessionsViewModel : ObservableObject
    {
        private bool _isInitialized = false;
        private readonly SemaphoreSlim _updateLock = new(1, 1);

        #region Application Services
        private AppServices.IChefRotationService ChefRotationService { get; }
        private AppServices.ICrepePartyService CrepService { get; }
        #endregion

        #region Vm presenters
        private IDialogPresenter UserDialogService { get; }
        private IUserSelectionPresenter UserSelectService { get; }
        #endregion

        #region Traduction
        public static string CrepeDuChef_title => Loca.Traduction.CrepeDuChef_title;
        public static string WhoIsTheChef => Loca.Traduction.WhoIsTheChef;
        public static string UsersAway => Loca.Traduction.UsersAway;
        public static string NoSessionInDb => Loca.Traduction.NoSessionInDb;

        #endregion

        public CrepeSessionsViewModel(
            AppServices.IChefRotationService chefRotationService,
            AppServices.ICrepePartyService crepService,
            IDialogPresenter userDialogService,
            IUserSelectionPresenter userSelectService
            )
        {
            ChefRotationService = chefRotationService;
            CrepService = crepService;
            UserDialogService = userDialogService;
            UserSelectService = userSelectService;
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
            AllChefs = new ObservableCollection<AppDto.UserDto>(await CrepService.GetAllChefsAsync());
            ChefsAvailable = new ObservableCollection<AppDto.UserDto>(AllChefs);
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
                IEnumerable<AppModel.CrepePartySession> sessions =
                    await CrepService.GetSessionsAsync();

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

                (AppDto.UserDto? selectedUser, int sessionNumber) =
                    await ChefRotationService.GetNextChefAsync(chefsCopy);

                await CrepService.AddCrepePartyAsync(
                    new AppDto.CrepesPartyDto
                    {
                        UserId = selectedUser.Id,
                        SessionNumber = sessionNumber,
                        Date = DateTime.UtcNow
                    });

                await RefreshSessionsAsync();
                ApplyFilter();

                await UserDialogService.ShowSuccessAsync(
                    title: Loca.Traduction.Today_s_Chef,
                    message: $"{selectedUser.FirstName} {selectedUser.LastName}");
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

                HashSet<int> availableIds = [.. ChefsAvailable.Select(c => c.Id)];
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
