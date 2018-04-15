using MODELPriorityQueue.Models;
using System;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MODELPriorityQueue.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.IsLoading = true;
            if (!(await ViewModel.AttemptLogin()))
            {
                await new MessageDialog("Incorrect Credentials").ShowAsync();
            }
            else
            {
                ViewModel.NavigateToMainPage();
            }
            ViewModel.IsLoading = false;
        }

        /// <summary>
        /// Temporary for skipping authentication but logging in a manager.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LoginAsManager_Click(object sender, RoutedEventArgs e)
        {
            var managers = await Manager.Get();
            App.LoggedInUser = managers.FirstOrDefault();
            Template10.Common.BootStrapper.Current.NavigationService.Navigate(typeof(MainPage));
        }

        private async void LoginAsTechnician_Click(object sender, RoutedEventArgs e)
        {
            var technicians = await Technician.Get();
            App.LoggedInUser = technicians.FirstOrDefault();
            Template10.Common.BootStrapper.Current.NavigationService.Navigate(typeof(MainPage));
        }
    }
}
