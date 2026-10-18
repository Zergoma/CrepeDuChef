using Avalonia.Controls;
using CrepeDuChef.Avalonia.ViewModels;

namespace CrepeDuChef.Avalonia;

public partial class CrepeSessionsView : UserControl
{
    // Avalonia ctor required to eleminate "Avalonia warning AVLN3001"
    public CrepeSessionsView()
    {
        InitializeComponent();
    }
    public CrepeSessionsView(CrepeSessionsViewModelAvaloniaAdapter vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}