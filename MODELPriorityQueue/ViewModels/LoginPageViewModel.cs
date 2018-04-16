using MODELPriorityQueue.Models;
using System.Threading.Tasks;
using Template10.Mvvm;
using System;

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

        //internal async Task CreateUser()
        //{
        //    Technician t = new Technician()
        //    {
        //        FirstName = "Jeremy",
        //        LastName = "Krouse",
        //        Password = "pancakes",
        //        Username = "jdaddy",
        //        StartDate = DateTimeOffset.Now
        //    };

        //    await t.Post();
        //}

        /// <summary>
        /// Tries to login into the system with the values stored in Username and Password.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AttemptLogin()
        {
            IUser potentialAccount = await Manager.Get("where Username eq " + Username);
            if (potentialAccount == default(Manager))
            {
                potentialAccount = await Technician.Get("where Username eq " + Username);
                if (potentialAccount == default(Technician))
                    return false;
            }
            App.LoggedInUser = potentialAccount;
            return potentialAccount.Password == Password;
        }

        public void NavigateToMainPage()
        {
            App.Current.NavigationService.Navigate(typeof(Views.MainPage));
        }
    }
}
