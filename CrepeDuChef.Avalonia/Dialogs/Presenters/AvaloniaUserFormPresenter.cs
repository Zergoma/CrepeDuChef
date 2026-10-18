using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Avalonia.DI;
using CrepeDuChef.Avalonia.Dialogs.Factories;
using CrepeDuChef.ViewModels.Models;
using CrepeDuChef.ViewModels.Presenters;
using System.Threading.Tasks;

namespace CrepeDuChef.Avalonia.Dialogs.Presenters
{
    public class AvaloniaUserFormPresenter : IChefFormPresenter
    {
        private readonly IAvaloniaUserFormDialogFactory _factory;
        private readonly MainWindowHolder _holder;

        public AvaloniaUserFormPresenter(MainWindowHolder holder, IAvaloniaUserFormDialogFactory factory)
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
