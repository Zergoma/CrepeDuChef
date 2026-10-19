using Avalonia.Controls;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrepeDuChef.Avalonia.Dialogs.Windows;

public partial class SelectChefsDialog : Window, IDialog<IReadOnlyList<ChefDto>>
{
    // Required by the XAML loader and the designer
    public SelectChefsDialog()
    {
        InitializeComponent();
    }

    public SelectChefsDialog(SelectChefsDialogViewModel vm) : this()
    {
        DataContext = vm;
        vm.RequestClose += result => Close(result);
    }

    public Task<IReadOnlyList<ChefDto>?> ShowAsync(Window owner)
        => ShowDialog<IReadOnlyList<ChefDto>?>(owner);
}
