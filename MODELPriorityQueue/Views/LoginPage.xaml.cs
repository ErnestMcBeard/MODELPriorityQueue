using System;
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
    }
}
