using System;
using System.ComponentModel;
using BossrAPI.Filters;
using System.Web.Http;
using Newtonsoft.Json;

namespace BossrAPI
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new RequireHttpsAttribute());
            GlobalConfiguration.Configuration.Filters.Add(new AuthorizeAttribute());
        }
    }
}