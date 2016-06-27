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
        public List<Status> Spawnable { get; private set; } = new List<Status>();
        public List<Status> Upcoming { get; private set; } = new List<Status>();
        public List<World> Worlds { get; private set; } = new List<World>();
        public List<Kill> Recent { get; private set; } = new List<Kill>();
        public List<Creature> Creatures { get; private set; } = new List<Creature>();

        public RestService()
        {
            client = new HttpClient();
        }

        public async Task<List<Status>> ReadSpawnableAsync(Guid worldId)
        {
            try
            {
                var response = await client.GetAsync(uri + "status?worldId=" + worldId + "&$filter=CanSpawn+eq+true");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Spawnable = JsonConvert.DeserializeObject<List<Status>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
            }

            return Spawnable;
        }

        public async Task<List<Status>> ReadUpcomingAsync(Guid worldId)
        {
            try
            {
                var response = await client.GetAsync(uri + "status?worldId=" + worldId + "&$filter=CanSpawn+eq+false&$orderby=ExpectedSpawnDateMin");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Upcoming = JsonConvert.DeserializeObject<List<Status>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
            }

            return Upcoming;
        }

        public async Task<List<World>> ReadWorldsAsync()
        {
            try
            {
                var response = await client.GetAsync(uri + "worlds?$orderby=Name&$expand=Location,PvpType");
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

        public async Task<List<Kill>> ReadRecentAsync(Guid worldId)
        {
            try
            {
                var response = await client.GetAsync(uri + "recent?worldId=" + worldId + "&$expand=Creature($expand=Category)&$orderby=SpawnedAtMin desc,Creature/Category/Name");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Recent = JsonConvert.DeserializeObject<List<Kill>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
            }

            return Recent;
        }

        public async Task<List<Creature>> ReadCreaturesAsync()
        {
            try
            {
                var response = await client.GetAsync(uri + "creatures?$select=Id,Name,Monitored,CategoryId&$orderby=Monitored+desc,CategoryId,Name");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Creatures = JsonConvert.DeserializeObject<List<Creature>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
            }

            return Creatures;
        }
    }
}
