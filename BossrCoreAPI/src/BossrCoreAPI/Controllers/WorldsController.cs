using System;
using System.Linq;
using System.Threading.Tasks;
using BossrCoreAPI.Controllers.Base;
using BossrCoreAPI.Models;
using BossrCoreAPI.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BossrCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator, SysOp")]
    public class WorldsController : GenericController<World>
    {
        public WorldsController(ApplicationDbContext context) : base(context)
        {
        }
        
        [HttpGet("{id}/spawns")]
        public async Task<IActionResult> GetWorldSpawns(int id)
        {
            if (context.Worlds.Any(x => x.Id == id) == false)
                return NotFound();

            return Ok(await context.Spawns.Where(x => x.WorldId == id).ToListAsync());
        }

        [HttpGet("{id}/spawns/recent")]
        public async Task<IActionResult> GetRecentSpawns(int id)
        {
            DateTimeOffset threeDaysAgo = new DateTimeOffset(DateTime.UtcNow).AddDays(-3);

            return Ok(await context.Spawns.Where(x => x.WorldId == id).Where(x => x.TimeMaxUtc > threeDaysAgo).ToListAsync());
        }
    }
}
