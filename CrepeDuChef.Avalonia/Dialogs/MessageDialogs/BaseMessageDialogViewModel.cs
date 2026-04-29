using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace CrepeDuChef.Avalonia.Dialogs.ViewModels
{
    public partial class BaseMessageDialogViewModel : ObservableObject
    {
        public event Action? RequestClose;

        public string Title { get; }
        public string Message { get; }

        public string Icon { get; }
        public string BackgroundColor { get; }
        public string ForegroundColor { get; }

        public BaseMessageDialogViewModel(
            string title,
            string message,
            string icon,
            string backgroundColor,
            string foregroundColor)
        {
            Title = title;
            Message = message;
            Icon = icon;
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
        }

        [RelayCommand]
        public void Close()
            => RequestClose?.Invoke();
    }
}
