using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.Exceptions
{
    public class RepositoryException : DomainException
    {
        public RepositoryException(string message) : base(message) { }
        public RepositoryException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class SaveChangesFailedException : RepositoryException
    {
        public SaveChangesFailedException(string entityName, Exception innerException)
            : base($"Failed to save changes for '{entityName}'.", innerException) { }
    }

    public class ConcurrencyException : RepositoryException
    {
        public ConcurrencyException(string entityName, object entityId)
            : base($"Concurrency conflict occurred while updating '{entityName}' with ID '{entityId}'.") { }
    }

    public class DatabaseConnectionException : RepositoryException
    {
        public DatabaseConnectionException(string connectionString, Exception innerException)
            : base($"Failed to connect to database: {connectionString}.", innerException) { }
    }
}
