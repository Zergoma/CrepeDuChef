using CommunityToolkit.Mvvm.ComponentModel;
using CrepeDuChef.Avalonia.Mappers;
using CrepeDuChef.Avalonia.Models.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SharedVm = CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Avalonia.ViewModels
{
    public partial class CrepeSessionsViewModelAvaloniaAdapter : ObservableObject
    {
        public SharedVm.CrepeSessionsViewModel Shared { get; }

        [ObservableProperty]
        public partial ObservableCollection<CrepePartyGroup_Ava> CrepeSessions { get; set; } = [];

        
        public CrepeSessionsViewModelAvaloniaAdapter(SharedVm.CrepeSessionsViewModel shared)
        {
            Shared = shared;

            _ = InitializeAsync();

            // On écoute les changements du VM partagé
            Shared.PropertyChanged += Shared_PropertyChanged;

            // Initialisation
            UpdateCrepeSessions();
        }
        private async Task InitializeAsync()
        {
            await Shared.OnAppearingAsync();

            UpdateCrepeSessions();
        }

        private void Shared_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Shared.Sessions))
            {
                UpdateCrepeSessions();
            }
        }

        private void UpdateCrepeSessions()
        {
            // Mapper to MAUI UI Type
            List<CrepePartyGroup_Ava> groups =
                CrepePartySessionToGroupMapper_Ava
                .MapToGroups(Shared.Sessions)
                .OrderByDescending(g => g.SessionNumber)
                .ToList();

            CrepeSessions = new ObservableCollection<CrepePartyGroup_Ava>(groups);
        }
    }
}
