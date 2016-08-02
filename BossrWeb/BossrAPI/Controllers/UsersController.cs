using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using BossrAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BossrAPI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : ApiController
    {
        private readonly BossrDbContext db = new BossrDbContext();
        private readonly ApplicationUserManager manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

        // GET: api/users
        public async Task<IHttpActionResult> GetUsers()
        {
            return Ok(await manager.Users.ToListAsync());
        }

        // GET: api/users/5
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await manager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST: api/users
        public async Task<IHttpActionResult> PostUser(UserRequest userRequest)
        {
            User user = new User { UserName = userRequest.Username };
            IdentityResult result = await manager.CreateAsync(user, userRequest.Password);
            if (result.Succeeded)
                return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
            
            return BadRequest(result.Errors.First());
        }

        // DELETE: api/users/5
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            var user = await manager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            IdentityResult result = await manager.DeleteAsync(user);
            if (result.Succeeded)
                return Ok(user);

            return BadRequest(result.Errors.First());
        }

        // PUT: api/users/5/password
        [HttpPut]
        [Route("api/users/{id}/password")]
        public async Task<IHttpActionResult> ChangeUserPassword(int id, PasswordRequest passwordRequest)
        {
            if (id != passwordRequest.Id)
                return BadRequest();

            if (passwordRequest.NewPassword == null || passwordRequest.OldPassword == null)
                return BadRequest();

            IdentityResult result = await manager.ChangePasswordAsync(id, passwordRequest.OldPassword, passwordRequest.NewPassword);

            if (result.Succeeded)
                return Ok();
            
            return BadRequest(result.Errors.First());
            
        }

        // PUT: api/users/5/roles/5
        [HttpPut]
        [Route("api/users/{id}/roles/{roleid}")]
        public async Task<IHttpActionResult> AssignUserToRole(int id, int roleid)
        {
            User user = await manager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            Role role = db.Roles.Find(roleid);
            if (role == null)
                return NotFound();

            IdentityResult result = await manager.AddToRoleAsync(user.Id, role.Name);

            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors.First());
        }

        // DELETE: api/users/5/roles/5
        [HttpDelete]
        [Route("api/users/{id}/roles/{roleid}")]
        public async Task<IHttpActionResult> DismissUserFromRole(int id, int roleid)
        {
            User user = await manager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            Role role = db.Roles.Find(roleid);
            if (role == null)
                return NotFound();

            IdentityResult result = await manager.RemoveFromRoleAsync(user.Id, role.Name);

            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors.First());
        }
    }
}
