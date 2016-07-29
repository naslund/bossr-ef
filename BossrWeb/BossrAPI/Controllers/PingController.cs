using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BossrAPI.Controllers
{
    public class PingController : ApiController
    {
        [AllowAnonymous]
        public Ping GetPing()
        {
            return new Ping { CurrentTime = $"{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")} UTC" };
        }
    }

    public class Ping
    {
        public string CurrentTime { get; set; }
    }
}