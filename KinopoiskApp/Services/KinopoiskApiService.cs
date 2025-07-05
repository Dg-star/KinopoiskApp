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
    public class KinopoiskApiService
    {
        private const string ApiUrl = "https://kinopoiskapiunofficial.tech/api/v2.2/films/";
        private const string ApiKey = "e7534db3-388a-487b-bc0a-14ed9e1d4be5";
        private const string CacheFileName = "movies_cache.json";

        private readonly HttpClient _client;

        public KinopoiskApiService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("X-API-KEY", ApiKey);
        }

        public async Task<List<Movie>> GetTopMoviesAsync(bool forceRefresh = false)
        {
            try
            {
                // Если не требуется принудительное обновление, пробуем загрузить из кеша
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

                // Сохраняем в кеш (даже если список пустой)
                await SaveToCache(freshMovies);

                return freshMovies;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Ошибка при загрузке фильмов: {ex.Message}");
                return new List<Movie>();
            }
        }

        private async Task<List<Movie>> LoadFromApi()
        {
            var response = await _client.GetAsync($"{ApiUrl}top?type=TOP_100_POPULAR_FILMS");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse>(content);

            return result?.Films ?? new List<Movie>();
        }

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

        private class ApiResponse
        {
            [JsonProperty("films")]
            public List<Movie> Films { get; set; }
        }
    }
}