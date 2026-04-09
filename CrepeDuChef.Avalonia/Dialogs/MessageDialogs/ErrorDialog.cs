using CrepeDuChef.Avalonia.Dialogs.ViewModels;
using CrepeDuChef.Avalonia.Dialogs.Windows;

namespace CrepeDuChef.Avalonia.Dialogs.MessageDialogs;

public static class ErrorDialog
{
    public static IDialog Create(string title, string message)
    {
        var vm = new BaseMessageDialogViewModel(
            title,
            message,
            icon: "❌",
            backgroundColor: "#FDE7E9",
            foregroundColor: "#A80000"
        );

        return new BaseMessageDialogWindow(vm);
    }
}
