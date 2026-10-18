
namespace CrepeDuChef.Avalonia.Dialogs.MessageDialogs;

public static class MessageDialog
{
    public static IDialog Create(string title, string message)
    {
        var vm = new BaseMessageDialogViewModel(
            title,
            message,
            icon: "ℹ️",
            backgroundColor: "#FFFFFF",
            foregroundColor: "#000000"
        );

        return new BaseMessageDialogWindow(vm);
    }
}