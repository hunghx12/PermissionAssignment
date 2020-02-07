namespace PermissionsAssignment.Models.CommandAggregates
{
    public interface ICommand
    {
        string CommandString { get; }
        string Execute(TreeNode<Staff> rootNode);
    }
}
