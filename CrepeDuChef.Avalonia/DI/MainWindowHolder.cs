using Avalonia.Controls;
using System;

namespace CrepeDuChef.Avalonia.DI
{
    /// <summary>
    /// Gives dialogs their owner window, which only exists once the application has started.
    /// </summary>
    public class MainWindowHolder
    {
        public Window? Window { get; set; }

        public Window RequiredWindow =>
            Window ?? throw new InvalidOperationException("The main window is not created yet.");
    }
}
