using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinopoiskApp.Models
{
    [DebuggerDisplay("{Title} ({Year})")]
    public class Movie
    {
        [JsonProperty("filmId")]
        public int Id { get; set; }

        [JsonProperty("nameRu")]
        public string Title { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; } 

        [JsonProperty("posterUrl")]
        public string PosterUrl { get; set; }

        [JsonProperty("rating")]
        public string Rating { get; set; }

        public bool IsFavorite { get; set; }

        public override string ToString() => $"{Title} ({Year})";
    }
}
