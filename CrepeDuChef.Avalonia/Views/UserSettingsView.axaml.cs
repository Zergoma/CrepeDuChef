using Avalonia.Controls;
using SharedVm = CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Avalonia.Views;

public partial class UserSettingsView : UserControl
{
    // Avalonia ctor required to eleminate "Avalonia warning AVLN3001"
    public UserSettingsView()
    {
        InitializeComponent();
    }

    public UserSettingsView(SharedVm.UserSettingViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;

        this.AttachedToVisualTree += async (_, __) =>
        {
            if (DataContext is SharedVm.UserSettingViewModel vm)
            {
                vm.UpdateUserCommand.Execute(null);
            }
        };
    }
}