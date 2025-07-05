using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinopoiskApp.Models;
using KinopoiskApp.Services.Interfaces;

namespace KinopoiskApp.Services
{
    public class MovieService : IMovieService
    {
        public IEnumerable<Movie> GetAllMovies()
        {
            return new List<Movie>
            {
                new Movie("Крестный отец", 1972),
                new Movie("Побег из Шоушенка", 1994),
                new Movie("Темный рыцарь", 2008),
                new Movie("Форрест Гамп", 1994),
                new Movie("Начало", 2010),
                new Movie("Матрица", 1999),
                new Movie("Список Шиндлера", 1993)
            };
        }
    }
}
