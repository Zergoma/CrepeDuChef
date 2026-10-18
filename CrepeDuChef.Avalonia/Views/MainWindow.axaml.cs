using Avalonia.Controls;
using CrepeDuChef.Avalonia.ViewModels;

namespace CrepeDuChef.Avalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel vm)
        {
            InitializeComponent();

            DataContext = vm;

            Opened += async (_, _) => await vm.GoToCrepePartyCommand.ExecuteAsync(null);
        }
    }
}
