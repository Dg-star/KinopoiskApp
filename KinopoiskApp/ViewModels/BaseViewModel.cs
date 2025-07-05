using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KinopoiskApp.ViewModels
{
    /// <summary>
    /// Базовый класс для всех ViewModel в приложении.
    /// Реализует интерфейс INotifyPropertyChanged для поддержки привязки данных в XAML.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие, которое происходит при изменении значения свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Вызывает событие PropertyChanged для уведомления об изменении свойства.
        /// </summary>
        /// <param name="propertyName">Имя изменившегося свойства (автоматически подставляется благодаря CallerMemberName)</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Устанавливает значение поля и уведомляет об изменении свойства, если значение действительно изменилось.
        /// </summary>
        /// <typeparam name="T">Тип свойства</typeparam>
        /// <param name="field">Ссылка на поле свойства</param>
        /// <param name="value">Новое значение</param>
        /// <param name="propertyName">Имя свойства (автоматически подставляется)</param>
        /// <returns>True, если значение изменилось, иначе False</returns>
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            // Проверяем, действительно ли значение изменилось
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            // Устанавливаем новое значение
            field = value;

            // Уведомляем об изменении свойства
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}