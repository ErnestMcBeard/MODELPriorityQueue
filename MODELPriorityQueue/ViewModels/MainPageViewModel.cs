using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using MODELPriorityQueue.Models;

namespace MODELPriorityQueue.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        //The active job and customer are the ones that are currently loaded onto the view
        //I assume that the activeJob and the activeCustomer information will have
        //to be stored by some kind of field variable that gets set by user clicks on the
        //job feed.
        private Job activeJob;
        private Customer activeCustomer;

        public void markCompleteion()
        {
            /*
             * The logic for this code should go something like this:
             * marks the active job complete
             * updates the back end
             * sets the new active job to the next on the queue or highest priority
             * updates the view
             */
            activeJob.Completed = true;
            //UPDATE BACKEND CODE HERE
            //SET ACTIVE JOB TO NEXT JOB
            //I believe the view will update whenever this changes?
        }

    }
}
