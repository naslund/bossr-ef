using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Bossr.Lib;
using Newtonsoft.Json;

namespace Bossr.Services
{
    public class RestService
    {
        private readonly HttpClient client = new HttpClient();
        private readonly Uri uri = new Uri("http://datecheckerapi.azurewebsites.net/");

        public async Task<List<Status>> ReadSpawnableAsync(Guid worldId)
        {
            HttpResponseMessage response = await client.GetAsync(uri + "status?worldId=" + worldId + "&$filter=CanSpawn+eq+true");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Status>>(content);
            }

            return null;
        }

        public async Task<List<Status>> ReadUpcomingAsync(Guid worldId)
        {
            HttpResponseMessage response = await client.GetAsync(uri + "status?worldId=" + worldId + "&$filter=CanSpawn+eq+false");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Status>>(content)
                    .OrderBy(x => x.ExpectedSpawnDateMin)
                    .ToList();
            }

            return null;
        }

        public async Task<List<World>> ReadWorldsAsync()
        {
            HttpResponseMessage response = await client.GetAsync(uri + "worlds?$orderby=Name&$expand=Location,PvpType");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<World>>(content);
            }

            return null;
        }

        public async Task<List<Kill>> ReadRecentAsync(Guid worldId)
        {
            HttpResponseMessage response = await client.GetAsync(uri + "recent?worldId=" + worldId + "&$expand=Creature($expand=Category)");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Kill>>(content)
                    .OrderByDescending(x => x.SpawnedAtMin)
                    .ThenBy(x => x.Creature.Category.Name)
                    .ToList();
            }

            return null;
        }

        public async Task<List<Creature>> ReadCreaturesAsync()
        {
            HttpResponseMessage response = await client.GetAsync(uri + "creatures?$expand=Category&$select=Id,Name,Monitored,CategoryId");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Creature>>(content)
                    .OrderByDescending(x => x.Monitored)
                    .ThenBy(x => x.CategoryId)
                    .ThenBy(x => x.Name)
                    .ToList();
            }

            return null;
        }
    }
}
