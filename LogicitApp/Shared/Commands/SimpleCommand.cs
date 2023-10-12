using System.Windows.Input;

namespace LogicitApp.Shared.Commands
{
    public class SimpleCommand : ICommand
    {
        private Action<object?> _action;

        public SimpleCommand(Action<object?> action)
        {
            _action = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _action.Invoke(parameter);
        }
    }
}
