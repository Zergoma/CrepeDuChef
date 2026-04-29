using Avalonia.Controls;
using System;

namespace CrepeDuChef.Avalonia.Services
{
    public interface INavigationService
    {
        event Action<Type>? OnNavigate;
        void NavigateTo<TView>() where TView : Control;
    }
}
