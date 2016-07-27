using System;
using BossrAPI;
using BossrAPI.Jobs;
using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace BossrAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("BossrContext");
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate(() => Scraping.Scrape(), Cron.MinuteInterval(15));
        }
    }
}