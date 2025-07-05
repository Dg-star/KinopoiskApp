using KinopoiskApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

namespace KinopoiskApp.ViewModels
{
    public class MovieViewModel
    {
        public string Title { get; }
        public int Year { get; }
        public string TitleWithYear => $"{Title} ({Year})";

        public MovieViewModel(Movie movie)
        {
            Title = movie.Title;
            Year = movie.Year;
        }
    }
}
