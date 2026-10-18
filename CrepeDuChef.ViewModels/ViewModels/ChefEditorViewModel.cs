using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.ViewModels.Models;
using Loca = CrepeDuChef.Localization.Resources.languages;

namespace CrepeDuChef.ViewModels.ViewModels
{
    /// <summary>
    /// Form used to add or edit a chef. Raises <see cref="RequestClose"/> with the entered names,
    /// or null when cancelled.
    /// </summary>
    public partial class ChefEditorViewModel : ObservableObject
    {
        public event Action<ChefFormData?>? RequestClose;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanValidate))]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        public partial string FirstName { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanValidate))]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        public partial string LastName { get; set; }

        public string Title { get; }
        public string ValidateMsg { get; }

        public string FirstNameMsg => Loca.Traduction.FirstName;
        public string LastNameMsg => Loca.Traduction.LastName;
        public string CancelMsg => Loca.Traduction.Cancel;

        public bool CanValidate =>
            !string.IsNullOrWhiteSpace(FirstName)
            && !string.IsNullOrWhiteSpace(LastName);

        private ChefEditorViewModel(string title, string validateMsg, string firstName, string lastName)
        {
            Title = title;
            ValidateMsg = validateMsg;
            FirstName = firstName;
            LastName = lastName;
        }

        public static ChefEditorViewModel ForAdd()
            => new(Loca.Traduction.AddAChef, Loca.Traduction.Add, string.Empty, string.Empty);

        public static ChefEditorViewModel ForEdit(ChefDto chef)
            => new(Loca.Traduction.UpdateAChef, Loca.Traduction.Update, chef.FirstName, chef.LastName);

        [RelayCommand]
        private void Cancel() => RequestClose?.Invoke(null);

        [RelayCommand(CanExecute = nameof(CanValidate))]
        private void Save() => RequestClose?.Invoke(new ChefFormData(FirstName.Trim(), LastName.Trim()));
    }
}
