using System;

namespace MODELPriorityQueue.Models
{
    public class Technician : User
    {
        private DateTimeOffset startDate;

        protected override string ServerPath
        {
            get { return "Technicians"; }
        }

        public DateTimeOffset StartDate
        {
            get { return startDate; }
            set { Set(() => StartDate, ref startDate, value); }
        }
    }
}
