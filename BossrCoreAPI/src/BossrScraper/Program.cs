using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using BossrLib.Classes;
using BossrScraper.Models;

namespace BossrScraper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            RestService.Configuration = builder.Build();

            Task.Run(async () =>
            {
                await Run();
            }).Wait();
            
            Console.ReadKey();
        }

        public static async Task Run()
        {
            Console.Title = "Bossr Autoscrape | Write 'exit' to close the program | Write 'force' to scrape";
            
            Timer timer = new Timer(async state =>
            {
                await ScrapeEvent(state);
            }, null, TimeSpan.FromSeconds(10), TimeSpan.FromMinutes(30));

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "exit")
                    break;
                if (input == "force")
                    await ScrapeEvent(null);
            }
        }

        private static async Task ScrapeEvent(object state)
        {
            await Scraper.ScrapeAllWorlds();
        }
    }
}
