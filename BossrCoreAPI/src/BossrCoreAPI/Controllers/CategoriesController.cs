using System.Linq;
using System.Threading.Tasks;
using BossrCoreAPI.Controllers.Base;
using BossrCoreAPI.Models;
using BossrCoreAPI.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BossrLib.Classes;

namespace BossrCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator, SysOp")]
    public class CategoriesController : GenericController<Category>
    {
        public CategoriesController(ApplicationDbContext context) : base(context)
        {
        }
        
        [HttpGet("{id}/creatures")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoryCreatures(int id)
        {
            if (await context.Categories.AnyAsync(x => x.Id == id) == false)
                return NotFound();

            return Ok(await context.Creatures.Where(x => x.CategoryId == id).ToListAsync());
        }
    }
}
