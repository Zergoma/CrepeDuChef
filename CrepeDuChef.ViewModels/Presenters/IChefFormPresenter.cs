using CrepeDuChef.Application.DTOs;
using CrepeDuChef.ViewModels.Models;

namespace CrepeDuChef.ViewModels.Presenters
{
    public interface IChefFormPresenter
    {
        /// <returns>The entered names, or null when the user cancels.</returns>
        Task<ChefFormData?> ShowAddChefFormAsync();

        /// <returns>The entered names, or null when the user cancels.</returns>
        Task<ChefFormData?> ShowUpdateChefFormAsync(ChefDto chef);
    }
}
