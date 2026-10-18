using CrepeDuChef.Maui.MVVM.ViewModels;

namespace CrepeDuChef.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
