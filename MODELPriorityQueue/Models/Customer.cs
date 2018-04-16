using System;
using System.Threading.Tasks;

namespace MODELPriorityQueue.Models
{
    public class Customer : DatabaseEntity<Customer>
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

        public int Priority()
        {
            if(timesServiced >= 5)
            {
                return 1;
            }
            else if(timesServiced >= 3)
            {
                return 2;
            }
            else if(timesServiced >= 1)
            {
                return 3;
            }
            else
            {
                return 4;
            }
            

        }
    }
}