using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamServer.Services;

namespace TeamServer.UnitTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IListenerService, ListenerService>();
            services.AddSingleton<IAgentService, AgentService>();
        }
    }
}
