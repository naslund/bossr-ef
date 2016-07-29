using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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