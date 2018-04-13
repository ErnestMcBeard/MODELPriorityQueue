using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using MODELPriorityQueue.Models;
using System.Collections.ObjectModel;

namespace MODELPriorityQueue.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<Job> jobs;
        public ObservableCollection<Job> Jobs
        {
            get { return jobs; }
            set { Set(() => Jobs, ref jobs, value); }
        }

        private Job selectedJob;
        public Job SelectedJob
        {
            get { return selectedJob; }
            set { Set(() => SelectedJob, ref selectedJob, value); }
        }


        public void MarkCompleteion()
        {
            /*
             * The logic for this code should go something like this:
             * marks the active job complete
             * updates the back end
             * sets the new active job to the next on the queue or highest priority
             * updates the view
             */
            SelectedJob.Completed = true;
            //UPDATE BACKEND CODE HERE
            //SET ACTIVE JOB TO NEXT JOB
            //I believe the view will update whenever this changes?
        }

        public void NavigateToSettingsPage()
        {
            App.Current.NavigationService.Navigate(typeof(Views.Settings));
        }
    }
}
