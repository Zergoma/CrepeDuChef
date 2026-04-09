using CommunityToolkit.Mvvm.ComponentModel;
using AppDto = CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.Avalonia.Models.UI
{
    public partial class SelectableUser : ObservableObject
    {
        public AppDto.UserDto User { get; }

        [ObservableProperty]
        public partial bool IsSelected { get; set; }

        public SelectableUser(AppDto.UserDto user, bool isSelected)
        {
            User = user;
            this.IsSelected = isSelected;
        }
    }
}
