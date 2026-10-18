using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Avalonia.Dialogs.Windows;
using CrepeDuChef.ViewModels.ViewModels;
using System.Collections.Generic;

namespace CrepeDuChef.Avalonia.Dialogs.Factories
{
    public class AvaloniaUserSelectionDialogFactory : IAvaloniaUserSelectionDialogFactory
    {
        public IDialog<IReadOnlyList<ChefDto>> Create(
            string title,
            string message,
            IReadOnlyList<ChefDto> allChefs,
            IReadOnlyList<ChefDto> selectedChefs)
        {
            return new SelectUsersDialog(
                new SelectChefsDialogViewModel(title, message, allChefs, selectedChefs));
        }
    }
}
