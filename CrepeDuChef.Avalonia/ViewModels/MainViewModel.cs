using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrepeDuChef.Avalonia.Services;
using CrepeDuChef.Avalonia.Views;
using CrepeDuChef.Localization.Resources.languages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CrepeDuChef.Avalonia.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IServiceProvider _provider;

        private readonly INavigationService _nav;

        public string UsersMsg => Traduction.Users;
        public string CrepePartyMsg => Traduction.CrepeDuChef_title;



        [ObservableProperty]
        public partial Control? CurrentView { get; set; }


        public MainViewModel(IServiceProvider provider, INavigationService nav)
        {
            _provider = provider;
            _nav = nav;

            // Subscribe to navigation notifications
            _nav.OnNavigate += OnNavigate;

            // Set the initial view
            _nav.NavigateTo<CrepeSessionsView>();
        }

        public void OnNavigate(Type viewType)
        {
            CurrentView = (Control)_provider.GetRequiredService(viewType);
        }

        [RelayCommand]
        public async Task GoToUser()
        {
            _nav.NavigateTo<UserSettingsView>();
        }

        [RelayCommand]
        public async Task GoToCrepeParty()
        {
            _nav.NavigateTo<CrepeSessionsView>();
        }

    }
}
