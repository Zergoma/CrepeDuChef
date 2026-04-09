using Avalonia.Controls;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.ValueObjects;
using System.Threading.Tasks;

using externalVM = CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Avalonia.Dialogs.Windows;

public partial class UserEditorWindow : Window, IDialog<UserFormData>
{
    private UserEditorWindow()
    {
        InitializeComponent();
    }

    // "Add" Factory 
    public static IDialog<UserFormData> CreateForAdd()
    {
        UserEditorWindow win = new();
        externalVM.UserEditorViewModel vm = new();
        win.DataContext = vm;

        // Close the dialog
        vm.RequestClose += result => win.Close(result);

        return win;
    }

    // "Edit" Factory
    public static IDialog<UserFormData> CreateForEdit(UserDto existing)
    {
        UserEditorWindow win = new();
        externalVM.UserEditorViewModel vm = new (
            existing.FirstName,
            existing.LastName);
        win.DataContext = vm;

        // Close the dialog
        vm.RequestClose += result => win.Close(result);

        return win;
    }

    public Task<UserFormData?> ShowAsync(Window owner)
        => this.ShowDialog<UserFormData?>(owner);
}