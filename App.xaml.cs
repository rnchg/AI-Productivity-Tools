using General.Apt.App.Utility;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace General.Apt.App
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            var processes = System.Diagnostics.Process.GetProcessesByName(processName);
            if (processes.Length > 1)
            {
                Dialog.ShowErrorDialog("程序已运行，不能再次打开！");
                Environment.Exit(1);
            }
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Setting.GetSetting();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Setting.SetSetting();
            base.OnExit(e);
        }

        private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var exception = e.Exception;
            ExceptionHandler(exception.GetBaseException());
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            ExceptionHandler(exception.GetBaseException());
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var exception = e.Exception;
            ExceptionHandler(exception.GetBaseException());
        }

        private void ExceptionHandler(Exception exception)
        {
            Dialog.ShowErrorDialog(exception.Message);
            Proc.Clear();
        }
    }
}
