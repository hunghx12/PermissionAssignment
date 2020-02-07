using System;
using System.Collections.Generic;
using System.Linq;
using PermissionsAssignment.Infrastructure;
using PermissionsAssignment.Models;

namespace PermissionsAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new TreeVisitor<Staff>();
            var repository = StaffRepository.GetInstance("Data/input.txt");
            var root = repository.GetRoot();

            var staffQueue = new Queue<TreeNode<Staff>>();
            staffQueue.Enqueue(root);
            while(staffQueue.Count != 0)
            {
                var staffNode = staffQueue.Dequeue();
                var permissions = tree.Traverse(staffNode, staff => staff.Permissions);
                Console.WriteLine($"{staffNode.Data.Name}'s accessible permissions: {string.Join(", ", permissions.Select(p => p.Name))}.");

                staffNode.Children.ForEach(x => staffQueue.Enqueue(x));
            }

            Console.WriteLine("_______________________");

            var commandRepository = CommandAndQueryRepository.GetInstance("Data/commands.txt");
            var commands = commandRepository.GetCommands();
            foreach (var command in commands)
            {
                var result = command.Execute(root);
                if (!string.IsNullOrEmpty(result))
                    Console.WriteLine($"Result of {command.CommandString} command: {command.Execute(root)}.");
                else
                    Console.WriteLine($"Command {command.CommandString} executed.");
            }
        }
    }
}
