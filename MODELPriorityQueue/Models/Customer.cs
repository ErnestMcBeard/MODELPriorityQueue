using System;

namespace MODELPriorityQueue.Models
{
    public class Customer : DatabaseEntry
    {
        private string name;
        private int timesServiced;
        private string street;
        private string city;
        private int zip;
        private string state;

        protected override string ServerPath
        {
            get { return "Customers"; }
        }

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
    }
}