using KinopoiskApp.Services;
using KinopoiskApp.ViewModels;
using KinopoiskApp.Views;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace KinopoiskApp
{
    public sealed partial class App : Application
    {
        
        public static KinopoiskApiService ApiService { get; private set; }
        public static MainViewModel MainVM { get; private set; }
        public static List<MovieViewModel> Movies { get; set; }

        public App()
        {
            this.InitializeComponent();
            InitializeServices();
        }

        private void InitializeServices()
        {
            
            ApiService = new KinopoiskApiService();
            MainVM = new MainViewModel(ApiService); 

            Movies = new List<MovieViewModel>();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                Window.Current.Content = new ShellPage();
            }

            Window.Current.Activate();
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception($"Failed to load page '{e.SourcePageType.FullName}'.");
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}