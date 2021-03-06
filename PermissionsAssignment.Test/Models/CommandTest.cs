﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PermissionsAssignment.Models;
using PermissionsAssignment.Models.CommandAggregates;

namespace PermissionsAssignment.Test.Models
{
    public class CommandTest
    {
        private TreeVisitor<Staff> treeVisitor;
        private TreeNode<Staff> root;
        private List<Permission> permissions;

        /// <summary>
        /// F1:         CEO(F)
        /// F2:     1(A)        2(B)
        /// F3:    3(C, E)     4(D) - 5(X)
        /// </summary>
        [SetUp]
        public void Setup()
        {
            permissions = new List<Permission>
            {
                new Permission("A"),
                new Permission("B"),
                new Permission("C"),
                new Permission("D"),
                new Permission("E"),
                new Permission("F"),
                new Permission("X"),
            };

            treeVisitor = new TreeVisitor<Staff>();
            var ceo = new Staff("CEO");
            ceo.AddPermissions(permissions.Where(x => x.Name == "F"));
            root = new TreeNode<Staff>(ceo);

            var staff1 = new Staff("1");
            staff1.AddPermissions(permissions.Where(x => x.Name == "A"));
            var staff1Node = new TreeNode<Staff>(staff1);

            var staff2 = new Staff("2");
            staff2.AddPermissions(permissions.Where(x => x.Name == "B"));
            var staff2Node = new TreeNode<Staff>(staff2);

            var staff3 = new Staff("3");
            staff3.AddPermissions(permissions.Where(x => x.Name == "C" || x.Name == "E"));
            var staff3Node = new TreeNode<Staff>(staff3);

            var staff4 = new Staff("4");
            staff4.AddPermissions(permissions.Where(x => x.Name == "D"));
            var staff4Node = new TreeNode<Staff>(staff4);

            var staff5 = new Staff("5");
            staff5.AddPermissions(permissions.Where(x => x.Name == "X"));
            var staff5Node = new TreeNode<Staff>(staff5);

            root.AddChild(staff1Node);
            root.AddChild(staff2Node);

            staff1Node.AddChild(staff3Node);

            staff2Node.AddChild(staff4Node);
            staff2Node.AddChild(staff5Node);

        }

        [Test]
        public void AddCommandTest()
        {
            ICommand addCommand = new AddCommand("ADD 2 F");

            var result = addCommand.Execute(root);

            var staff = treeVisitor.Find(root, x => x.Name == "2").Data;

            Assert.IsTrue(staff.Permissions.FirstOrDefault(x => x.Name == "F") != null);
        }

        [Test]
        public void RemoveCommandTest()
        {
            ICommand removeCommand = new RemoveCommand("REMOVE 3 E");

            var result = removeCommand.Execute(root);

            var staff = treeVisitor.Find(root, x => x.Name == "3").Data;

            Assert.IsNull(staff.Permissions.FirstOrDefault(x => x.Name == "E"));
        }
    }
}
