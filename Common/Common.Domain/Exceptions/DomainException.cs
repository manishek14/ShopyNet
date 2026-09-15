using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message) { }
        protected DomainException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class BusinessRuleViolationException : DomainException
    {
        public string RuleName { get; }
        public BusinessRuleViolationException(string ruleName, string message)
            : base($"[{ruleName}] {message}")
        {
            RuleName = ruleName;
        }
    }

    public class EntityNotFoundException : DomainException
    {
        public string EntityName { get; }
        public object EntityId { get; }

        public EntityNotFoundException(string entityName, object entityId)
            : base($"Entity '{entityName}' with ID '{entityId}' was not found.")
        {
            EntityName = entityName;
            EntityId = entityId;
        }
    }

    public class DuplicateEntityException : DomainException
    {
        public string EntityName { get; }
        public string PropertyName { get; }
        public object PropertyValue { get; }

        public DuplicateEntityException(string entityName, string propertyName, object propertyValue)
            : base($"Entity '{entityName}' with {propertyName} '{propertyValue}' already exists.")
        {
            EntityName = entityName;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }
    }
}
