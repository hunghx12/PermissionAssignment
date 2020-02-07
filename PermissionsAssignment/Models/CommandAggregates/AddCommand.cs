using System.Collections.Generic;

namespace PermissionsAssignment.Models.CommandAggregates
{
    public class AddCommand : ICommand
    {
        public string CommandString { get; private set; }

        public AddCommand(string commandString)
        {
            CommandString = commandString;
        }

        public string Execute(TreeNode<Staff> rootNode)
        {
            var cmdParts = CommandString.Split();
            var staffName = cmdParts[1];
            var permissionName = cmdParts[2];

            var tree = new TreeVisitor<Staff>();
            var addPermissionStaff = tree.Find(rootNode, x => x.Name == staffName).Data;
            addPermissionStaff.AddPermissions(new List<Permission> { new Permission(permissionName) });

            return string.Empty;
        }
    }
}
