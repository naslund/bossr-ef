using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BossrLib.Classes;
using BossrScraper.Models;
using HtmlAgilityPack;

namespace BossrScraper
{
    public static class Scraper
    {
        public static async Task ScrapeAllWorlds()
        {
            if (DateTime.UtcNow.Hour < 2)
            {
                Console.WriteLine($"{DateTime.UtcNow} UTC: Time is earlier than 02:00 UTC, not scraping.");
                return;
            }

            if (await RestService.PostTokenAsync() == false)
            {
                Console.WriteLine($"{DateTime.UtcNow} UTC: Error getting token");
                return;
            }

            List<World> worlds = await RestService.GetWorldsAsync();
            if (worlds == null)
            {
                Console.WriteLine($"{DateTime.UtcNow} UTC: Error getting worlds");
                return;
            }

            foreach (World world in worlds.OrderBy(x => x.Name))
                await Scrape(world);
        }

        private static async Task Scrape(World world)
        {
            int statsadded = 0;

            HtmlNodeCollection htmlNodeCollectionFromWorld = await GetHtmlNodeCollectionFromWorld(world.Name);
            if (htmlNodeCollectionFromWorld == null)
            {
                Console.WriteLine($"{DateTime.UtcNow} UTC: {world.Name} - Error reading stats");
                return;
            }

            if (IsThereAnyNewStats(world, htmlNodeCollectionFromWorld) == false)
            {
                Console.WriteLine($"{DateTime.UtcNow} UTC: {world.Name} - No new stats found");
                return;
            }

            List<KillStats> killStats = HtmlNodeCollectionToKillStats(htmlNodeCollectionFromWorld);
            if (killStats == null)
            {
                Console.WriteLine($"{DateTime.UtcNow} UTC: {world.Name} - Error parsing stats");
                return;
            }

            await AddMissingCreatures(killStats);

            List<Creature> creatures = await RestService.GetCreaturesAsync();
            if (creatures == null)
            {
                Console.WriteLine($"{DateTime.UtcNow} UTC: {world.Name} - Error getting creatures");
                return;
            }

            foreach (KillStats killStatsTableRow in killStats)
            {
                Creature creature = creatures.Single(x => x.Name == killStatsTableRow.Name);

                if (creature.Monitored)
                {
                    int amountOfKills = 1;
                    if (killStatsTableRow.LastDayKills > 0 || killStatsTableRow.LastDayDeaths > 0)
                    {
                        List<Location> locations = await RestService.GetCreatureLocations(creature);
                        if (locations == null)
                        {
                            Console.WriteLine($"{DateTime.UtcNow} UTC: {world.Name} - Error getting locations for {creature.Name}");
                            return;
                        }

                        if (killStatsTableRow.LastDayKills > 0 && locations.Count > 1)
                            amountOfKills = killStatsTableRow.LastDayKills;

                        for (int i = 0; i < amountOfKills; i++)
                        {
                            Spawn spawn = new Spawn
                            {
                                CreatureId = creature.Id,
                                WorldId = world.Id,
                                TimeMinUtc = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 2, 0, 0, DateTimeKind.Utc).AddDays(-1),
                                TimeMaxUtc = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 2, 0, 0, DateTimeKind.Utc)
                            };
                            statsadded++;
                            if (await RestService.PostSpawnAsync(spawn) == false)
                            {
                                Console.WriteLine($"{DateTime.UtcNow} UTC: Error posting spawn: {creature.Name} on {world.Name}");
                                return;
                            } 
                        }
                    }
                }
            }

            world.LastDayDeaths = int.Parse(htmlNodeCollectionFromWorld.Last().ChildNodes[1].InnerText.Replace("&#160;", ""));
            world.LastDayKills = int.Parse(htmlNodeCollectionFromWorld.Last().ChildNodes[2].InnerText.Replace("&#160;", ""));
            world.LastScrapeTime = DateTime.UtcNow;

            if (await RestService.PutWorldAsync(world))
                Console.WriteLine($"{DateTime.UtcNow} UTC: {world.Name}: {statsadded} kills added");
        }

        private static async Task<HtmlNodeCollection> GetHtmlNodeCollectionFromWorld(string world)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(new Uri("https://secure.tibia.com/community/?subtopic=killstatistics&world=" + world));

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(await response.Content.ReadAsStringAsync());

                return doc.DocumentNode.SelectSingleNode("//*[@class='BoxContent']/table").ChildNodes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static bool IsThereAnyNewStats(World world, HtmlNodeCollection htmlNodeCollection)
        {
            return htmlNodeCollection.Last().ChildNodes[1].InnerText.Replace("&#160;", "").Trim() != world.LastDayDeaths.ToString()
                || htmlNodeCollection.Last().ChildNodes[2].InnerText.Replace("&#160;", "").Trim() != world.LastDayKills.ToString();
        }

        private static async Task AddMissingCreatures(List<KillStats> lkstr)
        {
            List<Creature> creatures = await RestService.GetCreaturesAsync();
            if (creatures == null)
                return;

            foreach (KillStats kstr in lkstr)
            {
                if (creatures.Any(item => item.Name == kstr.Name) == false)
                {
                    Creature creature = new Creature
                    {
                        Name = kstr.Name,
                        Monitored = false
                    };

                    if (await RestService.PostCreatureAsync(creature))
                        Console.WriteLine($"{DateTime.UtcNow} UTC: Added {creature.Name} to Bossr.");
                    else
                        Console.WriteLine($"{DateTime.UtcNow} UTC: Error posting creature {creature.Name}");
                }
            }
        }

        private static List<KillStats> HtmlNodeCollectionToKillStats(HtmlNodeCollection scrape)
        {
            try
            {
                List<KillStats> result = new List<KillStats>();
                foreach (HtmlNode node in scrape)
                {
                    if (node.Attributes["bgcolor"].Value != "#505050")
                    {
                        KillStats killStat = new KillStats
                        {
                            Name = node.ChildNodes[0].InnerText.Replace("&#160;", ""),
                            LastDayDeaths = int.Parse(node.ChildNodes[1].InnerText.Replace("&#160;", "")),
                            LastDayKills = int.Parse(node.ChildNodes[2].InnerText.Replace("&#160;", "")),
                            LastWeekDeaths = int.Parse(node.ChildNodes[3].InnerText.Replace("&#160;", "")),
                            LastWeekKills = int.Parse(node.ChildNodes[4].InnerText.Replace("&#160;", ""))
                        };
                        result.Add(killStat);
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
