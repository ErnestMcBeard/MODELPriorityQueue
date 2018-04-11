using System;
using System.Collections.Generic;
using System.Linq;

namespace MODELPriorityQueue.Models
{
    public class Product : DatabaseEntry
    {
        private string name;
        private decimal price;
        private string category;


        public string Name
        {
            get { return name; }
            set { Set(() => Name, ref name, value); }
        }

        public decimal Price
        {
            get { return price; }
            set { Set(() => Price, ref price, value); }
        }

        public string Category
        {
            get { return category; }
            set { Set(() => Category, ref category, value); }
        }
    }
}