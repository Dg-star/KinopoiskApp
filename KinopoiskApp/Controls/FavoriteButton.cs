/// <summary>
/// Кастомный кнопочный контрол для добавления/удаления фильмов в избранное
/// Реализует визуальную индикацию состояния через анимации и диалоговые сообщения
/// 
/// Особенности:
/// - Поддерживает двустороннюю привязку через DependencyProperty
/// - Автоматически переключает визуальные состояния
/// - Показывает confirmation-диалог при добавлении
/// - Совместим с стандартными стилями Button
/// 
/// Использование:
/// <FavoriteButton IsFavorite="{Binding IsFavorite, Mode=TwoWay}"/>
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KinopoiskApp.Controls
{
    public sealed class FavoriteButton : Button
    {
        /// <summary>
        /// Инициализирует кнопку и подписывается на события
        /// Устанавливает DefaultStyleKey и обработчик клика
        /// </summary>
        public FavoriteButton()
        {
            DefaultStyleKey = typeof(FavoriteButton);
            Click += (s, e) =>
            {
                IsFavorite = !IsFavorite;
                if (IsFavorite)
                    ShowAddedToFavoritesMessage();
            };
        }

        /// <summary>
        /// Показывает диалоговое окно подтверждения добавления в избранное
        /// </summary>
        private async void ShowAddedToFavoritesMessage()
        {
            var dialog = new ContentDialog()
            {
                Title = "Добавлено в избранное",
                Content = "Фильм добавлен в вашу коллекцию",
                CloseButtonText = "OK"
            };
            await dialog.ShowAsync();
        }

        /// <summary>
        /// DependencyProperty для состояния "Избранное"
        /// </summary>я
        public static readonly DependencyProperty IsFavoriteProperty =
            DependencyProperty.Register(
                nameof(IsFavorite),
                typeof(bool),
                typeof(FavoriteButton),
                new PropertyMetadata(false, OnIsFavoriteChanged));

        /// <summary>
        /// Получает или устанавливает состояние избранного
        /// При установке автоматически обновляет визуальное состояние
        /// </summary>
        public bool IsFavorite
        {
            get => (bool)GetValue(IsFavoriteProperty);
            set => SetValue(IsFavoriteProperty, value);
        }

        /// <summary>
        /// Обработчик изменения состояния IsFavorite
        /// Автоматически переключает визуальные состояния
        /// </summary>
        private static void OnIsFavoriteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FavoriteButton button)
            {
                VisualStateManager.GoToState(button, (bool)e.NewValue ? "Favorite" : "NotFavorite", true);
            }
        }

        /// <summary>
        /// Применяет шаблон и инициализирует начальное состояние
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            VisualStateManager.GoToState(this, IsFavorite ? "Favorite" : "NotFavorite", false);
        }
    }
}
