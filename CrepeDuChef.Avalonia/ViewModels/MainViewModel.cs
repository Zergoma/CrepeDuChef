using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrepeDuChef.ViewModels.ViewModels;
using System.Threading.Tasks;
using CrepeDuChef.Localization.Resources;

namespace CrepeDuChef.Avalonia.ViewModels
{
    /// <summary>
    /// Shell of the main window: holds the current page view model; MainWindow's data templates pick the view.
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {
        private readonly CrepeSessionsViewModel _crepeSessions;
        private readonly ChefSettingsViewModel _chefSettings;

        public string AppTitle => UiResources.CrepeDuChef_title;
        public string ChefsMsg => UiResources.Chefs;
        public string CrepePartyMsg => UiResources.CrepeDuChef_title;

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
