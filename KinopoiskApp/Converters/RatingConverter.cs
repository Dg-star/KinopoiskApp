using Windows.UI.Xaml.Data;
using System;

namespace KinopoiskApp.Converters
{
    /// <summary>
    /// Конвертер для форматирования рейтинга фильма в удобочитаемый вид
    /// Преобразует числовые значения рейтинга в текстовый формат с визуальными элементами
    /// 
    /// Особенности:
    /// - Поддерживает multiple input types (double, float, string)
    /// - Автоматическая градация оценки (отличный/хороший/средний/плохой)
    /// - Визуализация с помощью emoji-звезды
    /// - Локализованный вывод (можно менять текстовые описания)
    /// 
    /// Использование:
    /// <TextBlock Text="{Binding Rating, Converter={StaticResource RatingConverter}}"/>
    /// </summary>
    public class RatingConverter : IValueConverter
    {
        /// <summary>
        /// Основной метод конвертации рейтинга
        /// </summary>
        /// <param name="value">Входное значение рейтинга (double, float или string)</param>
        /// <param name="targetType">Ожидаемый тип результата (игнорируется)</param>
        /// <param name="parameter">Дополнительный параметр (не используется)</param>
        /// <param name="language">Язык для локализации (не реализовано)</param>
        /// <returns>
        /// Форматированная строка вида "⭐ X.X (оценка)" или "Рейтинг отсутствует"
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return "Рейтинг отсутствует";

            // Обрабатываем разные форматы рейтинга
            return value switch
            {
                double d => FormatRating(d),
                float f => FormatRating(f),
                string s when double.TryParse(s, out var rating) => FormatRating(rating),
                _ => "Рейтинг отсутствует"
            };
        }

        /// <summary>
        /// Обратное преобразование не поддерживается
        /// </summary>
        /// <exception cref="NotImplementedException">Всегда выбрасывается при вызове</exception>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Внутренний метод форматирования числового рейтинга
        /// </summary>
        /// <param name="rating">Числовое значение рейтинга</param>
        /// <returns>
        /// Строка с отформатированным рейтингом и текстовой оценкой:
        /// - > 8.5: "отличный"
        /// - > 7:   "хороший"
        /// - > 5:   "средний"
        /// - > 0:   "плохой"
        /// - 0:     "Рейтинг отсутствует"
        /// </returns>
        private string FormatRating(double rating)
        {
            return rating switch
            {
                > 8.5 => $"⭐ {rating:F1} (отличный)",
                > 7 => $"⭐ {rating:F1} (хороший)",
                > 5 => $"⭐ {rating:F1} (средний)",
                > 0 => $"⭐ {rating:F1} (плохой)",
                _ => "Рейтинг отсутствует"
            };
        }
    }
}