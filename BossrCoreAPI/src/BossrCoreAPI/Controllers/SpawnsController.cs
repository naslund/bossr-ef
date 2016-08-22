using System;
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
    public class SpawnsController : GenericController<Spawn>
    {
        public SpawnsController(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IActionResult> PostEntity([FromBody]Spawn entity)
        {
            if (entity.TimeMinUtc.Ticks == 0 || entity.TimeMaxUtc.Ticks == 0)
                return BadRequest();

            if (entity.TimeMinUtc.Offset != TimeSpan.Zero || entity.TimeMaxUtc.Offset != TimeSpan.Zero)
                return BadRequest(new { message = "TimeMinUtc and TimeMaxUtc must be UTC (+00:00)" });

            if (entity.TimeMinUtc > entity.TimeMaxUtc)
                return BadRequest(new { message = "TimeMinUtc can only occur before TimeMaxUtc" });

            return await base.PostEntity(entity);
        }

        public override async Task<IActionResult> PutEntity([FromBody]Spawn entity)
        {
            if (entity.TimeMinUtc.Ticks == 0 || entity.TimeMaxUtc.Ticks == 0)
                return BadRequest();

            if (entity.TimeMinUtc.Offset != TimeSpan.Zero || entity.TimeMaxUtc.Offset != TimeSpan.Zero)
                return BadRequest(new { message = "TimeMinUtc and TimeMaxUtc must be UTC (+00:00)" });

            if (entity.TimeMinUtc > entity.TimeMaxUtc)
                return BadRequest(new { message = "TimeMinUtc can only occur before TimeMaxUtc" });

            return await base.PutEntity(entity);
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentSpawns()
        {
            DateTimeOffset threeDaysAgo = new DateTimeOffset(DateTime.UtcNow).AddDays(-3);

            return Ok(await context.Spawns.Where(x => x.TimeMaxUtc > threeDaysAgo).ToListAsync());
        }

        [HttpGet("{id}/worlds")]
        public async Task<IActionResult> GetSpawnWorld(int id)
        {
            Spawn spawn = await context.Spawns.Include(x => x.World).SingleOrDefaultAsync(x => x.Id == id);
            if (spawn?.World == null)
                return NotFound();

            spawn.World.Spawns = null;
            return Ok(spawn.World);
        }

        [HttpPut("{id}/worlds/{worldid}")]
        public async Task<IActionResult> PutSpawnWorld(int id, int worldid)
        {
            Spawn spawn = await context.Spawns.SingleOrDefaultAsync(x => x.Id == id);
            if (spawn == null)
                return NotFound();

            if (await context.Worlds.AnyAsync(x => x.Id == worldid) == false)
                return NotFound();

            spawn.WorldId = worldid;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}/creatures")]
        public async Task<IActionResult> GetSpawnCreature(int id)
        {
            Spawn spawn = await context.Spawns.Include(x => x.Creature).SingleOrDefaultAsync(x => x.Id == id);
            if (spawn?.Creature == null)
                return NotFound();

            spawn.Creature.Spawns = null;
            return Ok(spawn.Creature);
        }

        [HttpPut("{id}/creatures/{creatureid}")]
        public async Task<IActionResult> PutSpawnCreature(int id, int creatureid)
        {
            Spawn spawn = await context.Spawns.SingleOrDefaultAsync(x => x.Id == id);
            if (spawn == null)
                return NotFound();

            if (await context.Creatures.AnyAsync(x => x.Id == creatureid) == false)
                return NotFound();

            spawn.CreatureId = creatureid;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}/locations")]
        public async Task<IActionResult> GetSpawnLocation(int id)
        {
            Spawn spawn = await context.Spawns.Include(x => x.Location).SingleOrDefaultAsync(x => x.Id == id);
            if (spawn?.Location == null)
                return NotFound();

            spawn.Location.Spawns = null;
            return Ok(spawn.Location);
        }

        [HttpPut("{id}/locations/{locationid}")]
        public async Task<IActionResult> PutSpawnLocation(int id, int locationid)
        {
            Spawn spawn = await context.Spawns.SingleOrDefaultAsync(x => x.Id == id);
            if (spawn == null)
                return NotFound();

            if (await context.Locations.AnyAsync(x => x.Id == locationid) == false)
                return NotFound();

            spawn.LocationId = locationid;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}/locations")]
        public async Task<IActionResult> DeleteSpawnLocation(int id)
        {
            Spawn spawn = await context.Spawns.SingleOrDefaultAsync(x => x.Id == id);
            if (spawn == null)
                return NotFound();

            spawn.LocationId = null;
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
