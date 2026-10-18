using CommunityToolkit.Mvvm.ComponentModel;
using CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.ViewModels.Models
{
    public partial class SelectableChef : ObservableObject
    {
        public ChefDto Chef { get; }

        [ObservableProperty]
        public partial bool IsSelected { get; set; }

        public SelectableChef(ChefDto chef, bool isSelected)
        {
            Chef = chef;
            IsSelected = isSelected;
        }
    }
}
