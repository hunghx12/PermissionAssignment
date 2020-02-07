using System.Collections.Generic;

namespace PermissionsAssignment.Models
{
    public class TreeNode<T> where T : class
    {
        public readonly T Data;
        public List<TreeNode<T>> Children { get; private set; }

        public TreeNode(T data)
        {
            Data = data;
            Children = new List<TreeNode<T>>();
        }

        public void AddChild(TreeNode<T> node)
        {
            Children.Add(node);
        }
    }
}
