using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace BossrAPI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RolesController : ApiController
    {
        public async Task<IHttpActionResult> PostRole(IdentityRole role)
        {
            ApplicationRoleManager rolemanager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

            IdentityResult result = await rolemanager.CreateAsync(role);
            if (result.Succeeded)
                return Ok($"Created role: {role.Name}");

            return BadRequest(result.Errors.First());
        }
    }
}
