using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.ViewModels.Models;
using Loca = CrepeDuChef.Localization.Resources.languages;

namespace CrepeDuChef.ViewModels.ViewModels
{
    /// <summary>
    /// Multi-selection of chefs. Raises <see cref="RequestClose"/> with the selection, or null when cancelled.
    /// </summary>
    public partial class SelectChefsDialogViewModel : ObservableObject
    {
        public event Action<IReadOnlyList<ChefDto>?>? RequestClose;

        public string Title { get; }
        public string Message { get; }
        public IReadOnlyList<SelectableChef> Chefs { get; }

        public string CancelMsg => Loca.Traduction.Cancel;
        public string ValidateMsg => Loca.Traduction.Validate;

        public SelectChefsDialogViewModel(
            string title,
            string message,
            IEnumerable<ChefDto> allChefs,
            IEnumerable<ChefDto> selectedChefs)
        {
            Title = title;
            Message = message;

            HashSet<Guid> selectedIds = [.. selectedChefs.Select(c => c.Id)];
            Chefs = [.. allChefs.Select(c => new SelectableChef(c, selectedIds.Contains(c.Id)))];
        }

        [RelayCommand]
        private void Cancel() => RequestClose?.Invoke(null);

        [RelayCommand]
        private void Validate()
            => RequestClose?.Invoke([.. Chefs.Where(c => c.IsSelected).Select(c => c.Chef)]);
    }
}
