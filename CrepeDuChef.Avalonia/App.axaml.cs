using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CrepeDuChef.Avalonia.DI;
using CrepeDuChef.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using AvaloniaApp = Avalonia.Application;

namespace CrepeDuChef.Avalonia
{
    public partial class App : AvaloniaApp
    {
        private IServiceProvider? _services;
        public IServiceProvider Services => _services ?? throw new InvalidOperationException("App.Services not initialized");

        public void InitializeServices(IServiceProvider sp)
        {
            _services = sp;
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var sp = Services;

                desktop.MainWindow = sp.GetRequiredService<MainWindow>();

                MainWindowHolder windowHolder = sp.GetRequiredService<MainWindowHolder>();
                windowHolder.Window = desktop.MainWindow;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}