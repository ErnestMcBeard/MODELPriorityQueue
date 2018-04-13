using MODELPriorityQueue.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace MODELPriorityQueue.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
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

        private ObservableCollection<Customer> customers;
        public ObservableCollection<Customer> Customers
        {
            get { return customers; }
            set { Set(() => Customers, ref customers, value); }
        }

        private Manager newManager = new Manager();
        public Manager NewManager
        {
            get { return newManager; }
            set { Set(() => NewManager, ref newManager, value); }
        }

        private Technician newTechnician = new Technician();
        public Technician NewTechnician
        {
            get { return newTechnician; }
            set { Set(() => NewTechnician, ref newTechnician, value); }
        }

        private Customer newCustomer = new Customer();
        public Customer NewCustomer
        {
            get { return newCustomer; }
            set { Set(() => NewCustomer, ref newCustomer, value); }
        }

        public SettingsViewModel()
        {

        }

        public async Task LoadScreenData()
        {
            Managers = new ObservableCollection<Manager>(await Manager.Get());
            Technicians = new ObservableCollection<Technician>(await Technician.Get());
            Customers = new ObservableCollection<Customer>(await Customer.Get());
        }

        internal async Task AddManager()
        {
            var result = await NewManager.Post();
            if (result != default(Manager))
            {
                Managers.Add(result);
                NewManager = new Manager();
            }
        }

        internal async Task AddTechnician()
        {
            var result = await NewTechnician.Post();
            if (result != default(Technician))
            {
                Technicians.Add(result);
                NewTechnician = new Technician();
            }
        }

        internal async Task AddCustomer()
        {
            var result = await NewCustomer.Post();
            if (result != default(Customer))
            {
                Customers.Add(result);
                NewCustomer = new Customer();
            }
        }
    }
}
