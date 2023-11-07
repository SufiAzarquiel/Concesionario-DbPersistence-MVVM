using System;
using System.Windows.Input;

namespace Concesionario_DbPersistence_MVVM.viewmodel
{
    public class Command : ICommand
    {
        readonly Action<object> pendingAction;

        public Command(Action<object> execute) : this(execute, null)
        {
        }
        public Command(Action<object> execute, Predicate<object> canExecute)
        {
            pendingAction = execute;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            pendingAction(parameter);
        }
    }
}
