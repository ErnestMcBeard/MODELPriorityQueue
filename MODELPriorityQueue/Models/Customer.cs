using System;
using System.Threading.Tasks;

namespace MODELPriorityQueue.Models
{
<<<<<<< HEAD
    public class Customer : DatabaseEntity<Customer>
=======
    public class Customer : DatabaseEntry<Customer>
>>>>>>> d740321f29e99d3cf4abb813c3df9a3d3c8a6e12
    {
        private string endpoint = "Customers";

        private string name;
        private int timesServiced;
        private string street;
        private string city;
        private int zip;
        private string state;

        public string Name
        {
            get { return name; }
            set { Set(() => Name, ref name, value); }
        }

        public int TimesServiced
        {
            get { return timesServiced; }
            set { Set(() => TimesServiced, ref timesServiced, value); }
        }

        public string Street
        {
            get { return street; }
            set { Set(() => Street, ref street, value); }
        }

        public string City
        {
            get { return city; }
            set { Set(() => City, ref city, value); }
        }

        public int Zip
        {
            get { return zip; }
            set { Set(() => Zip, ref zip, value); }
        }

        public string State
        {
            get { return state; }
            set { Set(() => State, ref state, value); }
        }

        public override Task<bool> Add()
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Delete()
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Save()
        {
            throw new NotImplementedException();
        }
    }
}