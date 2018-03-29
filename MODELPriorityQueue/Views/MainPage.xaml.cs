using MODELPriorityQueue.Modals;
using System;
using Windows.UI.Xaml.Controls;

namespace MODELPriorityQueue.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private async void AddJobButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Implementation will need to be changed later.
            var dialog = new AddJobDialog();
            await dialog.ShowAsync();

        }

        private async void ViewStatsButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await new StatsDialog().ShowAsync();
        }
    }
}