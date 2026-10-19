using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrepeDuChef.Localization.Resources;
using System;

namespace CrepeDuChef.Avalonia.Dialogs.MessageDialogs
{
    public partial class MessageDialogViewModel : ObservableObject
    {
        public event Action? RequestClose;

        public string Title { get; }
        public string Message { get; }
        public MessageKind Kind { get; }

        public string OkMsg => UiResources.Ok;

        public MessageDialogViewModel(string title, string message, MessageKind kind)
        {
            Title = title;
            Message = message;
            Kind = kind;
        }

        [RelayCommand]
        private void Close() => RequestClose?.Invoke();
    }
}
