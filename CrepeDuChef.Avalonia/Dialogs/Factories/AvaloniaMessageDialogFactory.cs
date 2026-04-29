using CrepeDuChef.Avalonia.Dialogs.MessageDialogs;

namespace CrepeDuChef.Avalonia.Dialogs.Factories
{
    public class AvaloniaMessageDialogFactory : IAvaloniaMessageDialogFactory
    {
        public IDialog CreateSuccess(string title, string message)
            => MessageDialog.Create(title, message);

        public IDialog CreateError(string title, string message)
            => ErrorDialog.Create(title, message);

        public IDialog CreateWarning(string title, string message)
            => WarningDialog.Create(title, message);
    }
}
