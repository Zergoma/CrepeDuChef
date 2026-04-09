using Avalonia.Controls;
using CrepeDuChef.Avalonia.DI;
using CrepeDuChef.Avalonia.Dialogs.Factories;
using CrepeDuChef.Avalonia.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppDto = CrepeDuChef.Application.DTOs;
using SharedPresenters = CrepeDuChef.ViewModels.Presenters;

namespace CrepeDuChef.Avalonia.Dialogs.Presenters
{
    public class AvaloniaUserSelectionPresenter : SharedPresenters.IUserSelectionPresenter
    {
        private readonly IAvaloniaUserSelectionDialogFactory _factory;
        private readonly IDialogService _dialogService;
        private Window Owner => _holder.Window!;
        private readonly MainWindowHolder _holder;

        public AvaloniaUserSelectionPresenter(
            IDialogService dialog,
            MainWindowHolder holder,
            IAvaloniaUserSelectionDialogFactory factory)
        {
            _dialogService = dialog;
            _holder = holder;
            _factory = factory;
        }

        public Task<AppDto.UserDto[]?> SelectUsersAsync(
            string title,
            string message,
            List<AppDto.UserDto> allUsers,
            List<AppDto.UserDto> selectedUsers)
        {
            IDialog<AppDto.UserDto[]> dialog =
                _factory.Create(
                    title,
                    message,
                    allUsers,
                    selectedUsers);

            return _dialogService.ShowDialogAsync(dialog, Owner);
        }
    }
}
