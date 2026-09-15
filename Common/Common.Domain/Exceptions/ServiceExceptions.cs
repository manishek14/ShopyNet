using System;
using System.Collections.Generic;
using System.Text;


namespace Common.Domain.Exceptions
{
    public class ServiceException : DomainException
    {
        public ServiceException(string message) : base(message) { }
        public ServiceException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class OrderServiceException : ServiceException
    {
        public OrderServiceException(string operation, string message, Exception? innerException = null)
            : base($"Order Service: {operation} failed - {message}", innerException) { }
    }

    public class ProductServiceException : ServiceException
    {
        public ProductServiceException(string operation, string message, Exception? innerException = null)
            : base($"Product Service: {operation} failed - {message}", innerException) { }
    }

    public class ValidationException : ServiceException
    {
        public string PropertyName { get; }
        public string ErrorMessage { get; }

        public ValidationException(string propertyName, string errorMessage)
            : base($"Validation failed for '{propertyName}': {errorMessage}")
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
    }
}