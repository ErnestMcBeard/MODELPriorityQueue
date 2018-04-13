using System;
using System.Threading.Tasks;

namespace MODELPriorityQueue.Models
{
    public class Technician : User<Technician>
    {
        private DateTimeOffset startDate = new DateTimeOffset();
        public DateTimeOffset StartDate
        {
            get { return startDate; }
            set { Set(() => StartDate, ref startDate, value); }
        }
    }
}
