using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.ValueObjects;
using System.Collections.ObjectModel;
using CrepeDuChef.Maui.UI.Popups.Presenters;
using CrepeDuChef.Application.Extensions;


namespace CrepeDuChef.Maui.MVVM.ViewModels
{
    public partial class UserSettingViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial ObservableCollection<UserDto> Users { get; set; } = new();
        [ObservableProperty]
        public partial object? SelectedUser { get; set; } = null;

        private IChefManagementService ChefManagementService { get; }
        private IUserApplicationOrchestrator UserApplicationOrchestrator { get; }
        public IUserPopupPresenter UserPopupPresenter { get; }

        public UserSettingViewModel(
            IChefManagementService chefManagementService,
            IUserApplicationOrchestrator userApplicationOrchestrator,
            IUserPopupPresenter userPopupPresenter)
        {
            ChefManagementService = chefManagementService;
            UserApplicationOrchestrator = userApplicationOrchestrator;
            UserPopupPresenter = userPopupPresenter;
        }

        [RelayCommand]
        private async Task UpdateUser()
        {
            Users = [.. await ChefManagementService.GetAllUsersAsync()];
        }

        [RelayCommand]
        public async Task AddUser()
        {
            UserFormData? formData =
                await UserPopupPresenter.ShowAddUserFormAsync();

            // User cancel operation
            if(formData is null)
            {
                return;
            }

            UserOperationResult result =
                await UserApplicationOrchestrator.AddUserAsync(formData);

            if (result.Status == OperationStatus.Failure)
            {
                await UserPopupPresenter.ShowAbortedOperationAsync(result.ErrorMessage ?? "");
                return;
            }

            UpdateUserCommand.Execute(null);
        }

        private async Task HandleSuccessAsync()
        {
            await UserPopupPresenter.ShowUpdateSuccessedOperationAsync();
            UpdateUserCommand.Execute(null);
        }

        private async Task HandleFailureAsync(string error)
        {
            await UserPopupPresenter.ShowAbortedOperationAsync(error);
        }


        [RelayCommand]
        public async Task UserUpdate()
        {
            if (SelectedUser is not UserDto usr)
            {
                return;
            }

            // UI --> remove selected the user in the CollectionView
            SelectedUser = null;

            UserFormData? formData =
                await UserPopupPresenter.ShowUpdateUserFormAsync(usr);

            if (formData is null)
            {
                return;
            }

            UserOperationResult result =
                await UserApplicationOrchestrator.UpdateUserAsync(usr, formData);

            await (result.Status switch
            {
                OperationStatus.Success => HandleSuccessAsync(),
                OperationStatus.Failure => HandleFailureAsync(result.ErrorMessage),
                OperationStatus.Canceled => Task.CompletedTask,
                _ => Task.CompletedTask
            });
        }
    }
}
