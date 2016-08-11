using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrashDump
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Factory.StartNew(() =>
           {
               int localvalue = 10;
               int threadId = Thread.CurrentThread.ManagedThreadId;
               FileInfo fileInfo = new FileInfo(@"D:\windows-backup\tools\SysinternalsSuite\procdump.exe");
               Thread.Sleep(60 * 1000);

           });
            CreateProcessDump(@"d:\temp",@"D:\windows-backup\tools\SysinternalsSuite\procdump.exe");
           // int currentProcessId = Process.GetCurrentProcess().Id;
           // String CommandLine = @"-mp  -accepteula "+ currentProcessId+@" d:\temp\demo.dmp";
           // String FileName = ;
           // Console.WriteLine(CommandLine);

           // ProcessStartInfo psi = new ProcessStartInfo
           // {
           //     RedirectStandardOutput = true,
           //     UseShellExecute = false,
           //     CreateNoWindow = true,
           //     Arguments = CommandLine,
           //     FileName = FileName
           // };
           //var process=  Process.Start(psi);
           // Console.WriteLine("Waiting for procdump exit");
           // process.WaitForExit();
           // Console.WriteLine("Done");
        }

        /// <summary>
        /// Full memory dump using the Procdump sysinternals utility
        /// </summary>
        /// <param name="dumpFileLocation">location of the dump to be stored</param>
        /// <param name="procDumpFilePath">full path to procdump.exe </param>
        public static void CreateProcessDump(String dumpFileLocation,String procDumpFilePath)
        {
            try
            {
                int currentProcessId = Process.GetCurrentProcess().Id;
                String dumpFileName = String.Format("processdump_" + 
                                                    currentProcessId + "_" + 
                                                    DateTime.Now.Ticks+".dmp");

                String dumpFilePath = Path.Combine(dumpFileLocation, dumpFileName);
                String CommandLine = @"-mp  -accepteula " + currentProcessId + " "+dumpFilePath;

                Console.WriteLine("Input Params DumpFilePath:-{0}, ProcDumpExe:-{1}",dumpFilePath, procDumpFilePath);

                String FileName = procDumpFilePath;
                Console.WriteLine(CommandLine);

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = CommandLine,
                    FileName = FileName
                };
                Console.WriteLine("Creating the current Process Dump...");
                var process = Process.Start(psi);
                process.WaitForExit();
                Console.WriteLine("Done");

            }
            catch (Exception exp)
            {
                Console.WriteLine("Dump failed "+exp.ToString());
            }
        }
    }

    /// <summary>
    /// Dump writer
    /// </summary>
    public static class MiniDumpWriter
    {
        [Flags]
        public enum MINIDUMP_TYPE
        {
            MiniDumpNormal = 0x00000000,
            MiniDumpWithDataSegs = 0x00000001,
            MiniDumpWithFullMemory = 0x00000002,
            MiniDumpWithHandleData = 0x00000004,
            MiniDumpFilterMemory = 0x00000008,
            MiniDumpScanMemory = 0x00000010,
            MiniDumpWithUnloadedModules = 0x00000020,
            MiniDumpWithIndirectlyReferencedMemory = 0x00000040,
            MiniDumpFilterModulePaths = 0x00000080,
            MiniDumpWithProcessThreadData = 0x00000100,
            MiniDumpWithPrivateReadWriteMemory = 0x00000200,
            MiniDumpWithoutOptionalData = 0x00000400,
            MiniDumpWithFullMemoryInfo = 0x00000800,
            MiniDumpWithThreadInfo = 0x00001000,
            MiniDumpWithCodeSegs = 0x00002000
        }

        [DllImport("dbghelp.dll", EntryPoint = "MiniDumpWriteDump", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType, IntPtr expParam, IntPtr userStreamParam, IntPtr callbackParam);

        public static bool Write(MINIDUMP_TYPE options = MINIDUMP_TYPE.MiniDumpWithDataSegs | MINIDUMP_TYPE.MiniDumpWithUnloadedModules | MINIDUMP_TYPE.MiniDumpWithProcessThreadData)
        {
            string fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".dmp");
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Write))
            {
                Process currentProcess = Process.GetCurrentProcess();

                IntPtr currentProcessHandle = currentProcess.Handle;

                uint currentProcessId = (uint)currentProcess.Id;

                return MiniDumpWriteDump(currentProcessHandle, currentProcessId, fs.SafeFileHandle, (uint)options, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}
