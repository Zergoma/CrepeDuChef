namespace CrepeDuChef.Common
{
    public class DialogResult<T>
    {
        public DialogResultStatus Status { get; }
        public T? Data { get; }

        private DialogResult(DialogResultStatus status, T? data)
        {
            Status = status;
            Data = data;
        }

        public static DialogResult<T> Success(T data)
        {
            return new DialogResult<T>(DialogResultStatus.Success, data);
        }

        public static DialogResult<T> Cancel()
        {
            return new DialogResult<T>(DialogResultStatus.Cancel, default);
        }

        public static DialogResult<T> Failure()
        {
            return new DialogResult<T>(DialogResultStatus.Failure, default);
        }
    }
}
