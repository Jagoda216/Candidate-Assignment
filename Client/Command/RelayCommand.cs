using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Command
{
    /// <summary>
    /// A command implementation that delegates execution and condition-checking logic
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExcute;

        /// <summary>
        /// Initializes a new instance of RellayCommand
        /// </summary>
        /// <param name="execute">Action to execute</param>
        public RelayCommand(Action execute) : this(execute, null) { }

        /// <summary>
        /// Initializes a new instance of RellayCommand
        /// </summary>
        /// <param name="execute">Action to execute</param>
        /// <param name="canExcute">Function that determines if command can be executed</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RelayCommand(Action execute, Func<bool> canExcute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExcute = canExcute;
        }
        /// <summary>
        /// Determines if action can be executed
        /// </summary>
        /// <param name="parameter">Data used by command</param>
        /// <returns>True if command can be executed</returns>
        public bool CanExecute(object parameter)
        { 
            return _canExcute(); 
        }

        /// <summary>
        /// Execute command's action
        /// </summary>
        /// <param name="parameter">Data used by command</param>
        public void Execute(object parameter)
        {
            _execute();
        }

        /// <summary>
        /// Occurs when changes in command's ability to execute should be reevaluated.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
	}
}
