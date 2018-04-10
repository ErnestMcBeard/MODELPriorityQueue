using System;
using System.Collections.Generic;
using System.Linq;

namespace MODELPriorityQueue.Models
{
    public class Product : DatabaseEntry
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}