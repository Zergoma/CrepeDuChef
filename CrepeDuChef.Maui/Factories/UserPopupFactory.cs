using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Interfaces;
using CrepeDuChef.Maui.Resources.languages;
using UXDivers.Popups.Maui.Controls;

namespace CrepeDuChef.Maui.Factories
{
    public class UserPopupFactory : IUserPopupFactory<FormPopup, UserDto>
    {
        public FormPopup CreateAddUserForm()
        {
            return
                new FormPopup()
                {
                    Title = Traduction.AddAChef,
                    Text = Traduction.EnterFirstAndLastName,
                    ActionButtonText = Traduction.Add,
                    ShowActionButton = true,
                    Items =
                    new List<FormField>
                    {
                        new ()
                        {
                            IconColor = App.Current?.Resources["TextColor"] as Color,
                            Placeholder = Traduction.FirstName,
                        },
                        new ()
                        {
                            IconColor = App.Current?.Resources["TextColor"] as Color,
                            Placeholder =Traduction.LastName,
                        }
                    }
                };
        }

        public FormPopup CreateUpdateUserForm(UserDto user)
        {
            return
                new FormPopup()
                {
                    Title = Traduction.UpdateAChef,
                    Text = Traduction.UpdateFirstAndOrLastName,
                    ActionButtonText = Traduction.Update,
                    ShowActionButton = true,
                    Items =
                        new List<FormField>
                        {
                            new ()
                            {
                                IconColor = App.Current?.Resources["TextColor"] as Color,
                                Placeholder = Traduction.FirstName,
                                Value = user.FirstName,
                            },
                            new ()
                            {
                                IconColor = App.Current?.Resources["TextColor"] as Color,
                                Placeholder =Traduction.LastName,
                                Value=user.LastName,
                            }
                        }
                };
        }
    }
}
