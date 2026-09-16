using Common.Domain;
using System;

namespace Shop.Domain.RoleAgg
{
    public partial class Role
    {
        public class RolePermission : BaseEntity
        {
            // EF Core
            private RolePermission() { }

            public RolePermission(Guid roleId, Permission permission)
            {
                if (roleId == Guid.Empty)
                    throw new ArgumentException("RoleId cannot be empty.", nameof(roleId));

                RoleId = roleId;
                Permission = permission;
            }

            public Guid RoleId { get; private set; }
            public Permission Permission { get; private set; }
        }
    }
}