using CrepeDuChef.Application.DTOs;
using CrepeDuChef.ViewModels.Models;

namespace CrepeDuChef.Avalonia.Dialogs.Factories
{
    public interface IAvaloniaChefFormDialogFactory
    {
        IDialog<ChefFormData> CreateAddForm();
        IDialog<ChefFormData> CreateEditForm(ChefDto chef);
    }
}
