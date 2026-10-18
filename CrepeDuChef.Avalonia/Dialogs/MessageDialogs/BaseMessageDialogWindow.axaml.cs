using Avalonia.Controls;
using CrepeDuChef.Avalonia.Dialogs.ViewModels;
using System;
using System.Threading.Tasks;

namespace CrepeDuChef.Avalonia.Dialogs.Windows;

public partial class BaseMessageDialogWindow : Window, IDialog
{
    // Avalonia ctor required to eleminate "Avalonia warning AVLN3001"
    public BaseMessageDialogWindow()
    {
        InitializeComponent();
    }

    public BaseMessageDialogWindow(BaseMessageDialogViewModel vm)
    {
        InitializeComponent();

        DataContext = vm;

        vm.RequestClose += () => Close();
    }

    public Task ShowAsync(Window owner)
        => this.ShowDialog(owner);
}