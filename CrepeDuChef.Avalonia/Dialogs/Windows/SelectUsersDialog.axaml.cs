using Avalonia.Controls;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Avalonia.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharedVm = CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Avalonia.Dialogs.Windows;

public partial class SelectUsersDialog : Window, IDialog<UserDto[]>
{
    private SelectUsersDialog(
        string title,
        string message,
        IEnumerable<UserDto> allUsers,
        IEnumerable<UserDto> selectedUsers)
    {
        InitializeComponent();
        SharedVm.SelectUsersDialogViewModel sharedVm = 
            new (
                title,
                message,
                allUsers: allUsers,
                selectedUsers: selectedUsers);

        var adapter = new SelectUsersDialogViewModelAvaloniaAdapter(sharedVm);
        DataContext = adapter;

        // Close dialog
        adapter.RequestClose += result => Close(result);
    }

    public static IDialog<UserDto[]> Create(
        string title,
        string message,
        IEnumerable<UserDto> allUsers,
        IEnumerable<UserDto> selectedUsers)
    {
        return new SelectUsersDialog(
            title: title,
            message: message,
            allUsers: allUsers,
            selectedUsers: selectedUsers);
    }

    public Task<UserDto[]?> ShowAsync(Window owner)
        => this.ShowDialog<UserDto[]?>(owner);

}