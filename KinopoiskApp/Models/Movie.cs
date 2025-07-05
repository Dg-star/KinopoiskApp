using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinopoiskApp.Models
{
    public class Movie
    {
        public string Title { get; }
        public int Year { get; }

        public Movie(string title, int year)
        {
            Title = title; Year = year;
        }
    }
}
