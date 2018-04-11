using Newtonsoft.Json;
using System;
using Template10.Mvvm;

namespace MODELPriorityQueue.Models
{
    public abstract class DatabaseEntry : BindableBase
    {
        private Guid id;
        private bool isDirty;

        public Guid Id
        {
            get { return id; }
            set { Set(() => Id, ref id, value); }
        }

        [JsonIgnore]
        public bool IsDirty
        {
            get { return isDirty; }
            set { Set(() => IsDirty, ref isDirty, value); }
        }

        /// <summary>
        /// Adds this object as a new item to the database with a unique Guid
        /// </summary>
        public void Post()
        {

        }

        /// <summary>
        /// Sends this object to the database, and updates its fields on the server
        /// </summary>
        public void Update()
        {

        }

        /// <summary>
        /// Removes this object to the database
        /// </summary>
        public void Delete()
        {

        }
    }
}
