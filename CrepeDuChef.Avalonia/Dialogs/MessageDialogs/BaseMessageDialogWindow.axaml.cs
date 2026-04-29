using Avalonia.Controls;
using CrepeDuChef.Avalonia.Dialogs.ViewModels;
using System;
using System.Threading.Tasks;

namespace CrepeDuChef.Avalonia.Dialogs.Windows;

public partial class BaseMessageDialogWindow : Window, IDialog
{
    public BaseMessageDialogWindow(BaseMessageDialogViewModel vm)
    {
        InitializeComponent();

        DataContext = vm;

        vm.RequestClose += () => Close();
    }

    public Task ShowAsync(Window owner)
        => this.ShowDialog(owner);
}