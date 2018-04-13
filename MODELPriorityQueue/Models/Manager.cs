using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MODELPriorityQueue.Models
{
    public class Manager : User<Manager>
    {
<<<<<<< HEAD
        private string endpoint = "Managers";

        public override async Task<bool> Add()
        {
            Id = System.Guid.NewGuid();
            var json = JsonConvert.SerializeObject(this);
            var result = await Post(endpoint, json);
            return true;
        }

        public override Task<bool> Delete()
        {
            throw new System.NotImplementedException();
        }

        public override Task<bool> Save()
        {
            throw new System.NotImplementedException();
        }
=======
>>>>>>> d740321f29e99d3cf4abb813c3df9a3d3c8a6e12
    }
}
