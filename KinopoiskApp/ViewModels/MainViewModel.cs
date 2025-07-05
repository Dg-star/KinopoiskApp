using KinopoiskApp.Models;
using KinopoiskApp.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KinopoiskApp.ViewModels
{
    /// <summary>
    /// ViewModel для главного окна/страницы приложения
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        // Сервис для работы с API Кинопоиска
        private readonly KinopoiskApiService _apiService;

        // Поля для хранения данных
        private List<Movie> _movies;
        private bool _isLoading;
        private string _dataSource;

        /// <summary>
        /// Список фильмов для отображения
        /// </summary>
        public List<Movie> Movies
        {
            get => _movies;
            set => SetField(ref _movies, value);
        }

        /// <summary>
        /// Флаг загрузки данных
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            set => SetField(ref _isLoading, value);
        }

        /// <summary>
        /// Источник данных (кеш/API)
        /// </summary>
        public string DataSource
        {
            get => _dataSource;
            set => SetField(ref _dataSource, value);
        }

        /// <summary>
        /// Команда для обновления списка фильмов
        /// </summary>
        public ICommand RefreshCommand { get; }

        /// <summary>
        /// Конструктор ViewModel
        /// </summary>
        /// <param name="apiService">Сервис для работы с API (внедрение зависимости)</param>
        public MainViewModel(KinopoiskApiService apiService)
        {
            _apiService = apiService;

            // Инициализация команды обновления с принудительным обновлением
            RefreshCommand = new RelayCommand(async _ => await LoadMovies(true));

            // Первоначальная загрузка данных
            LoadMovies();
        }

        /// <summary>
        /// Загрузка списка фильмов
        /// </summary>
        /// <param name="forceRefresh">Принудительное обновление (мимо кеша)</param>
        private async Task LoadMovies(bool forceRefresh = false)
        {
            // Устанавливаем флаг загрузки
            IsLoading = true;
            DataSource = forceRefresh ? "Загрузка новых данных..." : "Проверка кеша...";

            try
            {
                // Получаем фильмы из API (с учетом кеширования)
                var movies = await _apiService.GetTopMoviesAsync(forceRefresh);
                Movies = movies;

                // Обновляем информацию об источнике данных
                DataSource = forceRefresh ? "Данные из API" : "Данные из кеша";

                // Логируем результат
                Debug.WriteLine($"Загружено фильмов: {movies?.Count ?? 0}");
            }
            finally
            {
                // В любом случае снимаем флаг загрузки
                IsLoading = false;
            }
        }
    }
}