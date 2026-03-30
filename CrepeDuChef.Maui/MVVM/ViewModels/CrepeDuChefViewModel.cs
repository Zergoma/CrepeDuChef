using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Domain.Exceptions;
using CrepeDuChef.Maui.Mappers;
using CrepeDuChef.Maui.Models.UI;
using CrepeDuChef.Maui.Resources.languages;
using CrepeDuChef.Maui.UI.Dialogs;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace CrepeDuChef.Maui.MVVM.ViewModels
{
    public partial class CrepeDuChefViewModel : ObservableObject
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        public partial ObservableCollection<CrepePartyGroup> CrepeSessions { get; set; } = [];

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

        public IChefRotationService ChefRotationService { get; }
        public IDialogPresenter DialogPresenter { get; }
        public ICrepePartyService CrepService { get; }

        private readonly SemaphoreSlim _updateLock = new(1, 1);


        public CrepeDuChefViewModel(
            IChefRotationService chefRotationService,
            IDialogPresenter DialogPresenter,
            ICrepePartyService crepService)
        {
            ChefRotationService = chefRotationService;
            this.DialogPresenter = DialogPresenter;
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
            AllChefs = new ObservableCollection<UserDto> ( await CrepService.GetAllChefsAsync() );
            ChefsAvailable = new ObservableCollection<UserDto>(AllChefs);
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
                var sessions = await CrepService.GetSessionsAsync();

                _allSessions = CrepePartySessionToGroupMapper
                    .MapToGroups(sessions)
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
            IEnumerable<CrepePartyGroup> filtered =
                ShowOnlyCurrent
                    ? _allSessions.Take(1)
                    : _allSessions;

            CrepeSessions = new ObservableCollection<CrepePartyGroup>(filtered);
        }

        private bool CanGetNewChef() => ChefsAvailable?.Count > 0;

        [RelayCommand(CanExecute = nameof(CanGetNewChef))]
        public async Task GetNewChef()
        {
            try
            {
                List<UserDto> chefsCopy = [.. ChefsAvailable];
                
                (UserDto? selectedUser, int sessionNumber) =
                    await ChefRotationService.GetNextChefAsync(chefsCopy);

                await CrepService.AddCrepePartyAsync(
                    new CrepesPartyDto
                    {
                        UserId = selectedUser.Id,
                        SessionNumber = sessionNumber,
                        Date = DateTime.UtcNow
                    });

                await RefreshSessionsAsync();
                ApplyFilter();

                await DialogPresenter.ShowMessageAsync(
                    Traduction.Today_s_Chef,
                    $"{selectedUser.FirstName} {selectedUser.LastName}");
            }
            catch (NoChefException)
            {
                await DialogPresenter.ShowWarningAsync(
                    Traduction.NoChefInDB,
                    Traduction.NeedAtLeastOneChef);
            }
            catch (NoChefSelectionException)
            {
                await DialogPresenter.ShowWarningAsync(
                    Traduction.Error,
                    Traduction.NoChefSelectionExceptionMessage);
            }
            catch (Exception ex)
            {
                await DialogPresenter.ShowWarningAsync(
                    Traduction.Error,
                    $"{ex.Message}");
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
                    await DialogPresenter.ShowWarningAsync(
                        Traduction.NoChefInDB,
                        Traduction.NeedAtLeastOneChef);

                    return;
                }

                ChefsAvailable ??= [.. AllChefs];

                HashSet<int> availableIds = [.. ChefsAvailable.Select(c => c.Id)];
                List<UserDto> availableChefs = [.. AllChefs.Where(c => availableIds.Contains(c.Id))];

                DialogResult<List<UserDto>> dialogResult =
                        await DialogPresenter.SelectUsersAsync(
                            title: Traduction.ChefsPresent,
                            allUsers: [.. AllChefs],
                            selectedUsers: availableChefs);
                

                if (dialogResult.Status != DialogResultStatus.Success
                    || dialogResult.Data == null)
                {
                    return;
                }

                List<UserDto> selectedUsers = [.. dialogResult.Data];

                if (!selectedUsers.Any())
                {
                    await DialogPresenter.ShowWarningAsync(
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
