using System.Collections.Generic;
using System.Threading.Tasks;
using BossrCoreAPI.Models.Identity;
using BossrCoreAPI.Models.Requests;
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
        public async Task<IActionResult> PostUser([FromBody]UserRequest userRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (userRequest.Username == null || userRequest.Password == null)
                return BadRequest();

            IdentityResult result = await usermanager.CreateAsync(new ApplicationUser { UserName = userRequest.Username }, userRequest.Password);
            if (result.Succeeded)
                return Ok(usermanager.FindByNameAsync(userRequest.Username)); // Todo: 201 Created

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
        
        [HttpPut("{id}/password")]
        public async Task<IActionResult> ChangeUserPassword(int id, [FromBody]PasswordRequest passwordRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (passwordRequest.OldPassword == null || passwordRequest.NewPassword == null)
                return BadRequest();

            ApplicationUser user = await usermanager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            IdentityResult result = await usermanager.ChangePasswordAsync(user, passwordRequest.OldPassword, passwordRequest.NewPassword);
            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }
        
        [HttpGet("{id}/roles")]
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
        
        [HttpPut("{id}/roles/{roleid}")]
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
        
        [HttpDelete("{id}/roles/{roleid}")]
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
