using System.Linq;

namespace PermissionsAssignment.Models.CommandAggregates
{
    public class QueryCommand : ICommand
    {
        public string CommandString { get; private set; }

        public QueryCommand(string commandString)
        {
            CommandString = commandString;
        }

        public string Execute(TreeNode<Staff> rootNode)
        {
            var staffName = CommandString.Split()[1];

            var tree = new TreeVisitor<Staff>();

            var staffNode = tree.Find(rootNode, x => x.Name == staffName);
            var permissions = tree.Traverse(staffNode, staff => staff.Permissions);

            return string.Join(", ", permissions.Select(p => p.Name));
        }
    }
}
