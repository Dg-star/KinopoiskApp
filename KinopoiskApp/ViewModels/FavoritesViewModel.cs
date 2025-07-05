using KinopoiskApp.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace KinopoiskApp.ViewModels
{
    public class FavoritesViewModel : BaseViewModel
    {
        public List<MovieViewModel> FavoriteMovies { get; }

        public FavoritesViewModel()
        {
            // Здесь должна быть реальная логика загрузки избранного
            FavoriteMovies = App.Movies
                .Where(m => m.IsFavorite)
                .ToList();
        }
    }
}