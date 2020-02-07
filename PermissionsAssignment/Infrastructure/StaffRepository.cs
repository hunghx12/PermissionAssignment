using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using PermissionsAssignment.Models;

namespace PermissionsAssignment.Infrastructure
{
    public class StaffRepository
    {
        private const string CEO = "CEO";
        private static StaffRepository _instance;

        public string InputUri { get; private set; }

        private StaffRepository(string inputUri)
        {
            InputUri = inputUri;
        }

        public static StaffRepository GetInstance(string inputUri)
        {
            return _instance ?? (_instance = new StaffRepository(inputUri));
        }

        private string[] ReadLines()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), InputUri);
            string[] content = File.ReadAllLines(path);
            return content;
        }

        public TreeNode<Staff> GetRoot()
        {
            TreeNode<Staff> ceoNode = null;

            var nodes = new List<TreeNode<Staff>>();
            var numberOfUsers = 0;
            var inputs = ReadLines();
            var lineNo = 1;
            foreach (var line in inputs)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                {
                    continue;
                }
                if (lineNo == 1)
                {
                    numberOfUsers = int.Parse(line.Trim());
                }
                else if (lineNo > 1 && lineNo <= numberOfUsers + 2)
                {
                    var staffName = lineNo == 2 ? CEO : (lineNo - 2).ToString();
                    var staff = new Staff(staffName);

                    var permissions = line.Split().Select(x => new Permission(x));
                    staff.AddPermissions(permissions);

                    var staffNode = new TreeNode<Staff>(staff);
                    if (lineNo == 2)
                    {
                        ceoNode = staffNode;
                    }
                    nodes.Add(staffNode);
                }
                else if (lineNo > numberOfUsers + 2 && lineNo <= 2 * numberOfUsers + 2)
                {
                    var staffName = (lineNo - numberOfUsers - 2).ToString();
                    var managerOfTheStaff = line.Trim();

                    var managerNode = nodes.Find(x => x.Data.Name == managerOfTheStaff);
                    var staffNode = nodes.Find(x => x.Data.Name == staffName);

                    managerNode.AddChild(staffNode);
                }
                else if (lineNo > 2 * numberOfUsers + 2)
                {
                    break;
                }

                ++lineNo;
            }
            return ceoNode;
        }
    }
}
