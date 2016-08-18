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
    public class RolesController : Controller
    {
        private readonly ApplicationRoleManager rolemanager;

        public RolesController(ApplicationRoleManager rolemanager)
        {
            this.rolemanager = rolemanager;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await rolemanager.Roles.ToListAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ApplicationRole role = await rolemanager.FindByIdAsync(id.ToString());
            if (role == null)
                return NotFound();

            return Ok(role);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, string name)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            ApplicationRole role = await rolemanager.FindByIdAsync(id.ToString());
            if (role == null)
                return BadRequest();

            role.Name = name;
            
            IdentityResult result = await rolemanager.UpdateAsync(role);
            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostRole(string name)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ApplicationRole role = new ApplicationRole { Name = name };

            IdentityResult result = await rolemanager.CreateAsync(role);
            if (result.Succeeded)
                return Ok(role); // Todo: 201 Created

            return BadRequest(result.Errors);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ApplicationRole role = await rolemanager.FindByIdAsync(id.ToString());
            if (role == null)
                return NotFound();

            IdentityResult result = await rolemanager.DeleteAsync(role);
            if (result.Succeeded)
                return Ok(role);

            return BadRequest(result.Errors);
        }
    }
}
