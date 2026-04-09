using Avalonia.Controls;
using CrepeDuChef.Avalonia.Dialogs;
using System.Threading.Tasks;

namespace CrepeDuChef.Avalonia.Services
{
    public class DialogService : IDialogService
    {
        public Task ShowMessageAsync(IDialog dialog, Window owner)
            => dialog.ShowAsync(owner);

        public Task<TResult?> ShowDialogAsync<TResult>(IDialog<TResult> dialog, Window owner)
            => dialog.ShowAsync(owner);
        
    }
}
