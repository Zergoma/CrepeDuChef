
namespace CrepeDuChef.Avalonia.Dialogs.MessageDialogs;

public static class WarningDialog
{
    public static IDialog Create(string title, string message)
    {
        var vm = new BaseMessageDialogViewModel(
            title,
            message,
            icon: "⚠️",
            backgroundColor: "#FFF4CE",
            foregroundColor: "#663C00"
        );

        return new BaseMessageDialogWindow(vm);
    }
}
