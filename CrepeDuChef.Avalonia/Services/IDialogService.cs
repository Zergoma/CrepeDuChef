using Avalonia.Controls;
using CrepeDuChef.Avalonia.Dialogs;
using System.Threading.Tasks;

namespace CrepeDuChef.Avalonia.Services
{
    public interface IDialogService
    {
        Task<TResult?> ShowDialogAsync<TResult>(IDialog<TResult> dialog, Window owner);
        Task ShowMessageAsync(IDialog dialog, Window owner);
    }
}
