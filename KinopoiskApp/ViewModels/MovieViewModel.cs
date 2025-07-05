using KinopoiskApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

namespace KinopoiskApp.ViewModels
{
    /// <summary>
    /// ViewModel для представления информации о фильме
    /// Обеспечивает удобное отображение данных фильма в UI
    /// </summary>
    public class MovieViewModel : BaseViewModel
    {
        // Приватное поле для хранения состояния "Избранное"
        private bool _isFavorite;

        /// <summary>
        /// Флаг, указывающий находится ли фильм в избранном
        /// </summary>
        public bool IsFavorite
        {
            get => _isFavorite;
            set => SetField(ref _isFavorite, value); // Уведомление об изменении через BaseViewModel
        }

        /// <summary>
        /// Название фильма (только для чтения)
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Год выпуска фильма (только для чтения)
        /// </summary>
        public string Year { get; }

        /// <summary>
        /// Комбинированное название с годом в формате "Название (Год)"
        /// </summary>
        public string TitleWithYear => $"{Title} ({Year})";

        /// <summary>
        /// URL постера фильма (только для чтения)
        /// </summary>
        public string PosterUrl { get; }

        /// <summary>
        /// Форматированная строка с рейтингом фильма
        /// </summary>
        public string Rating { get; }

        /// <summary>
        /// Конструктор, инициализирующий ViewModel на основе модели Movie
        /// </summary>
        /// <param name="movie">Модель данных фильма</param>
        public MovieViewModel(Movie movie)
        {
            // Инициализация свойств с обработкой возможных null-значений
            Title = movie.Title ?? "Без названия";
            Year = movie.Year ?? "N/A";

            // Форматирование строки рейтинга
            Rating = !string.IsNullOrEmpty(movie.Rating)
                ? $"Рейтинг: {movie.Rating}"
                : "Рейтинг отсутствует";

            PosterUrl = movie.PosterUrl;
        }
    }
}