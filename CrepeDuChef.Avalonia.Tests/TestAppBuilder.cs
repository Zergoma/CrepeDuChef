using Avalonia;
using Avalonia.Headless;
using CrepeDuChef.Avalonia.Tests;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]

namespace CrepeDuChef.Avalonia.Tests
{
    /// <summary>
    /// Runs the real App (styles, resources) on the headless platform with Skia rendering,
    /// so windows can be captured as images.
    /// </summary>
    public static class TestAppBuilder
    {
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UseSkia()
                .UseHeadless(new AvaloniaHeadlessPlatformOptions { UseHeadlessDrawing = false });
    }
}
