using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using MODELPriorityQueue.Models;
using System.Collections.ObjectModel;
using Windows.UI.Popups;

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

        private ObservableCollection<Customer> customers;
        public ObservableCollection<Customer> Customers
        {
            get { return customers; }
            set { Set(() => Customers, ref customers, value); }
        }

        private ObservableCollection<Manager> managers;
        public ObservableCollection<Manager> Managers
        {
            get { return managers; }
            set { Set(() => Managers, ref managers, value); }
        }

        private ObservableCollection<Technician> technicians;
        public ObservableCollection<Technician> Technicians
        {
            get { return technicians; }
            set { Set(() => Technicians, ref technicians, value); }
        }

        private IUser loggedInUser;
        public IUser LoggedInUser
        {
            get { return loggedInUser; }
            set { Set(() => LoggedInUser, ref loggedInUser, value); }
        }

        private Job selectedJob;
        public Job SelectedJob
        {
            get { return selectedJob; }
            set
            {
                Set(() => SelectedJob, ref selectedJob, value);
                ExpandPropertiesForJob();
            }
        }

        private Customer selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set { Set(() => SelectedCustomer, ref selectedCustomer, value); }
        }

        private Manager selectedManager;
        public Manager SelectedManager
        {
            get { return selectedManager; }
            set { Set(() => SelectedManager, ref selectedManager, value); }
        }

        private Technician selectedTechnician;
        public Technician SelectedTechnician
        {
            get { return selectedTechnician; }
            set { Set(() => SelectedTechnician, ref selectedTechnician, value); }
        }

        public async Task LoadScreenData()
        {
            LoggedInUser = App.LoggedInUser;
            Jobs = new ObservableCollection<Job>(await Job.Get());
            Customers = new ObservableCollection<Customer>(await Customer.Get());
            Managers = new ObservableCollection<Manager>(await Manager.Get());
            Technicians = new ObservableCollection<Technician>(await Technician.Get());
        }

        public void ExpandPropertiesForJob()
        {
            if (SelectedJob != null)
            {
                SelectedCustomer = Customers?.Where(x => x.Id == SelectedJob.Customer).FirstOrDefault();
                SelectedManager = Managers?.Where(x => x.Id == SelectedJob.AssignedBy).FirstOrDefault();
                SelectedTechnician = Technicians?.Where(x => x.Id == SelectedJob.Technician).FirstOrDefault();
            }
            else
            {
                SelectedCustomer = null;
                SelectedManager = null;
                SelectedTechnician = null;
            }
        }

        public void MarkCompleted()
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

        public async Task UpdateQueueOrder()
        {
            foreach(Job currenntJob in Jobs)
            {
                //This code will only execute if the Job that is moved is moved above one that has a lower priority then it. 
                //This prevents a call from Prioritize Queue from ruining the queue order.
                if (Jobs.IndexOf(currenntJob) != Jobs.Count - 1)
                {
                    if (currenntJob.Priority > Jobs.ElementAt(Jobs.IndexOf(currenntJob) + 1).Priority)
                    {
                        currenntJob.Priority = Jobs.ElementAt(Jobs.IndexOf(currenntJob) + 1).Priority;
                    }
                }
                if(Jobs.IndexOf(currenntJob) == 0)
                {
                    //Allegedly this is how you null a Guid value
                    currenntJob.PreviousJob = Guid.Empty;
                    await currenntJob.Update();

                }
                else
                {
                    currenntJob.PreviousJob = Jobs.ElementAt(Jobs.IndexOf(currenntJob)-1).Id;
                    await currenntJob.Update();
                }
                if(Jobs.IndexOf(currenntJob) == Jobs.Count - 1)
                {
                    currenntJob.NextJob = Guid.Empty;
                    await currenntJob.Update();
                }
                else
                {
                    currenntJob.NextJob = Jobs.ElementAt(Jobs.IndexOf(currenntJob) + 1).Id;
                    await currenntJob.Update();
                }
            }
        }

        public void PrioritizeQueue()
        {
            foreach (Job currentJob in Jobs)
            {
                if (currentJob.Priority < Jobs.ElementAt(Jobs.IndexOf(currentJob) + 1).Priority)
                {
                    continue;
                }
                else
                {
                    Job temp = Jobs.ElementAt(Jobs.IndexOf(currentJob) + 1);
                    Jobs.RemoveAt(Jobs.IndexOf(currentJob) + 1);
                    Jobs.Insert((Jobs.IndexOf(currentJob) + 1), currentJob);
                    Jobs.Insert(Jobs.IndexOf(currentJob), temp);
                    Jobs.Remove(currentJob);
                }   
            }
        }

        public async Task SetPrioritiesInQueue()
        {
            foreach (Job currentJob in Jobs)
            {
                Customer currentJobsCustomer = await Customer.Get(currentJob.Customer);
                currentJob.Priority = currentJobsCustomer.Priority();
            }
        }

        public void NavigateToSettingsPage()
        {
            App.Current.NavigationService.Navigate(typeof(Views.Settings));
        }

        public async Task SaveJob()
        {
            if (SelectedJob != null)
            {
                await SelectedJob.Update();
            }
        }

        public async Task DeleteJob()
        {
            if (SelectedJob != null)
            {
                if (await SelectedJob.Delete())
                {
                    // Remove the entity from the local list
                    Jobs.Remove(SelectedJob);
                    SelectedJob = null;
                }
            }
            else
            {
                await new MessageDialog("Please select a job").ShowAsync();
            }
        }

        public async Task AssignTechnician()
        {
            if (SelectedJob != null && App.LoggedInUser.GetType() == typeof(Technician))
            {
                SelectedJob.Technician = LoggedInUser.Id;
                await SelectedJob.Update();
            }
        }
    }
}
