using MODELPriorityQueue.Modals;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MODELPriorityQueue.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private async void AddJobButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementation will need to be changed later.
            var dialog = new AddJobDialog();
            await dialog.ShowAsync();
            await ViewModel.LoadScreenData();
        }

        private async void ViewStatsButton_Click(object sender, RoutedEventArgs e)
        {
            await new StatsDialog().ShowAsync();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SaveJob();
        }

        private void CompletedButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MarkCompleted();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NavigateToSettingsPage();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadScreenData();
        }

        private async void DeleteJobButton_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.DeleteJob();
        }
    }
}