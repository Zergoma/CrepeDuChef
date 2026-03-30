using CrepeDuChef.Application.ValueObjects;

namespace CrepeDuChef.Application.Extensions
{
    public static class UserOperationResultExtensions
    {
        public static async Task<UserOperationResult> OnSuccess(
            this UserOperationResult result,
            Func<Task> action)
        {
            if (result.Status == OperationStatus.Success)
                await action();

            return result;
        }

        public static async Task<UserOperationResult> OnFailure(
            this UserOperationResult result,
            Func<string, Task> action)
        {
            if (result.Status == OperationStatus.Failure)
                await action(result.ErrorMessage);

            return result;
        }

        public static async Task<UserOperationResult> OnCancel(
            this UserOperationResult result,
            Func<Task> action)
        {
            if (result.Status == OperationStatus.Canceled)
                await action();

            return result;
        }
    }
}
