using System;
using System.Threading.Tasks;

namespace MODELPriorityQueue.Models
{
    public class Job : DatabaseEntity<Job>
    {
        private string subject;
        private string description;
        private int priority;
        private int hours;
        private bool completed;
        private DateTimeOffset entered;
        private DateTimeOffset started;
        private DateTimeOffset finished;
        private Guid nextJob;
        private Guid previousJob;
        private Guid customer;
        private Guid assignedBy;
        private Guid technician;

        public string Subject
        {
            get { return subject; }
            set { Set(() => Subject, ref subject, value); }
        }

        public string Description
        {
            get { return description; }
            set { Set(() => Description, ref description, value); }
        }

        public int Priority
        {
            get { return priority; }
            set { Set(() => Priority, ref priority, value); }
        }

        public int Hours
        {
            get { return hours; }
            set { Set(() => Hours, ref hours, value); }
        }

        public bool Completed
        {
            get { return completed; }
            set { Set(() => Completed, ref completed, value); }
        }

        public DateTimeOffset Entered
        {
            get { return entered; }
            set { Set(() => Entered, ref entered, value); }
        }

        public DateTimeOffset Started
        {
            get { return started; }
            set { Set(() => Started, ref started, value); }
        }

        public DateTimeOffset Finished
        {
            get { return finished; }
            set { Set(() => Finished, ref finished, value); }
        }

        public Guid NextJob
        {
            get { return nextJob; }
            set { Set(() => NextJob, ref nextJob, value); }
        }

        public Guid PreviousJob
        {
            get { return previousJob; }
            set { Set(() => PreviousJob, ref previousJob, value); }
        }

        public Guid Customer
        {
            get { return customer; }
            set { Set(() => Customer, ref customer, value); }
        }

        public Guid AssignedBy
        {
            get { return assignedBy; }
            set { Set(() => AssignedBy, ref assignedBy, value); }
        }

        public Guid Technician
        {
            get { return technician; }
            set { Set(() => Technician, ref technician, value); }
        }
    }
}