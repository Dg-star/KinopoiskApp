using KinopoiskApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinopoiskApp.Services.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для работы с данными о фильмах
    /// Определяет базовые операции для получения и управления кешированными данными
    /// 
    /// Основные обязанности:
    /// - Загрузка топовых фильмов из API
    /// - Управление локальным кешем данных
    /// - Проверка актуальности кешированных данных
    /// 
    /// Рекомендации по реализации:
    /// - Все методы должны быть потокобезопасны
    /// - Должна быть обработка сетевых ошибок
    /// - Кеш должен автоматически обновляться при устаревании
    /// </summary>
    public interface IMovieService
    {
        /// <summary>
        /// Получает список топовых фильмов
        /// Сначала проверяет локальный кеш, затем загружает из API при необходимости
        /// </summary>
        /// <returns>
        /// Task<List<Movie>> - список фильмов, отсортированный по рейтингу
        /// В случае ошибки возвращает пустой список
        /// </returns>
        Task<List<Movie>> GetTopMoviesAsync();

        /// <summary>
        /// Очищает локальный кеш фильмов
        /// Используется для принудительного обновления данных
        /// </summary>
        /// <returns>Task, завершающийся когда кеш очищен</returns>
        Task ClearCacheAsync();

        /// <summary>
        /// Проверяет валидность кешированных данных
        /// </summary>
        /// <returns>
        /// Task<bool> - true если кеш существует и актуален,
        /// false если кеш устарел или отсутствует
        /// </returns>
        Task<bool> IsCacheValidAsync();
    }
}
