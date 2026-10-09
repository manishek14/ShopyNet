using System;

namespace Common.Domain
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
            IsDeleted = false;
        }

        public void Delete()
        {
            IsDeleted = true;
            DeletedAt = DateTime.Now;
        }

        public void Restore()
        {
            IsDeleted = false;
            DeletedAt = null;
        }
    }
}