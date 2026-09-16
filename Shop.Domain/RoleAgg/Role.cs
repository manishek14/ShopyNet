using Shop.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.RoleAgg
{
    public partial class Role
    {
        public Role(string title, List<RolePermission> permissions)
        {
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));
            Title = title;
            Permissions = permissions;
        }

        public Role(string title)
        {
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));
            Title = title;
            Permissions = new List<RolePermission>();
        }

        public string Title { get; private set; }
        public List<RolePermission> Permissions { get; private set; }

        // Ef
        private Role() { }

        public void Edit(string title)
        {
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));
            Title = title;
        }

        public void SetPermission (List<RolePermission> permission)
        {
            Permissions = permission;
        }
    }
}
