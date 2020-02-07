using System;
using System.Collections.Generic;

namespace PermissionsAssignment.Models
{
    public class Staff
    {
        private readonly List<Permission> _permissions;
        public IEnumerable<Permission> Permissions => _permissions;

        public string Name { get; private set; }

        public Staff(string name)
        {
            _permissions = new List<Permission>();
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
        }

        public void AddPermissions(IEnumerable<Permission> pemissions)
        {
            foreach (var p in pemissions)
            {
                if (!_permissions.Contains(p))
                {
                    _permissions.Add(p);
                }
            }
        }

        public void RemovePermission(Permission permission)
        {
            _permissions.Remove(permission);
        }

        public override bool Equals(object obj)
        {
            return obj is Staff s ? s.Name == Name : false;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Name.GetHashCode();
            return hash;
        }

        public static bool operator ==(Staff lhs, Staff rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Staff lhs, Staff rhs)
        {
            return !lhs.Equals(rhs);
        }
    }
}
