using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.ValueObjects;
using CrepeDuChef.Avalonia.Dialogs.Windows;

namespace CrepeDuChef.Avalonia.Dialogs.Factories
{
    public class AvaloniaUserFormDialogFactory : IAvaloniaUserFormDialogFactory
    {
        public IDialog<UserFormData> CreateAddForm()
            => UserEditorWindow.CreateForAdd();

        public IDialog<UserFormData> CreateEditForm(UserDto user)
            => UserEditorWindow.CreateForEdit(user);
    }
}
