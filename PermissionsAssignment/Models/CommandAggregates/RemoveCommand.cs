namespace PermissionsAssignment.Models.CommandAggregates
{
    public class RemoveCommand : ICommand
    {
        public string CommandString { get; private set; }

        public RemoveCommand(string commandString)
        {
            CommandString = commandString;
        }

        public string Execute(TreeNode<Staff> rootNode)
        {
            var cmdParts = CommandString.Split();
            var staffName = cmdParts[1];
            var permissionName = cmdParts[2];

            var tree = new TreeVisitor<Staff>();
            var removePermissionStaff = tree.Find(rootNode, x => x.Name == staffName).Data;
            removePermissionStaff.RemovePermission(new Permission(permissionName));

            return string.Empty;
        }
    }
}
