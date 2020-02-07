using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PermissionsAssignment.Models;

namespace PermissionsAssignment.Test.Models
{
    public class StaffPermissionTest
    {
        private Staff staff;

        [SetUp]
        public void Setup()
        {
            staff = new Staff("CEO");
            var permissions = new List<Permission>
            {
                new Permission("A"),
                new Permission("B"),
                new Permission("C"),
            };
            staff.AddPermissions(permissions);
        }

        [TestCase("X")]
        [TestCase("Y")]
        [TestCase("A")]
        public void AddPermission(string permissionName)
        {
            var permission = new Permission(permissionName);
            staff.AddPermissions(new Permission[] { permission });
            Assert.Contains(permission, staff.Permissions.ToList());
        }

        [TestCase("A")]
        [TestCase("B")]
        [TestCase("X")]
        public void RemovePermission(string permissionName)
        {
            var permission = new Permission(permissionName);
            staff.AddPermissions(new Permission[] { permission });
            Assert.Contains(permission, staff.Permissions.ToList());

            staff.RemovePermission(permission);
            Assert.That(!staff.Permissions.ToList().Contains(permission));
        }
    }
}
