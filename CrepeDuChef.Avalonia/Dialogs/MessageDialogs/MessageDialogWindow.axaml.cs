using Avalonia.Controls;
using System.Threading.Tasks;

namespace CrepeDuChef.Avalonia.Dialogs.MessageDialogs;

public partial class MessageDialogWindow : Window, IDialog
{
    // Required by the XAML loader and the designer
    public MessageDialogWindow()
    {
        InitializeComponent();
    }

    public MessageDialogWindow(MessageDialogViewModel vm) : this()
    {
        DataContext = vm;

        // success / warning / error style classes pick the icon and colors
        Classes.Add(vm.Kind.ToString().ToLowerInvariant());

        vm.RequestClose += Close;
    }

    public Task ShowAsync(Window owner)
        => ShowDialog(owner);
}
