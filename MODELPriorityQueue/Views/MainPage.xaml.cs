using MODELPriorityQueue.Modals;
using MODELPriorityQueue.Models;
using MODELPriorityQueue.ViewModels;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MODELPriorityQueue.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            base.OnNavigatedTo(e);
        }

        private async void AddJobButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Implementation will need to be changed later.
            var dialog = new AddJobDialog();
            await dialog.ShowAsync();
            await ViewModel.LoadScreenData();
        }

        private async void ViewStatsButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await new StatsDialog().ShowAsync();
        }

        private void SaveButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Auto generated method This is where the function call to the back end will go. I assume
        }

        private void CompletedButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            
            //This somehow needs to reference the MainPageViewModel   
        }

        private void SettingsButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.NavigateToSettingsPage();
        }

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.LoadScreenData();
        }

        private async void DeleteJobButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.DeleteJob();
        }
    }
}