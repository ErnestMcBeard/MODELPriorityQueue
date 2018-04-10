using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELPriorityQueue.Models
{
    public abstract class User : DatabaseEntry
    {
        public string username;
        public string password;
        public string firstName;
        public string lastName;
    }
}
