using System.Collections.Generic;
using System.Threading.Tasks;
using BossrCoreAPI.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BossrCoreAPI.Controllers.Identity
{
    [Route("api/[controller]")]
    [Authorize(Roles = "SysOp")]
    public class UsersController : Controller
    {
        private readonly ApplicationUserManager usermanager;
        private readonly ApplicationRoleManager rolemanager;

        public UsersController(ApplicationUserManager usermanager, ApplicationRoleManager rolemanager)
        {
            this.usermanager = usermanager;
            this.rolemanager = rolemanager;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await usermanager.Users.ToListAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await usermanager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            return Ok(user);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostUser(string username, string password)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (username == null || password == null)
                return BadRequest();

            IdentityResult result = await usermanager.CreateAsync(new ApplicationUser { UserName = username }, password);
            if (result.Succeeded)
                return Ok(usermanager.FindByNameAsync(username)); // Todo: 201 Created

            return BadRequest(result.Errors);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await usermanager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            IdentityResult result = await usermanager.DeleteAsync(user);
            if (result.Succeeded)
                return Ok(user);

            return BadRequest(result.Errors);
        }
        
        [HttpPut]
        [Route("{id}/password")]
        public async Task<IActionResult> ChangeUserPassword(int id, string oldPassword, string newPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (oldPassword == null || newPassword == null)
                return BadRequest();

            ApplicationUser user = await usermanager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            IdentityResult result = await usermanager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }
        
        [HttpGet]
        [Route("{id}/roles")]
        public async Task<IActionResult> GetUserRoles(int id)
        {
            ApplicationUser user = await usermanager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            List<ApplicationRole> roles = new List<ApplicationRole>();
            IList<string> rolenames = await usermanager.GetRolesAsync(user);
            foreach (string rolename in rolenames)
                roles.Add(await rolemanager.FindByNameAsync(rolename));

            return Ok(roles);
        }
        
        [HttpPut]
        [Route("{id}/roles/{roleid}")]
        public async Task<IActionResult> AssignUserToRole(int id, int roleid)
        {
            ApplicationUser user = await usermanager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            ApplicationRole role = await rolemanager.FindByIdAsync(roleid.ToString());
            if (role == null)
                return NotFound();

            IdentityResult result = await usermanager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }
        
        [HttpDelete]
        [Route("{id}/roles/{roleid}")]
        public async Task<IActionResult> DismissUserFromRole(int id, int roleid)
        {
            ApplicationUser user = await usermanager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            ApplicationRole role = await rolemanager.FindByIdAsync(roleid.ToString());
            if (role == null)
                return NotFound();

            IdentityResult result = await usermanager.RemoveFromRoleAsync(user, role.Name);
            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }
    }
}
