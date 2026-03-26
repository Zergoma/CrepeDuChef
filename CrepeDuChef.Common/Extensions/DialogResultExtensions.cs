namespace CrepeDuChef.Common.Extensions
{
    public static class DialogResultExtensions
    {
        public static DialogResult<T> ToSuccess<T>(this T data) => DialogResult<T>.Success(data);

        public static DialogResult<T> ToCancel<T>(this T _) => DialogResult<T>.Cancel();

        public static DialogResult<T> ToFailure<T>(this T _) => DialogResult<T>.Failure();

        public static DialogResult<T> ToResult<T>(this T? data)
        {
            return data == null
                ? DialogResult<T>.Cancel()
                : DialogResult<T>.Success(data);
        }
    }
}
