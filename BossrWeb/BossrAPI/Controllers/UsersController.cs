using System.Collections.Generic;
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
    public class UsersController : ApiController
    {
        private readonly BossrDbContext db = new BossrDbContext();
        private readonly ApplicationUserManager manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private readonly ApplicationRoleManager rolemanager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

        // GET: api/users
        [AllowAnonymous]
        public List<UserInfo> GetUsers()
        {
            List<UserInfo> userInfo = new List<UserInfo>();

            foreach (User user in manager.Users.ToList())
            {
                List<RoleInfo> roleInfo = user.Roles
                    .Select(bossrUserRole => rolemanager.FindById(bossrUserRole.RoleId))
                    .Select(role => new RoleInfo {Id = role.Id, Name = role.Name})
                    .ToList();

                userInfo.Add(new UserInfo
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Roles = roleInfo
                });
            }

            return userInfo;
        }

        // POST: api/users
        public async Task<IHttpActionResult> PostUser(User user)
        {
            IdentityResult result = await manager.CreateAsync(user, "Password1!");
            if (result.Succeeded)
                return Ok($"{RequestContext.Principal.Identity.Name} created user: {user.UserName} with password: Password1!");
            
            return BadRequest(result.Errors.First());
        }
        
        // PUT: api/users/5/roles/5
        [HttpPut]
        [Route("api/users/{userid}/roles/{roleid}")]
        public async Task<IHttpActionResult> AssignUserToRole(int userid, int roleid)
        {
            User user = await manager.FindByIdAsync(userid);
            Role role = db.Roles.Find(roleid);
            IdentityResult result = await manager.AddToRoleAsync(user.Id, role.Name);

            if (result.Succeeded)
                return Ok($"Added {user.UserName} to role {role.Name}");
            
            return BadRequest(result.Errors.First());
        }

        // DELETE: api/users/5/roles/5
        [HttpDelete]
        [Route("api/users/{userid}/roles/{roleid}")]
        public async Task<IHttpActionResult> DismissUserFromRole(int userid, int roleid)
        {
            User user = await manager.FindByIdAsync(userid);
            Role role = db.Roles.Find(roleid);
            IdentityResult result = await manager.RemoveFromRoleAsync(user.Id, role.Name);

            if (result.Succeeded)
                return Ok($"Removed {user.UserName} from role {role.Name}");

            return BadRequest(result.Errors.First());
        }
    }
}
