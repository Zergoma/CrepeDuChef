using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrepeDuChef.Application.ValueObjects;
using CrepeDuChef.Localization.Resources.languages;


namespace CrepeDuChef.ViewModels.ViewModels
{
    /// <summary>
    /// Represents the view model for editing user information, providing properties and commands for managing user
    /// input and validation in a user editor UI.
    /// </summary>
    /// <remarks>This view model exposes properties for the user's first and last names, as well as commands
    /// to save or cancel the editing operation. It raises the RequestClose event to signal when the editor should be
    /// closed, optionally providing the entered user data. The CanValidate property indicates whether the current input
    /// is valid for submission. This class is typically used in MVVM scenarios to support user editing dialogs or
    /// forms.</remarks>
    public partial class UserEditorViewModel : ObservableObject
    {
        public event Action<UserFormData?>? RequestClose;

        [ObservableProperty]
        public partial string FirstName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string LastName { get; set; } = string.Empty;

        public string FirstNameMsg => Traduction.FirstName;
        public string LastNameMsg => Traduction.LastName;
        public string CancelMsg => Traduction.Cancel;
        public string ValidateMsg => Traduction.Validate;
        public string UserEditorMsg => Traduction.Title_UserEditor;

        public UserEditorViewModel(
            string firstName = "",
            string lastName = "")
        {
            FirstName = firstName;
            LastName = lastName;
        }

        [RelayCommand]
        private void Cancel()
        {
            RequestClose?.Invoke(null);
        }

        [RelayCommand]
        private void Save()
        {
            var data = new UserFormData(FirstName, LastName);
            RequestClose?.Invoke(data);
        }

        public bool CanValidate =>
            !string.IsNullOrWhiteSpace(FirstName)
            && !string.IsNullOrWhiteSpace(LastName);

        partial void OnFirstNameChanged(string value)
            => OnPropertyChanged(nameof(CanValidate));

        partial void OnLastNameChanged(string value)
            => OnPropertyChanged(nameof(CanValidate));

    }
}
