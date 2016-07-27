using System;

namespace BossrAPI
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}