using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace General.Apt.App.Utility
{
    public class Proc
    {
        public static List<Process> Processes { get; private set; } = new List<Process>();

        public static Task StartProcess(string processFileName, string arguments, DataReceivedEventHandler outputEventHandler = null, DataReceivedEventHandler errorEventHandler = null, EventHandler exitedEventHandler = null)
        {
            return Task.Run(() =>
            {
                Process esrgan = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = processFileName,
                        Arguments = arguments,

                        CreateNoWindow = true,
                        UseShellExecute = false,

                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                    },
                    EnableRaisingEvents = true
                };
                esrgan.OutputDataReceived += outputEventHandler;
                esrgan.ErrorDataReceived += errorEventHandler;
                esrgan.Exited += exitedEventHandler;
                esrgan.Start();
                esrgan.BeginErrorReadLine();
                esrgan.BeginOutputReadLine();
                Processes.Add(esrgan);
                esrgan.WaitForExit();
            });
        }

        public static void CancelProcess(Process process, Action action = null, bool waitForExit = false)
        {
            if (!process.HasExited)
            {
                process.Kill();
                if (waitForExit)
                {
                    process.WaitForExit();
                }
            }
            action?.Invoke();
        }

        public static void Clear()
        {
            foreach (var process in Processes)
            {
                if (!process.HasExited)
                {
                    process.Kill();
                }
            }
        }

        [Flags]
        private enum ThreadAccess : int
        {
            SUSPEND_RESUME = (0x0002)
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        private static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        private static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool CloseHandle(IntPtr handle);

        public static void SuspendProcess(Process process)
        {
            foreach (ProcessThread pT in process.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                SuspendThread(pOpenThread);

                CloseHandle(pOpenThread);
            }
        }

        public static void ResumeProcess(Process process)
        {
            if (process.ProcessName == string.Empty)
                return;

            foreach (ProcessThread pT in process.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                int suspendCount;
                do
                {
                    suspendCount = ResumeThread(pOpenThread);
                } while (suspendCount > 0);

                CloseHandle(pOpenThread);
            }
        }
    }
}
