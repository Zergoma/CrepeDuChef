using Avalonia.Controls;
using System;

namespace CrepeDuChef.Avalonia.Services
{
    public class NavigationService : INavigationService
    {
        public event Action<Type>? OnNavigate;

        public void NavigateTo<TView>() where TView : Control
        {
            OnNavigate?.Invoke(typeof(TView));
        }
    }
}
