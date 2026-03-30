using CrepeDuChef.Maui.UI.Dialogs;

namespace CrepeDuChef.Maui.UI.Extentions
{
    public static class DialogResultExtensions
    {
        public static DialogResult<T> ToResult<T>(this T? data)
        {
            return data == null
                ? DialogResult<T>.Cancel()
                : DialogResult<T>.Success(data);
        }
    }
}
