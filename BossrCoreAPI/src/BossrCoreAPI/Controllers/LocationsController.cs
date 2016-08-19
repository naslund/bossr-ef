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
    public class LocationsController : GenericController<Location>
    {
        public LocationsController(ApplicationDbContext context) : base(context)
        {
        }

        [HttpGet("{id}/spawns")]
        public async Task<IActionResult> GetLocationSpawns(int id)
        {
            if (context.Locations.Any(x => x.Id == id) == false)
                return NotFound();

            return Ok(await context.Spawns.Where(x => x.LocationId.Value == id).ToListAsync());
        }

        [HttpGet("{id}/creatures")]
        public async Task<IActionResult> GetLocationCreature(int id)
        {
            Location location = await context.Locations.SingleOrDefaultAsync(x => x.Id == id);
            if (location == null)
                return NotFound();

            return Ok(await context.Creatures.SingleOrDefaultAsync(x => x.Id == location.CreatureId));
        }
    }
}