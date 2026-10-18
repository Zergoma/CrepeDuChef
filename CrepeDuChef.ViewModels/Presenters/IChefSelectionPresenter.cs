using CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.ViewModels.Presenters
{
    public interface IChefSelectionPresenter
    {
        /// <returns>The selected chefs, or null when the user cancels.</returns>
        Task<IReadOnlyList<ChefDto>?> SelectChefsAsync(
            string title,
            string message,
            IReadOnlyList<ChefDto> allChefs,
            IReadOnlyList<ChefDto> selectedChefs);
    }
}
