using CommunityToolkit.Maui.Views;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Maui.UI.Popups.Models;

namespace CrepeDuChef.Maui.UI.Popups.Views
{
    public partial class SelectUsersPopup : Popup<ChefSelectionPopupResult?>
    {
        public SelectUsersPopup(
            string title,
            string message,
            IReadOnlyList<ChefDto> allChefs,
            IReadOnlyList<ChefDto> selectedChefs)
        {
            InitializeComponent();

            TitleLabel.Text = title;
            MessageLabel.Text = message;

            UsersList.ItemsSource = allChefs;
            UsersList.SelectedItems = [.. selectedChefs];
        }

        private async void OnCancelClicked(object sender, EventArgs e)
            => await CloseAsync(null);

        private async void OnValidateClicked(object sender, EventArgs e)
            => await CloseAsync(new ChefSelectionPopupResult([.. UsersList.SelectedItems.Cast<ChefDto>()]));
    }
}
