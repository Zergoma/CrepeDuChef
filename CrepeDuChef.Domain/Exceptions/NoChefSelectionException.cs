namespace CrepeDuChef.Domain.Exceptions
{
    [Serializable]
    public class NoChefSelectionException : Exception
    {
        public NoChefSelectionException()
        {
        }

        public NoChefSelectionException(string? message) : base(message)
        {
        }

        public NoChefSelectionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}