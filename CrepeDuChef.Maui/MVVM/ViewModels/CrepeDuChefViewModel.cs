using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrepeDuChef.Common;
using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Exceptions;
using CrepeDuChef.Common.Interfaces;
using CrepeDuChef.Common.Models;
using CrepeDuChef.Maui.Mappers;
using CrepeDuChef.Maui.MVVM.Models;
using CrepeDuChef.Maui.Resources.languages;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace CrepeDuChef.Maui.MVVM.ViewModels
{
    public partial class CrepeDuChefViewModel : ObservableObject
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        public partial ObservableCollection<CrepePartyGroup> CrepeSessions { get; set; } = new();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TextSwitchMessage))]
        public partial bool ShowOnlyCurrent { get; set; } = true;


        public string TextSwitchMessage =>
            ShowOnlyCurrent
                ? Traduction.ShowOnlyCurrentSession
                : Traduction.ShowAllSession;


        private List<CrepePartyGroup> _allSessions = [];

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanUpdate))]
        public partial bool IsUpdating { get; private set; }

        public bool CanUpdate => !IsUpdating;

        [ObservableProperty]
        private partial ObservableCollection<UserDto> ChefsAvailable { get; set; } = [];

        partial void OnChefsAvailableChanged(ObservableCollection<UserDto> value)
        {
            GetNewChefCommand.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private partial ObservableCollection<UserDto> AllChefs { get; set; } = [];
        partial void OnAllChefsChanged(ObservableCollection<UserDto> value)
        {
            SelectAvailableUsersCommand.NotifyCanExecuteChanged();
            OnPropertyChanged(nameof(IsMoreThanOneChef));
        }

        public ICrepePartyRepository CrepePartyRepo { get; }
        public IChefRotationService ChefRotationService { get; }
        public IUserDialogService DialogService { get; }
        public ICrepePartyService CrepService { get; }

        private readonly SemaphoreSlim _updateLock = new(1, 1);


        public CrepeDuChefViewModel(
            ICrepePartyRepository crepePartyRepo,
            IChefRotationService chefRotationService,
            IUserDialogService dial,
            ICrepePartyService crepService)
        {
            CrepePartyRepo = crepePartyRepo;
            ChefRotationService = chefRotationService;
            DialogService = dial;
            CrepService = crepService;
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
                [.. await CrepePartyRepo.GetAllChefsAsync()];

            ChefsAvailable.Clear();
            foreach (var chef in AllChefs)
            {
                ChefsAvailable.Add(chef);
            }
        }

        private async Task RefreshSessionsAsync()
        {
            if (!await _updateLock.WaitAsync(0))
            {
                return;
            }

            if (IsUpdating)
            {
                return;
            }

            IsUpdating = true;

            try
            {
                IEnumerable<CrepePartySession> sessions =
                    await CrepService.GetSessionsAsync();

                IEnumerable<CrepePartyGroup> groups =
                    CrepePartySessionToPartyGroup.MapToGroups(sessions);

                _allSessions = [.. groups];
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
            ObservableCollection<CrepePartyGroup> newlist = [.. FilterSessions()];
            CrepeSessions = newlist;
        }

        private IEnumerable<CrepePartyGroup> FilterSessions()
        {
            if (ShowOnlyCurrent)
                return _allSessions.OrderByDescending(s => s.SessionNumber).Take(1);

            return _allSessions.OrderByDescending(s => s.SessionNumber);
        }


        private bool CanGetNewChef() => ChefsAvailable?.Count > 0;

        [RelayCommand(CanExecute = nameof(CanGetNewChef))]
        public async Task GetNewChef()
        {
            try
            {
                var chefsCopy = ChefsAvailable.ToList();
                var (selectedUser, sessionNumber) =
                    await ChefRotationService.SelectNextChefAsync(chefsCopy);

                await CrepePartyRepo.AddCrepePartyAsync(
                    new CrepesPartyDto
                    {
                        UserId = selectedUser.Id,
                        SessionNumber = sessionNumber,
                        Date = DateTime.UtcNow
                    });

                await CrepePartyRepo.CommitAsync();
                await RefreshSessionsAsync();
                ApplyFilter();
                await DialogService.ShowMessageAsync(Traduction.Today_s_Chef, $"{selectedUser.FirstName} {selectedUser.LastName}");
            }
            catch (NoChefException)
            {
                await DialogService.ShowWarningAsync(Traduction.NoChefInDB, Traduction.NeedAtLeastOneChef);
            }
            catch (NoChefSelectionException)
            {
                await DialogService.ShowWarningAsync(Traduction.Error, Traduction.NoChefSelectionExceptionMessage);
            }
            catch (Exception ex)
            {
                await DialogService.ShowWarningAsync(Traduction.Error, $"{ex.Message}");
            }
        }
        public bool IsMoreThanOneChef => AllChefs?.Count > 1;

        private bool CanSelectAvailable() => IsMoreThanOneChef;
        [RelayCommand(CanExecute = nameof(CanSelectAvailable))]
        public async Task SelectAvailableUsers()
        {
            try
            {
                if (AllChefs == null
                    || AllChefs.Count == 0)
                {
                    await DialogService.ShowWarningAsync(
                        Traduction.NoChefInDB,
                        Traduction.NeedAtLeastOneChef);

                    return;
                }

                HashSet<int> availableIds = [.. ChefsAvailable.Select(c => c.Id)];
                List<UserDto> availableChefs = [.. AllChefs.Where(c => availableIds.Contains(c.Id))];

                DialogResult<IEnumerable<UserDto>> result;

                try
                {
                    result =
                        await DialogService.SelectUsersAsync(
                            title: Traduction.ChefsPresent,
                            allUsers: AllChefs,
                            selectedUsers: availableChefs);
                }
                catch (Exception)
                {
                    return;
                }


                if (result == null
                    || result.Status != DialogResultStatus.Success
                    || result.Data == null)
                {
                    await Task.Yield(); // UI stabilization
                    return;
                }

                List<UserDto> selectedUsers = [.. result.Data];

                if (!selectedUsers.Any())
                {
                    await DialogService.ShowWarningAsync(
                        Traduction.NoChefSelected,
                        Traduction.SelectAtLeastOneChef);
                    return;
                }

                // use it
                ChefsAvailable = [.. selectedUsers];
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SelectAvailableUsers error: {ex}");
            }
        }

        [RelayCommand]
        public async Task ToggleSwitch()
        {
            ShowOnlyCurrent = !ShowOnlyCurrent;
        }
    }
}
