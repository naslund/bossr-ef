using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using BossrLib.Classes;

namespace BossrScraper
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            Task.Run(async () =>
            {
                await Run();
            }).Wait();

            Console.ReadKey();
        }

        public static async Task Run()
        {
            AuthenticationToken authenticationToken = await GetTokenAsync();

            List<World> worlds = await GetWorldsAsync(authenticationToken);
            foreach (World world in worlds.OrderBy(x => x.Name))
            {
                Console.WriteLine($"Id: {world.Id}\nName: {world.Name}\n");
            }

            List<Creature> creatures = await GetCreaturesAsync(authenticationToken);
            foreach (Creature creature in creatures)
            {
                Console.WriteLine($"Id: {creature.Id}\nName: {creature.Name}\nSpawntime: {creature.HoursBetweenEachSpawnMin} - {creature.HoursBetweenEachSpawnMax} hours\n");
            }
        }

        public static async Task<List<World>> GetWorldsAsync(AuthenticationToken authenticationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configuration["Data:API:Uri"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationToken.Token_Type, authenticationToken.Access_Token);

                Console.WriteLine("Requesting worlds from server");
                var response = await client.GetAsync("api/worlds");

                Console.WriteLine($"Server responded with: [{(int)response.StatusCode}] {response.StatusCode}");
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<World>>(await response.Content.ReadAsStringAsync());
                }
            }

            return null;
        }

        public static async Task<List<Creature>> GetCreaturesAsync(AuthenticationToken authenticationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configuration["Data:API:Uri"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationToken.Token_Type, authenticationToken.Access_Token);

                Console.WriteLine("Requesting creatures from server");
                var response = await client.GetAsync("api/creatures");

                Console.WriteLine($"Server responded with: [{(int)response.StatusCode}] {response.StatusCode}");
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<Creature>>(await response.Content.ReadAsStringAsync());
                }
            }

            return null;
        }

        public static async Task<AuthenticationToken> GetTokenAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configuration["Data:API:Uri"]);

                Console.WriteLine("Requesting token from server");
                var response = await client.PostAsync("connect/token", new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", Configuration["Data:API:Username"]), 
                    new KeyValuePair<string, string>("password", Configuration["Data:API:Password"]),
                    new KeyValuePair<string, string>("scope", "roles profile"),
                    new KeyValuePair<string, string>("resource", Configuration["Data:API:Uri"])
                }));
                
                Console.WriteLine($"Server responded with: [{(int)response.StatusCode}] {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<AuthenticationToken>(await response.Content.ReadAsStringAsync());
                }
            }

            return null;
        }

        public class AuthenticationToken
        {
            public string Token_Type { get; set; }
            public string Access_Token { get; set; }
            public int Expires_In { get; set; }
        }
    }
}
