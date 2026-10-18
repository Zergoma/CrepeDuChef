using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Styling;
using CrepeDuChef.Avalonia.Dialogs.Factories;
using CrepeDuChef.Avalonia.Tests.Support;
using CrepeDuChef.Avalonia.ViewModels;
using CrepeDuChef.Avalonia.Views;
using CrepeDuChef.ViewModels.ViewModels;
using FluentAssertions;

namespace CrepeDuChef.Avalonia.Tests.Screens
{
    /// <summary>
    /// Renders every screen and dialog in light and dark themes (PNG files in bin/.../snapshots)
    /// and fails on binding errors.
    /// </summary>
    public class ScreenRenderTests
    {
        public static TheoryData<string> Themes => new() { "Light", "Dark" };

        private static ThemeVariant Theme(string name) => name == "Dark" ? ThemeVariant.Dark : ThemeVariant.Light;

        private static async Task<MainWindow> MainWindowOnAsync(bool chefsPage, bool empty = false)
        {
            MainViewModel vm = new(
                SampleData.CrepeSessionsViewModel(empty),
                SampleData.ChefSettingsViewModel(empty));

            MainWindow window = new(vm) { Width = 1000, Height = 720 };

            if (chefsPage)
            {
                await vm.GoToChefsCommand.ExecuteAsync(null);
            }
            else
            {
                await vm.GoToCrepePartyCommand.ExecuteAsync(null);
            }

            return window;
        }

        private static void Render(Window window, string name, string theme)
        {
            // Closing tears bindings down and may log transient errors: only the rendering is checked
            using (BindingErrorSink bindings = new())
            {
                Snapshot.Save(window, name, Theme(theme));

                bindings.Errors.Should().BeEmpty();
            }

            window.Close();
        }

        [AvaloniaTheory]
        [MemberData(nameof(Themes))]
        public async Task Crepe_sessions_page(string theme)
            => Render(await MainWindowOnAsync(chefsPage: false), "crepe-sessions", theme);

        [AvaloniaTheory]
        [MemberData(nameof(Themes))]
        public async Task Crepe_sessions_page_empty(string theme)
            => Render(await MainWindowOnAsync(chefsPage: false, empty: true), "crepe-sessions-empty", theme);

        [AvaloniaTheory]
        [MemberData(nameof(Themes))]
        public async Task Chefs_page(string theme)
            => Render(await MainWindowOnAsync(chefsPage: true), "chefs", theme);

        [AvaloniaTheory]
        [MemberData(nameof(Themes))]
        public async Task Chefs_page_empty(string theme)
            => Render(await MainWindowOnAsync(chefsPage: true, empty: true), "chefs-empty", theme);

        [AvaloniaTheory]
        [MemberData(nameof(Themes))]
        public void Message_dialogs(string theme)
        {
            AvaloniaMessageDialogFactory factory = new();

            Render((Window)factory.CreateSuccess("Chef du jour", "Alice Delta-Colli Loth"), "dialog-success", theme);
            Render((Window)factory.CreateWarning("Aucun chef", "Il faut au moins un chef pour lancer le tirage."), "dialog-warning", theme);
            Render((Window)factory.CreateError("Opération annulée", "Le prénom est obligatoire."), "dialog-error", theme);
        }

        [AvaloniaTheory]
        [MemberData(nameof(Themes))]
        public void Chef_editor_dialog(string theme)
        {
            AvaloniaChefFormDialogFactory factory = new();

            Render((Window)factory.CreateEditForm(SampleData.Chefs[0]), "dialog-chef-editor", theme);
        }

        [AvaloniaTheory]
        [MemberData(nameof(Themes))]
        public void Chef_selection_dialog(string theme)
        {
            AvaloniaChefSelectionDialogFactory factory = new();

            Window dialog = (Window)factory.Create(
                "Chefs présents",
                "Sélectionnez les chefs présents aujourd'hui.",
                SampleData.Chefs,
                [SampleData.Chefs[0], SampleData.Chefs[2]]);

            Render(dialog, "dialog-chef-selection", theme);
        }
    }
}
