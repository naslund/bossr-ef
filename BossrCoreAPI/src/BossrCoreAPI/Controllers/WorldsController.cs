using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BossrCoreAPI.Controllers.Base;
using BossrCoreAPI.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BossrLib.Classes;

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
        [AllowAnonymous]
        public async Task<IActionResult> GetWorldSpawns(int id)
        {
            if (context.Worlds.Any(x => x.Id == id) == false)
                return NotFound();

            return Ok(await context.Spawns.Where(x => x.WorldId == id).ToListAsync());
        }

        [HttpGet("{id}/spawns/recent")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRecentSpawns(int id)
        {
            DateTimeOffset threeDaysAgo = new DateTimeOffset(DateTime.UtcNow).AddDays(-3);

            return Ok(await context.Spawns.Where(x => x.WorldId == id).Where(x => x.TimeMaxUtc > threeDaysAgo).ToListAsync());
        }

        [HttpGet("{id}/spawns/latest")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLatestSpawns(int id)
        {
            List<Spawn> spawns = new List<Spawn>();
            var creatures = await context.Creatures.Include(x => x.Locations).ToListAsync();
            var allSpawns = await context.Spawns.OrderBy(x => x.TimeMaxUtc).ToListAsync();

            foreach (var creature in creatures)
                spawns.AddRange(allSpawns.Where(x => x.CreatureId == creature.Id).Take(creature.Locations.Count));

            return Ok(spawns);
        }
    }
}
