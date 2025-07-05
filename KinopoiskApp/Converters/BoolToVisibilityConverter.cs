using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace KinopoiskApp.Converters
{
    /// <summary>
    /// Конвертер для преобразования bool в Visibility (с инверсией значения)
    /// Реализует обратную логику стандартного BoolToVisibility - false становится Visible
    /// 
    /// Особенности:
    /// - Оптимизирован для работы с привязками в UI
    /// - Поддерживает только одностороннюю конвертацию (ConvertBack не реализован)
    /// - Автоматически обрабатывает null-значения как false
    /// 
    /// Использование в XAML:
    /// <TextBlock Visibility="{Binding IsBusy, Converter={StaticResource BoolToVisibilityConverter}}"/>
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Преобразует bool значение в Visibility (с инверсией)
        /// </summary>
        /// <param name="value">Входное значение (ожидается bool)</param>
        /// <param name="targetType">Тип цели (игнорируется)</param>
        /// <param name="parameter">Параметр конвертера (не используется)</param>
        /// <param name="language">Языковой код (не используется)</param>
        /// <returns>
        /// Visibility.Visible если value == false, 
        /// Visibility.Collapsed если value == true,
        /// Visibility.Visible если value == null
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value is bool b && b) ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>
        /// Обратное преобразование не поддерживается
        /// </summary>
        /// <exception cref="NotImplementedException">Всегда выбрасывает исключение</exception>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}