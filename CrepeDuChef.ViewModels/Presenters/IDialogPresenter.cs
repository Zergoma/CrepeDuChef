using System;
using System.Collections.Generic;
using System.Text;

namespace CrepeDuChef.ViewModels.Presenters
{
    public interface IDialogPresenter
    {
        Task ShowSuccessAsync(string title, string message);
        Task ShowErrorAsync(string title, string message);
        Task ShowWarningAsync(string title, string message);
    }
}
