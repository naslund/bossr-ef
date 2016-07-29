using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BossrAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BossrAPI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AccountsController : ApiController
    {
        // POST: api/Accounts
        public async Task<IHttpActionResult> PostAccount(ApplicationUser user)
        {
            ApplicationUserManager usermanager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            IdentityResult result = await usermanager.CreateAsync(user, "Password1!");
            if (result.Succeeded)
                return Ok($"Created user: {user.UserName} with password: Password1!");

            return BadRequest(result.Errors.First());
        }
    }
}
