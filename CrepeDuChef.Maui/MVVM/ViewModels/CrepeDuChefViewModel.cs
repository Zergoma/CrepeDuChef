using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Exceptions;
using CrepeDuChef.Common.Extensions;
using CrepeDuChef.Common.Interfaces;
using CrepeDuChef.Maui.Resources.languages;
using System.Collections.ObjectModel;


namespace CrepeDuChef.Maui.MVVM.ViewModels
{
    public partial class CrepeDuChefViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial ObservableCollection<CrepeDisplay> CrepeHistory { get; set; } = new();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanUpdate))]
        public partial bool IsUpdating { get; private set; }

        public bool CanUpdate => !IsUpdating;

        public List<UserDto>? ChefsAvailable { get; set; } = null;

        public ICrepePartyRepository CrepePartyRepo { get; }
        public IChefRotationService ChefRotationService { get; }


        public IUserDialogService _dialogService { get; }


        private readonly SemaphoreSlim _updateLock = new(1, 1);


        public CrepeDuChefViewModel(
            ICrepePartyRepository crepePartyRepo,
            IChefRotationService chefRotationService,
            IUserDialogService dial)
        {
            CrepePartyRepo = crepePartyRepo;
            ChefRotationService = chefRotationService;
            _dialogService = dial;
        }

        public async Task OnAppearingAsync()
        {
            await UpdateDataAsync();
        }

        [RelayCommand]
        private async Task UpdateDataAsync()
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
                List<CrepesPartyDto> allCrepe = await CrepePartyRepo.GetAllCrepePartiesAsync();
                List<UserDto> chefs = await CrepePartyRepo.GetAllChefsAsync();
                Dictionary<int, UserDto> chefsById = chefs.ToDictionary(c => c.Id);

                var result = allCrepe.Select(cp =>
                {
                    // get user just for the name, don't care if it doesn't exist
                    chefsById.TryGetValue(cp.UserId, out var chef);

                    return new CrepeDisplay()
                    {
                        Date = cp.Date,
                        Name = chef?.FullName() ?? Traduction.NoName,
                    };
                }).OrderByDescending(c => c.Date);

                CrepeHistory.Clear();
                foreach (var item in result)
                {
                    CrepeHistory.Add(item);
                }
            }
            finally
            {
                IsUpdating = false;
                _updateLock.Release();
            }
        }

        [RelayCommand]
        public async Task GetNewChef()
        {
            try
            {
                var (selectedUser, sessionNumber) = await ChefRotationService.SelectNextChefAsync(ChefsAvailable);

                await CrepePartyRepo.AddCrepePartyAsync(
                    new CrepesPartyDto
                    {
                        UserId = selectedUser.Id,
                        SessionNumber = sessionNumber,
                        Date = DateTime.UtcNow
                    });

                await CrepePartyRepo.CommitAsync();
                await UpdateDataAsync();
                await _dialogService.ShowMessageAsync(Traduction.Today_s_Chef, $"{selectedUser.FirstName} {selectedUser.LastName}");

            }
            catch (NoChefException)
            {
                await _dialogService.ShowWarningAsync(Traduction.NoChefInDB, Traduction.NeedAtLeastOneChef);
            }
            catch (NoChefSelectionException)
            {
                await _dialogService.ShowWarningAsync(Traduction.Error, Traduction.NoChefSelectionExceptionMessage);
            }
            catch (Exception ex)
            {
                await _dialogService.ShowWarningAsync(Traduction.Error, $"{ex.Message}");
            }
        }

        [RelayCommand]
        public async Task SelectAvailableUsers()
        {
            List<UserDto> allChefs = 
                await CrepePartyRepo.GetAllChefsAsync();

            if (allChefs.Count == 0)
            {
                await _dialogService.ShowWarningAsync(
                    Traduction.NoChefInDB,
                    Traduction.NeedAtLeastOneChef);

                return;
            }

            if (ChefsAvailable is null)
            {
                ChefsAvailable = allChefs;
            }

            HashSet<int> availableIds =
                [.. ChefsAvailable.Select(c => c.Id)];

            // build the selected ones list
            List<UserDto> availableChefs =
                [.. allChefs.Where(c => availableIds.Contains(c.Id))];

            IEnumerable<UserDto>? result =
                await _dialogService.SelectUsersAsync(
                    title: Traduction.ChefsPresent,
                    allUsers: allChefs,
                    selectedUsers: availableChefs,
                    propertyToDisplay: "FirstName");

            // dialog canceled
            if (result is null)
            {
                return;
            }

            if (result.Any() is false)
            {
                try
                {
                    await _dialogService.ShowWarningAsync(
                        Traduction.NoChefSelected,
                        Traduction.SelectAtLeastOneChef);
                }
                catch (Exception)
                {

                }

                return;
            }

            // use it
            ChefsAvailable = [.. result];
        }
    }
}
