using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Avalonia.DI;
using CrepeDuChef.Avalonia.Dialogs.Factories;
using CrepeDuChef.ViewModels.Presenters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrepeDuChef.Avalonia.Dialogs.Presenters
{
    public class AvaloniaChefSelectionPresenter : IChefSelectionPresenter
    {
        private readonly IAvaloniaChefSelectionDialogFactory _factory;
        private readonly MainWindowHolder _holder;

        public AvaloniaChefSelectionPresenter(MainWindowHolder holder, IAvaloniaChefSelectionDialogFactory factory)
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
