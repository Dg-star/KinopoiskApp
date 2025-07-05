using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinopoiskApp.Models
{
    /// <summary>
    /// Основная модель данных, представляющая информацию о фильме
    /// Соответствует структуре данных API Kinopoisk Unofficial
    /// 
    /// Особенности:
    /// - Поддержка JSON сериализации/десериализации через Newtonsoft.Json
    /// - Оптимизирован для отображения в Debug режиме (DebuggerDisplay)
    /// - Реализует удобное строковое представление через ToString()
    /// 
    /// Использование:
    /// - Как DTO для API запросов
    /// - Как модель данных в ViewModels
    /// - Для хранения в кеше и локальной БД
    /// </summary>
    [DebuggerDisplay("{Title} ({Year})")]
    public class Movie
    {
        /// <summary>
        /// Уникальный идентификатор фильма в системе Kinopoisk
        /// Маппится на поле 'filmId' в JSON API
        /// </summary>
        [JsonProperty("filmId")]
        public int Id { get; set; }

        /// <summary>
        /// Название фильма на русском языке
        /// Маппится на поле 'nameRu' в JSON API
        /// Может быть null для некоторых фильмов
        /// </summary>
        [JsonProperty("nameRu")]
        public string Title { get; set; }

        /// <summary>
        /// Год выпуска фильма в виде строки
        /// Маппится на поле 'year' в JSON API
        /// Формат: "YYYY" или "YYYY-YYYY" для сериалов
        /// </summary>
        [JsonProperty("year")]
        public string Year { get; set; }

        /// <summary>
        /// URL постера фильма
        /// Маппится на поле 'posterUrl' в JSON API
        /// Может быть null или содержать относительный URL
        /// </summary>
        [JsonProperty("posterUrl")]
        public string PosterUrl { get; set; }

        /// <summary>
        /// Рейтинг фильма в виде строки
        /// Маппится на поле 'rating' в JSON API
        /// Формат: "X.Y" или null если рейтинг отсутствует
        /// </summary>
        [JsonProperty("rating")]
        public string Rating { get; set; }

        /// <summary>
        /// Флаг, указывающий что фильм добавлен в избранное
        /// Не является частью API, используется только локально
        /// </summary>
        public bool IsFavorite { get; set; }

        /// <summary>
        /// Возвращает строковое представление фильма в формате "Название (Год)"
        /// </summary>
        public override string ToString() => $"{Title} ({Year})";
    }
}
