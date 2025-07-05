using System;
using System.Windows.Input;

namespace KinopoiskApp.ViewModels
{
    /// <summary>
    /// Реализация ICommand для делегирования вызовов команд
    /// Позволяет создавать команды с логикой в ViewModel
    /// </summary>
    public class RelayCommand : ICommand
    {
        // Делегат для выполнения команды
        private readonly Action<object> _execute;
        // Делегат для проверки возможности выполнения команды
        private readonly Func<object, bool> _canExecute;
        // Событие изменения состояния команды
        private EventHandler _canExecuteChanged;

        /// <summary>
        /// Конструктор команды
        /// </summary>
        /// <param name="execute">Метод выполнения</param>
        /// <param name="canExecute">Метод проверки выполнения (опционально)</param>
        /// <exception cref="ArgumentNullException">Если execute равен null</exception>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Проверяет возможность выполнения команды
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        /// <returns>True, если команду можно выполнить</returns>
        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        /// <summary>
        /// Выполняет логику команды
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        public void Execute(object parameter) => _execute(parameter);

        /// <summary>
        /// Событие изменения состояния команды
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                _canExecuteChanged += value;
            }
            remove
            {
                _canExecuteChanged -= value;
            }
        }

        /// <summary>
        /// Вызывает уведомление об изменении состояния команды
        /// </summary>
        public void NotifyCanExecuteChanged()
        {
            _canExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}