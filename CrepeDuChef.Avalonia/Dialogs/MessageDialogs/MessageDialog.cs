using CrepeDuChef.Avalonia.Dialogs.ViewModels;
using CrepeDuChef.Avalonia.Dialogs.Windows;

namespace CrepeDuChef.Avalonia.Dialogs.MessageDialogs;

public static partial class MessageDialog
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