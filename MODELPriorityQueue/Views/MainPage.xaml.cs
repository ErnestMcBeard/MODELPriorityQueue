using MODELPriorityQueue.Modals;
using MODELPriorityQueue.Models;
using MODELPriorityQueue.ViewModels;
using System;
using Template10.Common;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace MODELPriorityQueue.Views
{
    public sealed partial class MainPage : Page
    {


        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            JobsList.ItemsSource = ViewModel.Jobs;

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

        private void DeleteJobButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        //QUEUE STUFF
        private TypedEventHandler<ListViewBase, ContainerContentChangingEventArgs> del;

        private void JobsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        void JobsList_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            QueueJob queueJob = args.ItemContainer.ContentTemplateRoot as QueueJob;

            if(args.InRecycleQueue == true)
            {
                queueJob.ClearData();
            }
            else if(args.Phase == 0)
            {
                queueJob.ShowPlaceholder(args.Item as Job);
                args.RegisterUpdateCallback(ContainerContentChangingDelegate);
            }
            else if(args.Phase == 1)
            {
                queueJob.ShowTitle();
                args.RegisterUpdateCallback(ContainerContentChangingDelegate);
            }
            else if(args.Phase == 2)
            {
                queueJob.ShowCategory();
                queueJob.ShowImage();
            }

            args.Handled = true;

        }

        private TypedEventHandler<ListViewBase, ContainerContentChangingEventArgs> ContainerContentChangingDelegate
        {
            get
            {
                if (del == null)
                {
                    del = new TypedEventHandler<ListViewBase, ContainerContentChangingEventArgs>(JobsList_ContainerContentChanging);
                }
                return del;
            }
        }

    }
}