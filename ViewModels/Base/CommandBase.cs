using System;
using System.Windows.Input;

namespace General.Apt.App.ViewModels.Base
{
    public class CommandBase : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExcute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public CommandBase(Action<object> execute, Func<object, bool> canExcute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExcute = canExcute;
        }

        public bool CanExecute(object parameter) => _canExcute == null || _canExcute(parameter);

        public void Execute(object parameter) => _execute(parameter);
    }
}
