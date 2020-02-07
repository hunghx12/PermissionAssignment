using System.Collections.Generic;
using System.IO;
using System.Reflection;
using PermissionsAssignment.Models.CommandAggregates;

namespace PermissionsAssignment.Infrastructure
{
    public class CommandAndQueryRepository
    {
        private static CommandAndQueryRepository _instance;

        public string InputUri { get; private set; }

        private CommandAndQueryRepository(string inputUri)
        {
            InputUri = inputUri;
        }

        public static CommandAndQueryRepository GetInstance(string inputUri)
        {
            return _instance ?? (_instance = new CommandAndQueryRepository(inputUri));
        }

        private string[] ReadLines()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), InputUri);
            string[] content = File.ReadAllLines(path);
            return content;
        }

        public IEnumerable<ICommand> GetCommands()
        {
            var commands = new List<ICommand>();
            foreach (var line in ReadLines())
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                ICommand command = null;
                switch (line.Split()[0])
                {
                    case "ADD":
                        command = new AddCommand(line);
                        break;
                    case "REMOVE":
                        command = new RemoveCommand(line);
                        break;
                    case "QUERY":
                        command = new QueryCommand(line);
                        break;
                }
                if (command != null)
                    commands.Add(command);
            }
            return commands;
        }
    }
}
