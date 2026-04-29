using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Avalonia.Dialogs.Windows;
using System.Collections.Generic;

namespace CrepeDuChef.Avalonia.Dialogs.Factories
{
    public class AvaloniaUserSelectionDialogFactory : IAvaloniaUserSelectionDialogFactory
    {
        public IDialog<UserDto[]> Create(string title, string message, List<UserDto> allUsers, List<UserDto> selectedUsers)
        {
            return SelectUsersDialog.Create(
                title,
                message,
                allUsers,
                selectedUsers);
        }
    }
}
