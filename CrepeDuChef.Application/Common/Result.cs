namespace CrepeDuChef.Application.Common
{
    /// <summary>
    /// Outcome of an application operation that can fail for expected reasons (e.g. validation).
    /// Unexpected failures are still raised as exceptions.
    /// </summary>
    public sealed class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public IReadOnlyList<string> Errors { get; }

        private Result(bool isSuccess, T? value, IReadOnlyList<string> errors)
        {
            IsSuccess = isSuccess;
            Value = value;
            Errors = errors;
        }

        public static Result<T> Success(T value) => new(true, value, []);

        public static Result<T> Failure(IEnumerable<string> errors) => new(false, default, [.. errors]);
    }
}
