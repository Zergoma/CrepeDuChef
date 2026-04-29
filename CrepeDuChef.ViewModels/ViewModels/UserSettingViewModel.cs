using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Loca = CrepeDuChef.Localization.Resources.languages;
using System.Collections.ObjectModel;
using AppContracts = CrepeDuChef.Application.Interfaces;
using AppDto = CrepeDuChef.Application.DTOs;
using AppValueObjs = CrepeDuChef.Application.ValueObjects;
using VmPresenters = CrepeDuChef.ViewModels.Presenters;


namespace CrepeDuChef.ViewModels.ViewModels
{
    /// <summary>
    /// Represents the view model for managing user settings, including user selection, addition, and update operations
    /// within the application.
    /// </summary>
    /// <remarks>This view model coordinates user-related actions by interacting with user management services
    /// and presenters. It exposes commands for adding and updating users, and maintains the current list of users and
    /// the selected user for editing. This class is typically used in MVVM scenarios to bind user management
    /// functionality to the UI.</remarks>
    public partial class UserSettingViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial ObservableCollection<AppDto.UserDto> Users { get; set; } = new();
        [ObservableProperty]
        public partial object? SelectedUser { get; set; } = null;

        private AppContracts.IChefManagementService ChefManagementService { get; }
        private AppContracts.IUserApplicationOrchestrator UserApplicationOrchestrator { get; }
        public VmPresenters.IUserFormPresenter UserFormPresenter { get; }
        public VmPresenters.IDialogPresenter UserDialPresenter { get; }


        public string UserSettingMsg => Loca.Traduction.UserSetting;

        public UserSettingViewModel(
            AppContracts.IChefManagementService chefManagementService,
            AppContracts.IUserApplicationOrchestrator userApplicationOrchestrator,
            VmPresenters.IUserFormPresenter userPopupPresenter,
            VmPresenters.IDialogPresenter userDialPresenter)
        {
            ChefManagementService = chefManagementService;
            UserApplicationOrchestrator = userApplicationOrchestrator;
            UserFormPresenter = userPopupPresenter;
            UserDialPresenter = userDialPresenter;
        }

        [RelayCommand]
        private async Task UpdateUser()
        {
            Users = [.. await ChefManagementService.GetAllUsersAsync()];
        }

        [RelayCommand]
        public async Task AddUser()
        {
            AppValueObjs.UserFormData? formData =
                await UserFormPresenter.ShowAddUserFormAsync();

            // User cancel operation
            if (formData is null)
            {
                return;
            }

            AppValueObjs.UserOperationResult result =
                await UserApplicationOrchestrator.AddUserAsync(formData);

            if (result.Status == AppValueObjs.OperationStatus.Failure)
            {
                await UserDialPresenter.ShowErrorAsync(
                    title: Loca.Traduction.OperationCanceled,
                    message: result.ErrorMessage ?? "");
                return;
            }

            await UpdateUserCommand.ExecuteAsync(null);
        }

        private async Task HandleSuccessAsync()
        {
            await UserDialPresenter.ShowSuccessAsync(
                title: Loca.Traduction.Updated,
                message: Loca.Traduction.InformationUpdated);
            await UpdateUserCommand.ExecuteAsync(null);
        }

        private async Task HandleFailureAsync(string error)
        {
            await UserDialPresenter.ShowErrorAsync(
                title: Loca.Traduction.OperationCanceled,
                message: error);
        }


        [RelayCommand]
        public async Task UserUpdate()
        {
            if (SelectedUser is not AppDto.UserDto usr)
            {
                return;
            }

            // UI --> remove selected from UI
            SelectedUser = null;

            AppValueObjs.UserFormData? formData =
                await UserFormPresenter.ShowUpdateUserFormAsync(usr);

            if (formData is null)
            {
                return;
            }

            AppValueObjs.UserOperationResult result =
                await UserApplicationOrchestrator.UpdateUserAsync(usr, formData);

            await (result.Status switch
            {
                AppValueObjs.OperationStatus.Success => HandleSuccessAsync(),
                AppValueObjs.OperationStatus.Failure => HandleFailureAsync(result.ErrorMessage),
                AppValueObjs.OperationStatus.Canceled => Task.CompletedTask,
                _ => Task.CompletedTask
            });
        }
    }
}
