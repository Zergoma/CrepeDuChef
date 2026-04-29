using CommunityToolkit.Maui.Views;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.ValueObjects;

namespace CrepeDuChef.Maui.UI.Popups.Views
{
    public partial class SelectUsersPopup : Popup<UserSelectionResult?>
    {
        private readonly List<UserDto> _allUsers;
        private readonly List<UserDto> _selectedUsers;

        public SelectUsersPopup(
            string title,
            string message,
            List<UserDto> allUsers,
            List<UserDto> selectedUsers)
        {
            InitializeComponent();

            TitleLabel.Text = title;
            MessageLabel.Text = message;

            _allUsers = allUsers;
            _selectedUsers = selectedUsers;

            UsersList.ItemsSource = _allUsers;

            // Pré-sélection
            UsersList.SelectedItems = new List<object>(_selectedUsers);
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await CloseAsync(null);
        }

        private async void OnValidateClicked(object sender, EventArgs e)
        {
            var selected = UsersList.SelectedItems
                .Cast<UserDto>()
                .ToList();

            await CloseAsync(new UserSelectionResult(selected.ToArray()));
        }
    }
}
