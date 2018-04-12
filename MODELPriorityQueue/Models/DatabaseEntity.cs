using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Popups;
using Windows.Web.Http;

namespace MODELPriorityQueue.Models
{
    public abstract class DatabaseEntity<E> : BindableBase
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
        protected async Task<E> Post(string requestEndpoint, string jsonObject)
        {
            try
            {
                var requestUrl = Globals.WebApiUrl + "/" + requestEndpoint;
                var client = new HttpClient();
                var content = new HttpStringContent(jsonObject);
                var response = await client.PostAsync(new Uri(requestUrl), content);

                if (!response.IsSuccessStatusCode)
                {
                    await new MessageDialog(response.StatusCode.ToString()).ShowAsync();
                    return default(E);
                }
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<E>(responseString);
            }
            catch (Exception e)
            {
                await new MessageDialog(e.ToString()).ShowAsync();
                return default(E);
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

        public abstract Task<bool> Add();

        public abstract Task<bool> Save();

        public abstract Task<bool> Delete();
    }
}
