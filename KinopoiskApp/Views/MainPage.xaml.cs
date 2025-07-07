using KinopoiskApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;

namespace KinopoiskApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; }

        public MainPage()
        {
            ViewModel = App.MainVM;

            this.InitializeComponent();
            Debug.WriteLine("[DEBUG] MainPage инициализирована");
            this.DataContext = ViewModel;
        }
    }
}
