using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Input;

namespace JapaneseAssist.Models
{
    class ButtonCommand : ICommand
    {
        private readonly Action ExecuteDelegate;
        private readonly Func<bool> CanExecuteDelegate;
        public event EventHandler CanExecuteChanged;

        public void Execute(Object argument = null)
        {
            if(CanExecute())
                ExecuteDelegate.Invoke();
        }
        public bool CanExecute(Object argument = null)
        {
            return CanExecuteDelegate.Invoke();
        }

        public ButtonCommand(Action methodToExecute, Func<bool> executeCondition)
        {
            ExecuteDelegate = methodToExecute;
            CanExecuteDelegate = executeCondition;
        }
        public void FireCanExecuteChanged() => CanExecuteChanged.Invoke(null, new EventArgs());
    }
}
