using KinopoiskApp.Models;
using KinopoiskApp.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KinopoiskApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly KinopoiskApiService _apiService;
        private List<Movie> _movies;
        private bool _isLoading;
        private string _dataSource;

        public List<Movie> Movies
        {
            get => _movies;
            set => SetField(ref _movies, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetField(ref _isLoading, value);
        }

        public string DataSource
        {
            get => _dataSource;
            set => SetField(ref _dataSource, value);
        }

        public ICommand RefreshCommand { get; }

        public MainViewModel(KinopoiskApiService apiService)
        {
            _apiService = apiService;
            RefreshCommand = new RelayCommand(async _ => await LoadMovies(true));
            LoadMovies();
        }

        private async Task LoadMovies(bool forceRefresh = false)
        {
            IsLoading = true;
            DataSource = forceRefresh ? "Загрузка новых данных..." : "Проверка кеша...";

            var movies = await _apiService.GetTopMoviesAsync(forceRefresh);
            Movies = movies;

            DataSource = forceRefresh ? "Данные из API" : "Данные из кеша";
            IsLoading = false;

            Debug.WriteLine($"Загружено фильмов: {movies?.Count ?? 0}");
        }
    }
}