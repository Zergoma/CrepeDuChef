using CrepeDuChef.Application.DTOs;
using System.Collections.Generic;

namespace CrepeDuChef.Avalonia.Dialogs.Factories
{
    public interface IAvaloniaUserSelectionDialogFactory
    {
        IDialog<UserDto[]> Create(
        string title,
        string message,
        List<UserDto> allUsers,
        List<UserDto> selectedUsers);
    }
}
