using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BossrAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace BossrAPI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RolesController : ApiController
    {
        private readonly ApplicationRoleManager manager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

        // GET: api/roles
        public async Task<IHttpActionResult> GetRoles()
        {
            return Ok(await manager.Roles.Select(x => new RoleInfo { Id = x.Id, Name = x.Name }).ToListAsync());
        }

        // POST: api/roles
        public async Task<IHttpActionResult> PostRole(Role role)
        {
            IdentityResult result = await manager.CreateAsync(role);
            if (result.Succeeded)
                return Ok($"{RequestContext.Principal.Identity.Name} created role: {role.Name}");

            return BadRequest(result.Errors.First());
        }
    }
}
