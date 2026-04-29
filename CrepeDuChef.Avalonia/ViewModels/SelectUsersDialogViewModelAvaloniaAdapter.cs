using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AppDto = CrepeDuChef.Application.DTOs;
using CrepeDuChef.Avalonia.Models.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SharedVm = CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Avalonia.ViewModels
{
    public partial class SelectUsersDialogViewModelAvaloniaAdapter : ObservableObject
    {
        public SharedVm.SelectUsersDialogViewModel Shared { get; }
        public event Action<AppDto.UserDto[]?>? RequestClose;

        [ObservableProperty]
        public partial ObservableCollection<SelectableUser> Users { get; set; } = [];
        public SelectUsersDialogViewModelAvaloniaAdapter(SharedVm.SelectUsersDialogViewModel sharedVm)
        {
            Shared = sharedVm;

            var selected =
                new HashSet<AppDto.UserDto>(Shared.SelectedUsers);
            
            Users =
                new ObservableCollection<SelectableUser>(
                    Shared.AllUsers
                        .Select(u => new SelectableUser(u, selected.Contains(u))));

            Shared.RequestClose += result => RequestClose?.Invoke(result);
        }

        [RelayCommand]
        private void Cancel()
        => Shared.CancelCommand.Execute(null);

        [RelayCommand]
        private void Validate()
        {
            var selectedUsers = Users
                .Where(u => u.IsSelected)
                .Select(u => u.User)
                .ToArray();

            RequestClose?.Invoke(selectedUsers);
        }
    }
}
