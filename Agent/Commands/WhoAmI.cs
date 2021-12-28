using Agent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Commands
{
    public class WhoAmI : AgentCommand
    {
        public override string Name => "whoami";

        public override string Execute(AgentTask task)
        {
            var identity = WindowsIdentity.GetCurrent();
            return identity.Name;
        }
    }
}
