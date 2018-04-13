using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.Web.Http;

namespace MODELPriorityQueue.Models
{
    public abstract class DatabaseEntity<T> : BindableBase
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
            string response = await client.GetStringAsync(new Uri(BaseUrl));
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
            string response = await client.GetStringAsync(new Uri(string.Format("{0}({1})", BaseUrl, id)));
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

            string response = await client.GetStringAsync(new Uri(string.Format("{0}?{1}", BaseUrl, query)));
            if (!string.IsNullOrEmpty(response))
            {
                return JsonConvert.DeserializeObject<ODataResponse<T>>(response).Value.FirstOrDefault();
            }
            return default(T);
        }



        /// <summary>
        /// Adds this object as a new item to the database with a unique Guid
        /// </summary>
        public async Task<T> Post()
        {
            try
            {
                var requestUrl = new Uri(BaseUrl);
                var json = JsonConvert.SerializeObject(this);
                var client = new HttpClient();
                var content = new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                request.Content = content;
                var response = await client.SendRequestAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    await new MessageDialog(response.StatusCode.ToString()).ShowAsync();
                    return default(T);
                }
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseString);
            }
            catch (Exception e)
            {
                await new MessageDialog(e.ToString()).ShowAsync();
                return default(T);
            }
        }



        /// <summary>
        /// Sends this object to the database, and updates its fields on the server
        /// </summary>
        //public async Task<E> Update()
        //{

        //}



        /// <summary>
        /// Removes this object to the database
        /// </summary>
        //public async Task<E> Delete()
        //{

        //}
    }
}
