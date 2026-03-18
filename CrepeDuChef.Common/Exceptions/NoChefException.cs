namespace CrepeDuChef.Common.Exceptions
{
    [Serializable]
    public class NoChefException : Exception
    {
        public NoChefException()
        {
        }

        public NoChefException(string? message) : base(message)
        {
        }

        public NoChefException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}