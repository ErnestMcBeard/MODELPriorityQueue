using System;
using System.Collections.Generic;
using System.Text;
using Template10.Mvvm;

namespace MODELPriorityQueue.Models
{
    public abstract class DatabaseEntry : BindableBase
    {
        public Guid id;
        public bool isDirty;

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
