using System;

namespace MODELPriorityQueue.Models
{
    public class Customer : DatabaseEntry
    {
        public string Name { get; set; }
        public int TimesServiced { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public string State { get; set; }
    }
}