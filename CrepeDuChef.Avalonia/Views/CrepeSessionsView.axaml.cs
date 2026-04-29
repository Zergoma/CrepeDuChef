using Avalonia.Controls;
using CrepeDuChef.Avalonia.ViewModels;

namespace CrepeDuChef.Avalonia;

public partial class CrepeSessionsView : UserControl
{
    public CrepeSessionsView(CrepeSessionsViewModelAvaloniaAdapter vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}