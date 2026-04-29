using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.ValueObjects;

namespace CrepeDuChef.Avalonia.Dialogs.Factories
{
    public interface IAvaloniaUserFormDialogFactory
    {
        IDialog<UserFormData> CreateAddForm();
        IDialog<UserFormData> CreateEditForm(UserDto user);
    }
}
