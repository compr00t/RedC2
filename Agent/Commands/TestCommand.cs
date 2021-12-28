using Agent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Commands
{
    public class TestCommand : AgentCommand
    {
        public override string Name => "TestCommand";

        public override string Execute(AgentTask task)
        {
            return "Hello from Test Command!";
        }
    }
}
