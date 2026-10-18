using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Media.Imaging;
using Avalonia.Styling;
using Avalonia.Threading;

namespace CrepeDuChef.Avalonia.Tests.Support
{
    /// <summary>
    /// Renders a window and saves it as a PNG under the test output "snapshots" folder,
    /// to review the UI without launching the app.
    /// </summary>
    public static class Snapshot
    {
        public static string Directory { get; } = Path.Combine(AppContext.BaseDirectory, "snapshots");

        public static string Save(Window window, string name, ThemeVariant? theme = null)
        {
            if (theme is not null)
            {
                window.RequestedThemeVariant = theme;
            }

            window.Show();
            Dispatcher.UIThread.RunJobs();
            AvaloniaHeadlessPlatform.ForceRenderTimerTick();

            using WriteableBitmap? frame = window.CaptureRenderedFrame();
            if (frame is null)
            {
                throw new InvalidOperationException("No frame was rendered.");
            }

            System.IO.Directory.CreateDirectory(Directory);
            string suffix = theme == ThemeVariant.Dark ? "-dark" : string.Empty;
            string path = Path.Combine(Directory, $"{name}{suffix}.png");
            frame.Save(path);

            window.Close();
            return path;
        }
    }
}
