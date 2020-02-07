using System;

namespace PermissionsAssignment.Models
{
    public class Permission
    {
        public string Name { get; private set; }

        public Permission(string name)
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
        }

        public override bool Equals(object obj)
        {
            return !(obj is Permission p) ? false : p.Name == Name;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Name.GetHashCode();
            return hash;
        }

        public static bool operator ==(Permission lhs, Permission rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Permission lhs, Permission rhs)
        {
            return !lhs.Equals(rhs);
        }
    }
}
