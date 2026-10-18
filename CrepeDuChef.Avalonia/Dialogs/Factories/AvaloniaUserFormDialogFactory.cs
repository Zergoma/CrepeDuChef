using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Avalonia.Dialogs.Windows;
using CrepeDuChef.ViewModels.Models;
using CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Avalonia.Dialogs.Factories
{
    public class AvaloniaUserFormDialogFactory : IAvaloniaUserFormDialogFactory
    {
        public IDialog<ChefFormData> CreateAddForm()
            => new UserEditorWindow(ChefEditorViewModel.ForAdd());

        public IDialog<ChefFormData> CreateEditForm(ChefDto chef)
            => new UserEditorWindow(ChefEditorViewModel.ForEdit(chef));
    }
}
