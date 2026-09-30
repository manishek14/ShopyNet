namespace Common.Domain.Exceptions
{
    public class InvalidDomainDataException : Exception
    {
        public InvalidDomainDataException(string message) : base(message) { }
        public InvalidDomainDataException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}