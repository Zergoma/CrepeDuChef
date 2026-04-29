using Avalonia.Controls;
using CrepeDuChef.Avalonia.DI;
using CrepeDuChef.Avalonia.Dialogs.Factories;
using CrepeDuChef.Avalonia.Services;
using System.Threading.Tasks;
using AppDto = CrepeDuChef.Application.DTOs;
using AppValueObj = CrepeDuChef.Application.ValueObjects;
using SharedPresenters = CrepeDuChef.ViewModels.Presenters;

namespace CrepeDuChef.Avalonia.Dialogs.Presenters
{
    public class AvaloniaUserFormPresenter : SharedPresenters.IUserFormPresenter
    {
        private readonly IAvaloniaUserFormDialogFactory _factory;
        private readonly IDialogService _dialogService;

        private Window Owner => _holder.Window!;
        private readonly MainWindowHolder _holder;

        public AvaloniaUserFormPresenter(
            IDialogService dialog,
            MainWindowHolder holder,
            IAvaloniaUserFormDialogFactory factory)
        {
            _dialogService = dialog;
            _holder = holder;
            _factory = factory;
        }


        public Task<AppValueObj.UserFormData?> ShowAddUserFormAsync()
        {
            IDialog<AppValueObj.UserFormData> dialog =
                _factory.CreateAddForm();

            Task<AppValueObj.UserFormData?> resu =
                _dialogService.ShowDialogAsync<AppValueObj.UserFormData>(dialog, Owner);

            return resu;
        }

        public Task<AppValueObj.UserFormData?> ShowUpdateUserFormAsync(AppDto.UserDto user)
        {
            IDialog<AppValueObj.UserFormData> dialog =
                _factory.CreateEditForm(user);

            Task<AppValueObj.UserFormData?> resu =
                _dialogService.ShowDialogAsync<AppValueObj.UserFormData>(dialog, Owner);

            return resu;
        }
    }
}
