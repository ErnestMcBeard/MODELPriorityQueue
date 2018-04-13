using System;
using System.Threading.Tasks;

namespace MODELPriorityQueue.Models
{
    public class Technician : User<Technician>
    {
        private DateTimeOffset startDate;
        
        public DateTimeOffset StartDate
        {
            get { return startDate; }
            set { Set(() => StartDate, ref startDate, value); }
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
