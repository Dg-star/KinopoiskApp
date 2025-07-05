using KinopoiskApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinopoiskApp.Services.Interfaces
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetAllMovies();
    }
}
