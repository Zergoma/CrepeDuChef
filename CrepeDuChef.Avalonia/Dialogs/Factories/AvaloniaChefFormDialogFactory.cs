using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Avalonia.Dialogs.Windows;
using CrepeDuChef.ViewModels.Models;
using CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Avalonia.Dialogs.Factories
{
    public class AvaloniaChefFormDialogFactory : IAvaloniaChefFormDialogFactory
    {
        public IDialog<ChefFormData> CreateAddForm()
            => new ChefEditorWindow(ChefEditorViewModel.ForAdd());

        public IDialog<ChefFormData> CreateEditForm(ChefDto chef)
            => new ChefEditorWindow(ChefEditorViewModel.ForEdit(chef));
    }
}
