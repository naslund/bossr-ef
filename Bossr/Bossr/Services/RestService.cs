using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Bossr.Lib;
using Newtonsoft.Json;

namespace Bossr.Services
{
    public class RestService
    {
        HttpClient client;
        private static readonly Uri uri = new Uri("http://datecheckerapi.azurewebsites.net/");
        public List<Status> Statuses { get; private set; } = new List<Status>();
        public List<World> Worlds { get; private set; } = new List<World>();

        public RestService()
        {
            client = new HttpClient();
        }

        public async Task<List<Status>> ReadStatusAsync(Guid worldId)
        {
            try
            {
                var response = await client.GetAsync(uri + "status?worldId=" + worldId + "&$filter=CanSpawn+eq+true");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Statuses = JsonConvert.DeserializeObject<List<Status>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
            }

            return Statuses;
        }

        public async Task<List<World>> ReadWorldsAsync()
        {
            try
            {
                var response = await client.GetAsync(uri + "worlds?$orderby=Name");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Worlds = JsonConvert.DeserializeObject<List<World>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
            }

            return Worlds;
        }
    }
}
