using CommunityToolkit.Maui.Extensions;
using CrepeDuChef.Maui.UI.Popups.Core;
using CrepeDuChef.Maui.UI.Popups.Factories;
using CrepeDuChef.Maui.UI.Popups.Models;
using CrepeDuChef.Maui.UI.Popups.Views;
using SharedPresenters = CrepeDuChef.ViewModels.Presenters;

namespace CrepeDuChef.Maui.UI.Popups.Presenters
{
    public class MauiDialogPresenter : SharedPresenters.IDialogPresenter
    {
        private readonly IMessagePopupFactory _factory;
        private readonly PopupCoordinator _coordinator;
        private readonly PopupOptionsFactory _options;

        public MauiDialogPresenter(
            PopupCoordinator coordinator,
            PopupOptionsFactory options,
            IMessagePopupFactory factory)
        {
            _coordinator = coordinator;
            _options = options;
            _factory = factory;
        }

        public Task ShowSuccessAsync(string title, string message)
            => ShowMessageAsync(title, message, MessageKind.Success);

        public Task ShowErrorAsync(string title, string message)
            => ShowMessageAsync(title, message, MessageKind.Error);

        public Task ShowWarningAsync(string title, string message)
            => ShowMessageAsync(title, message, MessageKind.Warning);

        private Task ShowMessageAsync(string title, string message, MessageKind kind)
        {
            return _coordinator.RunAsync(async () =>
            {
                MessagePopup popup =
                    _factory.Create(title, message, kind);

                await Shell.Current.ShowPopupAsync(popup, _options.Create());
            });
        }
    }
}
