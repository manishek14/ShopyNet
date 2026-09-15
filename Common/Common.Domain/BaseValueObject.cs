using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain
{
    public abstract class BaseValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is not BaseValueObject other)
                return false;

            return GetEqualityComponents()
                .SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(0, (current, obj) =>
                    HashCode.Combine(current, obj));
        }
    }
}
