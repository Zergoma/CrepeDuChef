using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Avalonia.DI;
using CrepeDuChef.Avalonia.Dialogs.Factories;
using CrepeDuChef.ViewModels.Presenters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrepeDuChef.Avalonia.Dialogs.Presenters
{
    public class AvaloniaUserSelectionPresenter : IChefSelectionPresenter
    {
        private readonly IAvaloniaUserSelectionDialogFactory _factory;
        private readonly MainWindowHolder _holder;

        public AvaloniaUserSelectionPresenter(MainWindowHolder holder, IAvaloniaUserSelectionDialogFactory factory)
        {
            _holder = holder;
            _factory = factory;
        }

        public Task<IReadOnlyList<ChefDto>?> SelectChefsAsync(
            string title,
            string message,
            IReadOnlyList<ChefDto> allChefs,
            IReadOnlyList<ChefDto> selectedChefs)
        {
            return _factory
                .Create(title, message, allChefs, selectedChefs)
                .ShowAsync(_holder.RequiredWindow);
        }
    }
}
