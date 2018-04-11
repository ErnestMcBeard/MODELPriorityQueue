using System;

namespace MODELPriorityQueue.Models
{
    public class Technician : User
    {
        private DateTimeOffset startDate;

        public DateTimeOffset StartDate
        {
            get { return startDate; }
            set { Set(() => StartDate, ref startDate, value); }
        }
    }
}
