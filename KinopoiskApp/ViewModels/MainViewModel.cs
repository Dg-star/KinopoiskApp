using KinopoiskApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinopoiskApp.ViewModels
{
    public class MainViewModel
    {
        private readonly IMovieService _movieService;

        public List<MovieViewModel> Movies { get; }

        public MainViewModel(IMovieService movieService)
        {
            _movieService = movieService;
            Movies = _movieService.GetAllMovies()
                .Select(movie => new MovieViewModel(movie))
                .ToList();
        }
    }
}
