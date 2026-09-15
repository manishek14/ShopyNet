using System;
using System.Collections.Generic;

namespace Common.Domain
{
    public class BaseEntity
    {
        public Guid Id { get; private set; }
        public DateTime CreationDate { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
    }
}