using System;
using System.Collections.Generic;
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
    public class CreaturesController : GenericController<Creature>
    {
        public CreaturesController(ApplicationDbContext context) : base(context)
        {
        }

        // GET: api/creatures/5/spawns
        [HttpGet("{id}/spawns")]
        public async Task<IActionResult> GetCreatureSpawns(int id)
        {
            if (await context.Creatures.AnyAsync(x => x.Id == id) == false)
                return NotFound();

            return Ok(await context.Spawns.Where(x => x.CreatureId == id).ToListAsync());
        }

        [HttpGet("{id}/locations")]
        public async Task<IActionResult> GetCreatureLocations(int id)
        {
            if (await context.Creatures.AnyAsync(x => x.Id == id) == false)
                return NotFound();

            return Ok(await context.Locations.Where(x => x.CreatureId == id).ToListAsync());
        }
    }
}
