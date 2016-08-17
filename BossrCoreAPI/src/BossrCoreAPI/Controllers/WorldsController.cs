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
    public class WorldsController : GenericController<World>
    {
        public WorldsController(ApplicationDbContext context) : base(context)
        {
        }

        // GET: api/Worlds/5/spawns
        [HttpGet]
        [Route("{id}/spawns")]
        public async Task<IActionResult> GetWorldSpawns(int id)
        {
            if (context.Worlds.Any(x => x.Id == id) == false)
                return NotFound();

            return Ok(await context.Spawns.Where(x => x.WorldId == id).ToListAsync());
        }
    }
}
