namespace CrepeDuChef.Avalonia.Dialogs.Factories
{
    public interface IAvaloniaMessageDialogFactory
    {
        IDialog CreateSuccess(string title, string message);
        IDialog CreateError(string title, string message);
        IDialog CreateWarning(string title, string message);
    }
}
