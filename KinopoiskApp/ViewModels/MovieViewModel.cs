using KinopoiskApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

namespace KinopoiskApp.ViewModels
{
    public class MovieViewModel : BaseViewModel
    {
        private bool _isFavorite;
        public bool IsFavorite
        {
            get => _isFavorite;
            set => SetField(ref _isFavorite, value);
        }
        public string Title { get; }
        public string Year { get; }
        public string TitleWithYear => $"{Title} ({Year})";
        public string PosterUrl { get; }
        public string Rating { get; }
        public MovieViewModel(Movie movie)
        {
            Title = movie.Title ?? "Без названия";
            Year = movie.Year ?? "N/A";
            Rating = !string.IsNullOrEmpty(movie.Rating) ? $"Рейтинг: {movie.Rating}" : "Рейтинг отсутствует";
            PosterUrl = movie.PosterUrl;
        }
    }
}
