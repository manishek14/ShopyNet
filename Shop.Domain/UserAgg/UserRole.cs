using Common.Domain;
using System;

namespace Shop.Domain.UserAgg
{
    public class UserRole : BaseEntity
    {
        // EF Core
        private UserRole() { }

        public UserRole(Guid roleId)
        {
            if (roleId == Guid.Empty)
                throw new ArgumentException("RoleId cannot be empty.", nameof(roleId));

            RoleId = roleId;
        }

        public Guid UserId { get; internal set; }
        public Guid RoleId { get; private set; }
    }
}