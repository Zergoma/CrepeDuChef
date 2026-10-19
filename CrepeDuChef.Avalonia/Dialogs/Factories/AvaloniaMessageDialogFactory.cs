using CrepeDuChef.Avalonia.Dialogs.MessageDialogs;

namespace CrepeDuChef.Avalonia.Dialogs.Factories
{
    public class AvaloniaMessageDialogFactory : IAvaloniaMessageDialogFactory
    {
        public IDialog CreateSuccess(string title, string message)
            => Create(title, message, MessageKind.Success);

        public IDialog CreateError(string title, string message)
            => Create(title, message, MessageKind.Error);

        public IDialog CreateWarning(string title, string message)
            => Create(title, message, MessageKind.Warning);

        private static MessageDialogWindow Create(string title, string message, MessageKind kind)
            => new(new MessageDialogViewModel(title, message, kind));
    }
}
