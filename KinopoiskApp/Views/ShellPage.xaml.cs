using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace KinopoiskApp.Views
{
    public sealed partial class ShellPage : Page
    {
        public ShellPage()
        {
            InitializeComponent();
            ContentFrame.Navigate(typeof(MainPage));
        }

        private void NavView_SelectionChanged(NavigationView sender,
                                            NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItem item)
            {
                switch (item.Tag.ToString())
                {
                    case "main":
                        ContentFrame.Navigate(typeof(MainPage));
                        break;
                    case "favorites":
                        ContentFrame.Navigate(typeof(FavoritesPage));
                        break;
                }
            }
        }
    }
}