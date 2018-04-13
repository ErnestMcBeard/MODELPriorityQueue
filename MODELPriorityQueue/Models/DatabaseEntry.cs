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
        /// Selects all of the object from DbSet<T>
        /// </summary>
        /// <returns>Returns a list of T object, or null if something fucked up</returns>
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
        /// Selects an object from DbSet<T> with the given Id
        /// </summary>
        /// <param name="id">The Guid of the desired object</param>
        /// <returns>An object from DbSet<T> with the given id, or null if it does not exist</returns>
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
        /// Selects an object from DbSet<T> with the given Id
        /// </summary>
        /// <param name="query">The filter, orderby, etc. See this method definition for examples</param>
        /// <returns>An object from DbSet<T> with the given id, or null if it does not exist</returns>
        public static async Task<T> Get(string query)
        {
            //Basic Query Examples
            //Filter:   $filter=Firstname eq 'John' and TimesServiced eq 2
            //Orderby:  $orderby=Priority desc
            //Combo:    $filter=Firstname eq 'John'&$orderby=Priority desc

            //For more query options try google. There's a shit-ton

            string response = await client.GetStringAsync(string.Format("{0}?{1}", BaseUrl, query));
            if (!string.IsNullOrEmpty(response))
            {
                return JsonConvert.DeserializeObject<ODataResponse<T>>(response).Value.FirstOrDefault();
            }
            return default(T);
        }

        /// <summary>
        /// Inserts this object into the database
        /// </summary>
        /// <returns>The object in its state after inserting it</returns>
        public async Task<T> Post()
        {
            string json = JsonConvert.SerializeObject(this);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(BaseUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                T updatedObject = JsonConvert.DeserializeObject<T>(message);
                return updatedObject;
            }

            return null;
        }

        /// <summary>
        /// Updates this instance of the object in the database
        /// </summary>
        /// <returns>True if it succeeded, false otherwise</returns>
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
        /// Deletes the object from DbSet<T> with the same Id as this object
        /// </summary>
        /// <returns>True if it succeeded, false otherwise</returns>
        public async Task<bool> Delete()
        {
            HttpResponseMessage response = await client.DeleteAsync(string.Format("{0}({1})", BaseUrl, Id));
            return response.IsSuccessStatusCode;
        }
    }
}
