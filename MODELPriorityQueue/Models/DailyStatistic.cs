using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELPriorityQueue.Models
{
    public class DailyStatistic : DatabaseEntity<DailyStatistic>
    {
        private DateTime date;
        private int lastQueueLength;

        public DateTime Date
        {
            get { return date; }
            set { Set(() => Date, ref date, value); }
        }

        public int LastQueueLength
        {
            get { return lastQueueLength; }
            set { Set(() => LastQueueLength, ref lastQueueLength, value); }
        }
    }
}
