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
            var win = new Window(appshell);
#if WINDOWS
            win.Width = 800;
            win.Height = 800;
#endif
            return win;
        }
    }
}