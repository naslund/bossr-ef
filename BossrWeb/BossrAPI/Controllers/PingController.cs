using System;
using System.Web.Http;

namespace BossrAPI.Controllers
{
    public class PingController : ApiController
    {
        public string GetPing()
        {
            return DateTime.UtcNow.ToString();
        }
    }
}