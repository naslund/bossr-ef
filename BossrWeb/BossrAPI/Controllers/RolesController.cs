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
            return Ok(await manager.Roles.ToListAsync());
        }

        // GET: api/roles/5
        public async Task<IHttpActionResult> GetRole(int id)
        {
            Role role = await manager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            return Ok(role);
        }

        // PUT: api/roles/5
        public async Task<IHttpActionResult> PutRole(int id, Role role)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != role.Id)
                return BadRequest();

            Role userRole = await manager.FindByIdAsync(id);
            userRole.Name = role.Name;

            IdentityResult result = await manager.UpdateAsync(userRole);

            if (result.Succeeded)
                return Ok(userRole);

            return BadRequest(result.Errors.First());
        }

        // POST: api/roles
        public async Task<IHttpActionResult> PostRole(Role role)
        {
            IdentityResult result = await manager.CreateAsync(role);
            if (result.Succeeded)
                return Ok(role);

            return BadRequest(result.Errors.First());
        }
    }
}
