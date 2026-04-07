namespace CrepeDuChef.Domain.Exceptions
{
    [Serializable]
    public class EmptyFirstNameException : Exception
    {
        public EmptyFirstNameException()
        {
        }

        public EmptyFirstNameException(string? message) : base(message)
        {
        }

        public EmptyFirstNameException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}