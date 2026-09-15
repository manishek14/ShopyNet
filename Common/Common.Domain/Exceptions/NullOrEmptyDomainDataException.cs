using System;

namespace Shop.Domain.UserAgg
{
    [Serializable]
    public class NullOrEmptyDomainDataException : Exception
    {
        public NullOrEmptyDomainDataException()
        {
        }

        public NullOrEmptyDomainDataException(string? message) : base(message)
        {
        }

        public NullOrEmptyDomainDataException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public static void CheckString(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new NullOrEmptyDomainDataException($"{paramName} cannot be null or empty.");
        }
    }
}