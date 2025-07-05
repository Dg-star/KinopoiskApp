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

        // Dependency Property для состояния "Избранное"
        public static readonly DependencyProperty IsFavoriteProperty =
            DependencyProperty.Register(
                nameof(IsFavorite),
                typeof(bool),
                typeof(FavoriteButton),
                new PropertyMetadata(false, OnIsFavoriteChanged));

        public bool IsFavorite
        {
            get => (bool)GetValue(IsFavoriteProperty);
            set => SetValue(IsFavoriteProperty, value);
        }

        private static void OnIsFavoriteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FavoriteButton button)
            {
                VisualStateManager.GoToState(button, (bool)e.NewValue ? "Favorite" : "NotFavorite", true);
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            VisualStateManager.GoToState(this, IsFavorite ? "Favorite" : "NotFavorite", false);
        }
    }
}
