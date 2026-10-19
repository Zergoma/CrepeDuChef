using CrepeDuChef.Localization.Resources;

namespace CrepeDuChef.Maui.MVVM.ViewModels
{
    /// <summary>
    /// Texts of the shell (window title and flyout entries).
    /// </summary>
    public class AppShellViewModel
    {
        public string AppTitle => UiResources.CrepeDuChef_title;
        public string CrepePartyMsg => UiResources.CrepeDuChef_title;
        public string ChefsMsg => UiResources.Chefs;
    }
}
