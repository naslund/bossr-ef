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
    public class SpawnsController : GenericController<Spawn>
    {
        public SpawnsController(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IActionResult> PostEntity(Spawn entity)
        {
            if (entity.TimeMinUtc.Ticks == 0 || entity.TimeMaxUtc.Ticks == 0)
                return BadRequest();

            if (entity.TimeMinUtc.Offset != TimeSpan.Zero || entity.TimeMaxUtc.Offset != TimeSpan.Zero)
                return BadRequest(new { message = "TimeMinUtc and TimeMaxUtc must be UTC (+00:00)" });

            if (entity.TimeMinUtc > entity.TimeMaxUtc)
                return BadRequest(new { message = "TimeMinUtc can only occur before TimeMaxUtc" });

            return await base.PostEntity(entity);
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentSpawns()
        {
            DateTimeOffset threeDaysAgo = new DateTimeOffset(DateTime.UtcNow).AddDays(-3);

            return Ok(await context.Spawns.Where(x => x.TimeMaxUtc > threeDaysAgo).ToListAsync());
        }
    }
}
