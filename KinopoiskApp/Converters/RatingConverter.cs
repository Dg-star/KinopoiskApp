using Windows.UI.Xaml.Data;
using System;

namespace KinopoiskApp.Converters
{
    public class RatingConverter : IValueConverter
    {
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

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

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