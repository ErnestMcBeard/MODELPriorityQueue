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
            Jobs = new ObservableCollection<Job>(await Job.Get());
            Customers = new ObservableCollection<Customer>(await Customer.Get());
            Managers = new ObservableCollection<Manager>(await Manager.Get());
            Technicians = new ObservableCollection<Technician>(await Technician.Get());
        }

        public void ExpandPropertiesForJob()
        {
            SelectedCustomer = Customers?.Where(x => x.Id == SelectedJob.Customer).FirstOrDefault();
            SelectedManager = Managers?.Where(x => x.Id == SelectedJob.AssignedBy).FirstOrDefault();
            SelectedTechnician = Technicians?.Where(x => x.Id == SelectedJob.Technician).FirstOrDefault();
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

        public async Task DeletedJob()
        {
            
        }
    }
}
