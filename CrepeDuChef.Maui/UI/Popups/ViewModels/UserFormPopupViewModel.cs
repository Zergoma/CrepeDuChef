using CommunityToolkit.Mvvm.ComponentModel;
using CrepeDuChef.Application.ValueObjects;

namespace CrepeDuChef.Maui.UI.Popups.ViewModels
{
    public partial class UserFormPopupViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial string FirstName { get; set; }

        [ObservableProperty]
        public partial string LastName { get; set; }

        [ObservableProperty]
        public partial string Title { get; set; }

        [ObservableProperty]
        public partial string ValidateButtonText { get; set; }


        public UserFormPopupViewModel(
            string firstName = "",
            string lastName = "",
            string title = "",
            string validateButtonText = "")
        {
            FirstName = firstName;
            LastName = lastName;
            Title = title;
            ValidateButtonText = validateButtonText;
        }

        public bool CanValidate =>
        !string.IsNullOrWhiteSpace(FirstName) &&
        !string.IsNullOrWhiteSpace(LastName);

        partial void OnFirstNameChanged(string value) =>
            OnPropertyChanged(nameof(CanValidate));

        partial void OnLastNameChanged(string value) =>
            OnPropertyChanged(nameof(CanValidate));

        public UserFormData ToResult() =>
            new(FirstName, LastName);

    }
}
