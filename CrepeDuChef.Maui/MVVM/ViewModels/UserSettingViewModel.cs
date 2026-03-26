using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrepeDuChef.Common;
using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Interfaces;
using CrepeDuChef.Common.Models;
using System.Collections.ObjectModel;


namespace CrepeDuChef.Maui.MVVM.ViewModels
{
    public partial class UserSettingViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial ObservableCollection<UserDto> Users { get; set; } = new();
        private ICrepePartyRepository CrepePartyRepo { get; }
        public IUserDtoPopupService UserDtoPopupService { get; }
        [ObservableProperty]
        public partial object? SelectedUser { get; set; } = null;


        public UserSettingViewModel(ICrepePartyRepository CrepePartyRepo, IUserDtoPopupService userDtoPopupService)
        {
            this.CrepePartyRepo = CrepePartyRepo;
            UserDtoPopupService = userDtoPopupService;
        }

        [RelayCommand]
        private async Task UpdateUser()
        {
            List<UserDto> allchefs =
                await CrepePartyRepo.GetAllChefsAsync();
            
            var existingIds = new HashSet<int>(Users.Select(u => u.Id));
            foreach (var chef in allchefs)
            {
                if (!existingIds.Contains(chef.Id))
                {
                    Users.Add(chef);
                }
            }
        }

        [RelayCommand]
        public async Task AddUser()
        {
            UserDataResult fromPopup =
                await UserDtoPopupService.ShowAddUserFormAsync();

            switch(fromPopup.Status)
            {
                case FormResultStatus.Cancelled:
                    return;
                case FormResultStatus.Invalid:
                    await UserDtoPopupService.ShowAbortedOperationAsync(fromPopup.ErrorMessage);
                    return;
                default:
                    break;
            }

            UserDto newChef =
                new()
                {
                    FirstName = fromPopup.FirstNameUpdate,
                    LastName = fromPopup.LastNameUpdate,
                };

            await CrepePartyRepo.AddChefAsync(newChef);
            await CrepePartyRepo.CommitAsync();

            UpdateUserCommand.Execute(null);
        }

        [RelayCommand]
        public async Task UserUpdate()
        {
            if (SelectedUser is not UserDto usr)
            {
                return;
            }

            try
            {
                UserDataResult formResults =
                    await UserDtoPopupService.ShowUpdateUserFormAsync(usr);


                switch(formResults.Status)
                {
                    case FormResultStatus.Cancelled:
                        return;

                    case FormResultStatus.Invalid:
                        await UserDtoPopupService.ShowAbortedOperationAsync(formResults.ErrorMessage);
                        return;

                    default:
                        break;
                }

                if (formResults.FirstNameUpdate == usr.FirstName
                    && formResults.LastNameUpdate == usr.LastName)
                {
                    return;
                }

                usr.FirstName = formResults.FirstNameUpdate;
                usr.LastName = formResults.LastNameUpdate;
                await CrepePartyRepo.UpdateChefAsync(usr);
                await CrepePartyRepo.CommitAsync();

                await UserDtoPopupService.ShowUpdateSuccessedOperationAsync();

                UpdateUserCommand.Execute(null);
            }
            finally
            {
                // Unselect the user
                SelectedUser = null;
            }
        }
    }
}
