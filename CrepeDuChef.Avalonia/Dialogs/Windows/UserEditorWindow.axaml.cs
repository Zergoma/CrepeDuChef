using Avalonia.Controls;
using CrepeDuChef.ViewModels.Models;
using CrepeDuChef.ViewModels.ViewModels;
using System.Threading.Tasks;

namespace CrepeDuChef.Avalonia.Dialogs.Windows;

public partial class UserEditorWindow : Window, IDialog<ChefFormData>
{
    // Required by the XAML loader and the designer
    public UserEditorWindow()
    {
        InitializeComponent();
    }

    public UserEditorWindow(ChefEditorViewModel vm) : this()
    {
        DataContext = vm;
        vm.RequestClose += result => Close(result);
    }

    public Task<ChefFormData?> ShowAsync(Window owner)
        => ShowDialog<ChefFormData?>(owner);
}
