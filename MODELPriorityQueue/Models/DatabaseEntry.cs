using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Template10.Mvvm;
using System.Reflection;
using System.Linq;
using System.Text;

namespace MODELPriorityQueue.Models
{
    public abstract class DatabaseEntry<T> : BindableBase where T : DatabaseEntry<T>
    {
        private Guid id;
        private bool isDirty;
        private static string serverDomain = "http://localhost:51578";
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
        
        private static string BaseUrl
        {
            get { return string.Format("{0}/{1}", serverDomain, typeof(T).Name); }
        }
        
        /// <summary>
        /// Gets all objects of this class from the server
        /// </summary>
        public static async Task<List<T>> Get()
        {
            string response = await client.GetStringAsync(BaseUrl);
            if (!string.IsNullOrEmpty(response))
            {
                return JsonConvert.DeserializeObject<ODataResponse<T>>(response).Value;
            }
            return null;
        }

        /// <summary>
        /// Gets object of this class with the specified from the server
        /// </summary>
        public static async Task<T> Get(Guid id)
        {
            string response = await client.GetStringAsync(string.Format("{0}({1})", BaseUrl, id));
            if (!string.IsNullOrEmpty(response))
            {
                return JsonConvert.DeserializeObject<ODataResponse<T>>(response).Value.FirstOrDefault();
            }
            return default(T);
        }

        /// <summary>
        /// Adds this object as a new item to the database with a unique Guid
        /// </summary>
        public async Task Post()
        {
            string json = JsonConvert.SerializeObject(this);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(BaseUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                T updatedObject = (T)this;
                updatedObject = JsonConvert.DeserializeObject<T>(message);
            }
        }

        /// <summary>
        /// Updates this object in the database
        /// </summary>
        public async Task<bool> Update()
        {
            var method = new HttpMethod("PATCH");
            var json = JsonConvert.SerializeObject(this);

            var request = new HttpRequestMessage(method, string.Format("{0}({1})", BaseUrl, Id))
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            HttpResponseMessage response = await client.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Removes this object from the database
        /// </summary>
        public async Task<bool> Delete()
        {
            HttpResponseMessage response = await client.DeleteAsync(string.Format("{0}({1})", BaseUrl, Id));
            return response.IsSuccessStatusCode;
        }
    }
}
