using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using KinopoiskApp.Models;
using Newtonsoft.Json;
using Windows.Storage;

namespace KinopoiskApp.Services
{
    /// <summary>
    /// Сервис для работы с API Kinopoisk Unofficial
    /// Обеспечивает загрузку и кеширование данных о фильмах
    /// 
    /// Основные функции:
    /// - Получение списка популярных фильмов
    /// - Автоматическое кеширование данных
    /// - Поддержка принудительного обновления
    /// - Обработка сетевых ошибок
    /// 
    /// Особенности реализации:
    /// - Использует HttpClient для запросов
    /// - Хранит кеш в локальном хранилище приложения
    /// - Логирует ключевые события
    /// </summary>
    public class KinopoiskApiService
    {
        /// <summary>
        /// Базовый URL API Kinopoisk Unofficial
        /// </summary>
        private const string ApiUrl = "https://kinopoiskapiunofficial.tech/api/v2.2/films/";

        /// <summary>
        /// API ключ для аутентификации
        /// </summary>
        private const string ApiKey = "e7534db3-388a-487b-bc0a-14ed9e1d4be5";

        /// <summary>
        /// Имя файла для хранения кеша
        /// </summary>
        private const string CacheFileName = "movies_cache.json";

        private readonly HttpClient _client;

        /// <summary>
        /// Инициализирует новый экземпляр сервиса
        /// Настраивает HttpClient с необходимыми заголовками
        /// </summary>
        public KinopoiskApiService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("X-API-KEY", ApiKey);
        }

        /// <summary>
        /// Получает список топовых фильмов
        /// </summary>
        /// <param name="forceRefresh">Принудительно обновить данные из API, игнорируя кеш</param>
        /// <returns>
        /// Список фильмов. В случае ошибки возвращает пустой список.
        /// Приоритет загрузки: API (если forceRefresh=true или нет валидного кеша) → Локальный кеш
        /// </returns>
        public async Task<List<Movie>> GetTopMoviesAsync(bool forceRefresh = false)
        {
            try
            {
                if (!forceRefresh)
                {
                    var cachedMovies = await LoadFromCache();
                    if (cachedMovies != null && cachedMovies.Count > 0)
                    {
                        Debug.WriteLine("[CACHE] Используем данные из кеша");
                        return cachedMovies;
                    }
                }

                Debug.WriteLine(forceRefresh
                    ? "[CACHE] Принудительное обновление данных из API"
                    : "[CACHE] Данные в кеше отсутствуют, загружаем из API");

                var freshMovies = await LoadFromApi();
                await SaveToCache(freshMovies);
                return freshMovies;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Ошибка при загрузке фильмов: {ex.Message}");
                return new List<Movie>();
            }
        }

        /// <summary>
        /// Загружает данные о фильмах из API
        /// </summary>
        /// <returns>Список фильмов или null при ошибке</returns>
        private async Task<List<Movie>> LoadFromApi()
        {
            var response = await _client.GetAsync($"{ApiUrl}top?type=TOP_100_POPULAR_FILMS");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse>(content);

            return result?.Films ?? new List<Movie>();
        }

        /// <summary>
        /// Загружает данные из локального кеша
        /// </summary>
        /// <returns>Список фильмов из кеша или null если кеш не существует/невалиден</returns>
        private async Task<List<Movie>> LoadFromCache()
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.TryGetItemAsync(CacheFileName);
                if (file == null) return null;

                var json = await FileIO.ReadTextAsync(file as StorageFile);
                return JsonConvert.DeserializeObject<List<Movie>>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CACHE ERROR] Ошибка чтения кеша: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Сохраняет список фильмов в локальный кеш
        /// </summary>
        /// <param name="movies">Список фильмов для сохранения</param>
        private async Task SaveToCache(List<Movie> movies)
        {
            try
            {
                var json = JsonConvert.SerializeObject(movies);
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                    CacheFileName, CreationCollisionOption.ReplaceExisting);

                await FileIO.WriteTextAsync(file, json);
                Debug.WriteLine($"[CACHE] Данные сохранены в кеш. Фильмов: {movies.Count}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CACHE ERROR] Ошибка сохранения кеша: {ex.Message}");
            }
        }

        /// <summary>
        /// Внутренний класс для десериализации ответа API
        /// </summary>
        private class ApiResponse
        {
            [JsonProperty("films")]
            public List<Movie> Films { get; set; }
        }
    }
}