using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AppDto = CrepeDuChef.Application.DTOs;
using Loca = CrepeDuChef.Localization.Resources.languages;
using System.Collections.ObjectModel;


namespace CrepeDuChef.ViewModels.ViewModels
{
    public partial class SelectUsersDialogViewModel : ObservableObject
    {
        public event Action<AppDto.UserDto[]?>? RequestClose;

        public ObservableCollection<AppDto.UserDto> AllUsers { get;  }
        public ObservableCollection<AppDto.UserDto> SelectedUsers { get; }

        public string TitleMsg { get; }
        public static string CloseMsg => Loca.Traduction.Cancel;
        public static string ValidateMsg => Loca.Traduction.Validate;

        public string Message { get; }
        public SelectUsersDialogViewModel(
            string title,
            string message,
            IEnumerable<AppDto.UserDto> allUsers,
            IEnumerable<AppDto.UserDto> selectedUsers)
        {
            Message = message;
            TitleMsg = title;
            AllUsers = [.. allUsers];
            SelectedUsers = [.. selectedUsers];
        }

        public List<AppDto.UserDto> GetSelectedUsers()
            => [.. SelectedUsers];

        [RelayCommand]
        public void Cancel()
        {
            RequestClose?.Invoke(null);
        }

        [RelayCommand]
        public void Validate()
        {
            RequestClose?.Invoke(GetSelectedUsers().ToArray());
        }
    }
}
