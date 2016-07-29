using System;
using BossrAPI.Filters;
using System.Web.Http;

namespace BossrAPI
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new RequireHttpsAttribute());
        }
    }
}