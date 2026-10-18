using Avalonia.Controls;
using CrepeDuChef.ViewModels.Models;
using CrepeDuChef.ViewModels.ViewModels;
using System.Threading.Tasks;

namespace CrepeDuChef.Avalonia.Dialogs.Windows;

public partial class ChefEditorWindow : Window, IDialog<ChefFormData>
{
    // Required by the XAML loader and the designer
    public ChefEditorWindow()
    {
        InitializeComponent();
    }

    public ChefEditorWindow(ChefEditorViewModel vm) : this()
    {
        DataContext = vm;
        vm.RequestClose += result => Close(result);
    }

    public Task<ChefFormData?> ShowAsync(Window owner)
        => ShowDialog<ChefFormData?>(owner);
}
