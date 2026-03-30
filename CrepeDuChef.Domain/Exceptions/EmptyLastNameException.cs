namespace CrepeDuChef.Domain.Exceptions
{
    [Serializable]
    public class EmptyLastNameException : Exception
    {
        public EmptyLastNameException()
        {
        }

        public EmptyLastNameException(string? message) : base(message)
        {
        }

        public EmptyLastNameException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}