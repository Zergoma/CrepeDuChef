using CrepeDuChef.Avalonia.DI;
using CrepeDuChef.Avalonia.Dialogs.Factories;
using CrepeDuChef.ViewModels.Presenters;
using System.Threading.Tasks;

namespace CrepeDuChef.Avalonia.Dialogs.Presenters
{
    public class AvaloniaDialogPresenter : IDialogPresenter
    {
        private readonly IAvaloniaMessageDialogFactory _factory;
        private readonly MainWindowHolder _holder;

        public AvaloniaDialogPresenter(MainWindowHolder holder, IAvaloniaMessageDialogFactory factory)
        {
            _holder = holder;
            _factory = factory;
        }

        public Task ShowSuccessAsync(string title, string message)
            => _factory.CreateSuccess(title, message).ShowAsync(_holder.RequiredWindow);

        public Task ShowErrorAsync(string title, string message)
            => _factory.CreateError(title, message).ShowAsync(_holder.RequiredWindow);

        public Task ShowWarningAsync(string title, string message)
            => _factory.CreateWarning(title, message).ShowAsync(_holder.RequiredWindow);
    }
}
