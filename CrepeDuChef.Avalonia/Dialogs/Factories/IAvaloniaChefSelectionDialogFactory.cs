using CrepeDuChef.Application.DTOs;
using System.Collections.Generic;

namespace CrepeDuChef.Avalonia.Dialogs.Factories
{
    public interface IAvaloniaChefSelectionDialogFactory
    {
        IDialog<IReadOnlyList<ChefDto>> Create(
            string title,
            string message,
            IReadOnlyList<ChefDto> allChefs,
            IReadOnlyList<ChefDto> selectedChefs);
    }
}
