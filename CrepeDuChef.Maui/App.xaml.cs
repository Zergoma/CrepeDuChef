using CommunityToolkit.Maui.Core.Platform;

namespace CrepeDuChef.Maui
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        private IServiceProvider ServiceProvider { get; }
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            ServiceProvider = serviceProvider;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var appshell = ServiceProvider.GetRequiredService<AppShell>();
            return new Window(appshell);
        }
    }
}