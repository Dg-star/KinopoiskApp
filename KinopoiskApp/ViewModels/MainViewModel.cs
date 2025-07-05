using KinopoiskApp.Models;
using KinopoiskApp.Services;
using KinopoiskApp.Services.Interfaces;
using KinopoiskApp.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace KinopoiskApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private List<MovieViewModel> _movies;

        public List<MovieViewModel> Movies
        {
            get => _movies;
            set => SetField(ref _movies, value);
        }

        public MainViewModel(IMovieService movieService)
        {
            LoadMovies(movieService);
        }

        private void LoadMovies(IMovieService movieService)
        {
            Movies = movieService.GetAllMovies()
                .Select(m => new MovieViewModel(m))
                .ToList();
        }
    }
}