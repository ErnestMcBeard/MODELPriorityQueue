using System.Threading.Tasks;
using Template10.Mvvm;

namespace MODELPriorityQueue.ViewModels
{
    class LoginPageViewModel : ViewModelBase
    {
        private string username;
        public string Username
        {
            get { return username; }
            set { Set(() => Username, ref username, value); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { Set(() => Password, ref password, value); }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { Set(() => IsLoading, ref isLoading, value); }
        }

        public LoginPageViewModel()
        {

        }

        /// <summary>
        /// Tries to login into the system with the values stored in Username and Password.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AttemptLogin()
        {
            return true;
        }

        public void NavigateToMainPage()
        {
            App.Current.NavigationService.Navigate(typeof(Views.MainPage));
        }
    }
}
