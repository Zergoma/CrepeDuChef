using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Avalonia.DI;
using CrepeDuChef.Avalonia.Dialogs.Factories;
using CrepeDuChef.ViewModels.Models;
using CrepeDuChef.ViewModels.Presenters;
using System.Threading.Tasks;

namespace CrepeDuChef.Avalonia.Dialogs.Presenters
{
    public class AvaloniaChefFormPresenter : IChefFormPresenter
    {
        private readonly IAvaloniaChefFormDialogFactory _factory;
        private readonly MainWindowHolder _holder;

        public AvaloniaChefFormPresenter(MainWindowHolder holder, IAvaloniaChefFormDialogFactory factory)
        {
            _holder = holder;
            _factory = factory;
        }

        public Task<ChefFormData?> ShowAddChefFormAsync()
            => _factory.CreateAddForm().ShowAsync(_holder.RequiredWindow);

        public Task<ChefFormData?> ShowUpdateChefFormAsync(ChefDto chef)
            => _factory.CreateEditForm(chef).ShowAsync(_holder.RequiredWindow);
    }
}
