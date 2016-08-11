using Microsoft.Diagnostics.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumpThreadStacks
{
    public class Program
    {

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Enter valid .Net Process name");
                return;
            }
            String processName = args[1];
            var process = Process.GetProcessesByName("processName");
            if (process == null)
            {
                Console.WriteLine($"Process '{processName}' not found. Enter valid .Net Process name");
                return;
            }
            int pid = process[0].Id;
            using (DataTarget dataTarget = DataTarget.AttachToProcess(pid,
                (uint)TimeSpan.FromSeconds(5).TotalMilliseconds))
            {
                string dacLocation = dataTarget.ClrVersions[0].TryGetDacLocation();
                ClrRuntime runtime = dataTarget.CreateRuntime(dacLocation);
                DumpThreadStack(runtime);
            }
            Console.ReadLine();
        }

        public static void DumpThreadStack(ClrRuntime runtime)
        {
            foreach (ClrThread thread in runtime.Threads)
            {
                Console.WriteLine("ThreadID: {0:X}", thread.OSThreadId);
                Console.WriteLine("Callstack:");

                if (thread.StackTrace != null)
                {


                    foreach (ClrStackFrame frame in thread.StackTrace)
                        Console.WriteLine("{0,12:X} {1,12:X} {2}", frame.InstructionPointer, frame.StackPointer, frame.DisplayString);
                }

                Console.WriteLine();
            }
        }
    }
}
