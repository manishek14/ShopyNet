using Common.Domain;
using Shop.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Domain.RoleAgg
{
    public partial class Role : BaseAggregate
    {
        // EF Core
        private Role() { }

        public Role(string title, List<RolePermission> permissions)
        {
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));

            if (permissions == null || permissions.Count == 0)
                throw new NullOrEmptyDomainDataException("Permissions cannot be empty.");

            Title = title;
            Permissions = permissions;
            CreatedAt = DateTime.Now;
        }

        public Role(string title)
        {
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));

            Title = title;
            Permissions = new List<RolePermission>();
            CreatedAt = DateTime.Now;
        }

        public string Title { get; private set; } = string.Empty;
        public List<RolePermission> Permissions { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public void Edit(string title)
        {
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));
            Title = title;
            UpdatedAt = DateTime.Now;
        }

        public void SetPermission(List<RolePermission> permissions)
        {
            if (permissions == null)
                throw new ArgumentNullException(nameof(permissions));

            Permissions.Clear();
            Permissions.AddRange(permissions);
            UpdatedAt = DateTime.Now;
        }

        public void AddPermission(Permission permission)
        {
            if (Permissions.Any(p => p.Permission == permission))
                throw new InvalidOperationException($"Permission '{permission}' already exists.");

            var rolePermission = new RolePermission(Id, permission);
            Permissions.Add(rolePermission);
            UpdatedAt = DateTime.Now;
        }

        public void RemovePermission(Permission permission)
        {
            var rolePermission = Permissions.FirstOrDefault(p => p.Permission == permission);
            if (rolePermission == null)
                throw new KeyNotFoundException($"Permission '{permission}' not found.");

            Permissions.Remove(rolePermission);
            UpdatedAt = DateTime.Now;
        }

        public bool HasPermission(Permission permission)
        {
            return Permissions.Any(p => p.Permission == permission);
        }

        public bool HasAnyPermission(params Permission[] permissions)
        {
            return permissions.Any(p => HasPermission(p));
        }

        public bool HasAllPermissions(params Permission[] permissions)
        {
            return permissions.All(p => HasPermission(p));
        }

        public List<Permission> GetPermissions()
        {
            return Permissions.Select(p => p.Permission).ToList();
        }
    }
}