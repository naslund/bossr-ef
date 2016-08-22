using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BossrLib.Classes;
using BossrScraper.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BossrScraper
{
    public static class RestService
    {
        public static IConfigurationRoot Configuration { get; set; }
        private static AuthToken Token { get; set; }

        public static async Task<List<World>> GetWorldsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configuration["Data:API:Uri"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token.Token_Type, Token.Access_Token);
                
                var response = await client.GetAsync("api/worlds");
                
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<List<World>>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }

        public static async Task<List<Creature>> GetCreaturesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configuration["Data:API:Uri"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token.Token_Type, Token.Access_Token);
                
                var response = await client.GetAsync("api/creatures");
                
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<List<Creature>>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }

        public static async Task<List<Location>> GetCreatureLocations(Creature creature)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configuration["Data:API:Uri"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token.Token_Type, Token.Access_Token);

                var response = await client.GetAsync($"api/creatures/{creature.Id}/locations");

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<List<Location>>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }

        public static async Task<bool> PostSpawnAsync(Spawn spawn)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configuration["Data:API:Uri"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token.Token_Type, Token.Access_Token);

                var response = await client.PostAsync("api/spawns", new StringContent(JsonConvert.SerializeObject(spawn), Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> PostCreatureAsync(Creature creature)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configuration["Data:API:Uri"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token.Token_Type, Token.Access_Token);
                
                var response = await client.PostAsync("api/creatures", new StringContent(JsonConvert.SerializeObject(creature), Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> PostTokenAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configuration["Data:API:Uri"]);
                
                var response = await client.PostAsync("connect/token", new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", Configuration["Data:API:Username"]),
                    new KeyValuePair<string, string>("password", Configuration["Data:API:Password"]),
                    new KeyValuePair<string, string>("scope", "roles profile"),
                    new KeyValuePair<string, string>("resource", Configuration["Data:API:Uri"])
                }));

                if (response.IsSuccessStatusCode)
                {
                    Token = JsonConvert.DeserializeObject<AuthToken>(await response.Content.ReadAsStringAsync());
                    return true;
                }
                return false;
            }
        }

        public static async Task<bool> PutWorldAsync(World world)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configuration["Data:API:Uri"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token.Token_Type, Token.Access_Token);
                
                var response = await client.PutAsync($"api/worlds/{world.Id}", new StringContent(JsonConvert.SerializeObject(world), Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
        }
    }
}
