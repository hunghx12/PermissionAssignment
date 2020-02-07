using System;
using System.Collections.Generic;
using System.Linq;

namespace PermissionsAssignment.Models
{
    public class TreeVisitor<T> where T : class
    {
        public IEnumerable<M> Traverse<M>(TreeNode<T> node, Func<T, IEnumerable<M>> visitor)
        {
            if (node == null)
                return null;
            var output = visitor?.Invoke(node.Data);
            foreach (TreeNode<T> kid in node.Children)
                output = output?.Concat(Traverse(kid, visitor));
            return output.Distinct();
        }

        public TreeNode<T> Find(TreeNode<T> root, Predicate<T> predicate)
        {
            if (root == null)
                return null;
            var found = predicate?.Invoke(root.Data);
            
            if (found.GetValueOrDefault())
                return root;
            foreach (var child in root.Children)
            {
                var foundChild = Find(child, predicate);
                if (foundChild != null)
                    return foundChild;
            }
            return null;
        }
    }
}
