using CommunityToolkit.Mvvm.ComponentModel;
using CrepeDuChef.Maui.Mappers;
using CrepeDuChef.Maui.Models.UI;
using System.Collections.ObjectModel;
using System.ComponentModel;

using SharedVM = CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Maui.MVVM.ViewModels
{
    public partial class CrepeSessionsViewModel_MauiAdapter : ObservableObject
    {
        public SharedVM.CrepeSessionsViewModel Shared { get; }

        [ObservableProperty]
        public partial ObservableCollection<CrepePartyGroup> CrepeSessions { get; set; } = [];

        public CrepeSessionsViewModel_MauiAdapter(SharedVM.CrepeSessionsViewModel shared)
        {
            Shared = shared;

            // On écoute les changements du VM partagé
            Shared.PropertyChanged += Shared_PropertyChanged;

            // Initialisation
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
            List<CrepePartyGroup> groups =
                CrepePartySessionToGroupMapper
                .MapToGroups(Shared.Sessions)
                .OrderByDescending(g => g.SessionNumber)
                .ToList();

            CrepeSessions = new ObservableCollection<CrepePartyGroup>(groups);
        }
    }
}
