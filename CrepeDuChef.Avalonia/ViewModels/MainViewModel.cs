using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrepeDuChef.ViewModels.ViewModels;
using System.Threading.Tasks;
using Loca = CrepeDuChef.Localization.Resources.languages;

namespace CrepeDuChef.Avalonia.ViewModels
{
    /// <summary>
    /// Shell of the main window: holds the current page view model; MainWindow's data templates pick the view.
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {
        private readonly CrepeSessionsViewModel _crepeSessions;
        private readonly ChefSettingsViewModel _chefSettings;

        public string UsersMsg => Loca.Traduction.Users;
        public string CrepePartyMsg => Loca.Traduction.CrepeDuChef_title;

        [ObservableProperty]
        public partial object? CurrentPage { get; private set; }

        public MainViewModel(CrepeSessionsViewModel crepeSessions, ChefSettingsViewModel chefSettings)
        {
            _crepeSessions = crepeSessions;
            _chefSettings = chefSettings;
        }

        [RelayCommand]
        public async Task GoToCrepeParty()
        {
            CurrentPage = _crepeSessions;
            await _crepeSessions.OnAppearingAsync();
        }

        [RelayCommand]
        public async Task GoToChefs()
        {
            CurrentPage = _chefSettings;
            await _chefSettings.LoadChefsCommand.ExecuteAsync(null);
        }
    }
}
