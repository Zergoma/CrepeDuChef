using Avalonia.Controls;
using System.Threading.Tasks;

namespace CrepeDuChef.Avalonia.Dialogs
{
    public interface IDialog
    {
        Task ShowAsync(Window owner);
    }
    public interface IDialog<TResult>
    {
        Task<TResult?> ShowAsync(Window owner);
    }
}
