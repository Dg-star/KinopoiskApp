using KinopoiskApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;

namespace KinopoiskApp.Views
{
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
