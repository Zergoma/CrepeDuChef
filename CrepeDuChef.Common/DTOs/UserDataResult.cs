namespace CrepeDuChef.Common.DTOs
{
    public class UserDataResult
    {
        required public FormResultStatus Status { get; init; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string FirstNameUpdate { get; init; } = string.Empty;
        public string LastNameUpdate { get; init; } = string.Empty;
    }
}
