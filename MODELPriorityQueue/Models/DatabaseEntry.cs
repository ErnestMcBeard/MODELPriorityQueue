using Newtonsoft.Json;
using System;
using System.Net.Http;
using Template10.Mvvm;

namespace MODELPriorityQueue.Models
{
    public abstract class DatabaseEntry : BindableBase
    {
        private Guid id;
        private bool isDirty;
        private string serverDomain = "http://localhost:51578";
        private static readonly HttpClient client = new HttpClient();

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
        
        protected abstract string ServerPath { get; }

        /// <summary>
        /// Adds this object as a new item to the database with a unique Guid
        /// </summary>
        public async void Post()
        {
            string json = JsonConvert.SerializeObject(this);
            HttpContent content = new StringContent(json);
            string url = string.Format("{0}/{1}", serverDomain, ServerPath);
            HttpResponseMessage response = await client.PostAsync(url, content);
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
