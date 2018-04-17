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
            //This currently returns the entire list of Jobs back regardless.
            Jobs = new ObservableCollection<Job>(await Job.Get());
            //This is an inefficient, but rudimentary way of making sure only uncompleted jobs are loaded back.
            ObservableCollection<Job> temp = new ObservableCollection<Job>();
            foreach(Job job in Jobs)
            {
                if (!job.Completed)
                {
                    temp.Add(job);
                }
            }
            Jobs = temp;
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

        public async Task MarkCompleted()
        {
            if (SelectedJob != null)
            {
                //Mark for completion
                SelectedJob.Completed = true;
                //Set the finish time
                SelectedJob.Finished = DateTimeOffset.Now;

                /*if(Jobs.IndexOf(SelectedJob) != 0)
                {
                    //Change the Guid reference of the pervious job in the queue to the next job after removal.
                    Jobs.ElementAt(Jobs.IndexOf(SelectedJob) - 1).NextJob = SelectedJob.NextJob;
                    await Jobs.ElementAt(Jobs.IndexOf(SelectedJob) - 1).Update();
                }
                if(Jobs.IndexOf(SelectedJob) != Jobs.Count-1)
                {
                    //Change the Guid reference of the next job in the queue to the previous job after removal.
                    Jobs.ElementAt(Jobs.IndexOf(SelectedJob) + 1).PreviousJob = SelectedJob.PreviousJob;
                    await Jobs.ElementAt(Jobs.IndexOf(SelectedJob) + 1).Update();
                }
                */
                await SelectedJob.Update();

                Jobs.Remove(SelectedJob);
                SelectedJob = null;
            }
            else
            {
                await new MessageDialog("Please select a job").ShowAsync();
            }

        }

        public async Task UpdateQueueOrder()
        {
            foreach (Job currenntJob in Jobs)
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
                }
                else
                {
                    currenntJob.PreviousJob = Jobs.ElementAt(Jobs.IndexOf(currenntJob)-1).Id;
                }
                if(Jobs.IndexOf(currenntJob) == Jobs.Count - 1)
                {
                    currenntJob.NextJob = Guid.Empty;
                }
                else
                {
                    currenntJob.NextJob = Jobs.ElementAt(Jobs.IndexOf(currenntJob) + 1).Id;
                }

                await currenntJob.Update();
            }
        }

        /*Dead Code
        public void PrioritizeQueue()
        {
            Collection<Job> temp = new Collection<Job>();
            Job highestPriority = Jobs.ElementAt(0);
            int i = 0;
            while(Jobs.Count != 0)
            {
                if(i == Jobs.Count - 1)
                {
                    temp.Add(highestPriority);
                    Jobs.Remove(highestPriority);
                    i = 0;
                    highestPriority = Jobs.ElementAt(i);
                }

                if (Jobs.Count == 1)
                {
                    temp.Add(highestPriority);
                    Jobs.Remove(highestPriority);
                    break;
                }

                if(highestPriority.Priority > Jobs.ElementAt(i+1).Priority)
                {
                    highestPriority = Jobs.ElementAt(i+1);
                }
                i++;
            }

            while(temp.Count != 0)
            {
                Jobs.Add(temp.ElementAt(0));
                temp.RemoveAt(0);
            }
        }

        //This Shouldnt be needed anymore
        public async Task SetPrioritiesInQueue()
        {
            foreach (Job currentJob in Jobs)
            {
                Customer currentJobsCustomer = await Customer.Get(currentJob.Customer);
                currentJob.Priority = currentJobsCustomer.Priority();
            }
        }
        */

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

        public async Task UpdateDailyStatistic()
        {
            //Statistics Stuff
            DailyStatistic todaysStat = await DailyStatistic.Get(string.Format("$filter=Date eq {0}", new DateTimeOffset(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, default(TimeSpan)).ToString("yyyy-MM-ddTHH:mm:ssZ")));
            if (todaysStat == null)
            {
                todaysStat = new DailyStatistic();
                todaysStat.Date = new DateTimeOffset(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, default(TimeSpan));
                todaysStat.LastQueueLength = Jobs.Count;
                await todaysStat.Post();
            }
            else
            {
                todaysStat.LastQueueLength = Jobs.Count;
                await todaysStat.Update();
            }
        }
    }
}
