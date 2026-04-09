using Avalonia.Controls;
using CrepeDuChef.Avalonia.DI;
using CrepeDuChef.Avalonia.Dialogs.Factories;
using CrepeDuChef.Avalonia.Services;
using System.Threading.Tasks;
using SharedPresenters = CrepeDuChef.ViewModels.Presenters;

namespace CrepeDuChef.Avalonia.Dialogs.Presenters
{
    public class AvaloniaDialogPresenter : SharedPresenters.IDialogPresenter
    {
        private readonly IAvaloniaMessageDialogFactory _factory;
        private readonly IDialogService _dialogService;
        private Window Owner => _holder.Window!;
        private readonly MainWindowHolder _holder;

        public AvaloniaDialogPresenter(
            IDialogService dialog,
            MainWindowHolder holder,
            IAvaloniaMessageDialogFactory factory)
        {
            _dialogService = dialog;
            _holder = holder;
            _factory = factory;
        }

        public Task ShowSuccessAsync(string title, string message)
        {
            IDialog dialog =
                _factory.CreateSuccess(title, message);

            return _dialogService.ShowMessageAsync(dialog, Owner);
        }

        public Task ShowErrorAsync(string title, string message)
        {
            IDialog dialog =
                _factory.CreateError(title, message);

            return _dialogService.ShowMessageAsync(dialog, Owner);
        }

        public Task ShowWarningAsync(string title, string message)
        {
            IDialog dialog =
                _factory.CreateWarning(title, message);

            return _dialogService.ShowMessageAsync(dialog, Owner);
        }
    }
}
