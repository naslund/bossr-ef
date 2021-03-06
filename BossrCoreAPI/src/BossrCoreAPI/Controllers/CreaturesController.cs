﻿using System;
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
    public class CreaturesController : GenericController<Creature>
    {
        public CreaturesController(ApplicationDbContext context) : base(context)
        {
        }

        [HttpGet("{id}/categories")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCreatureCategory(int id)
        {
            Creature creature = await context.Creatures.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == id);
            if (creature?.Category == null)
                return NotFound();
            
            return Ok(creature.Category);
        }

        [HttpPut("{id}/categories/{categoryid}")]
        public async Task<IActionResult> PutCreatureCategory(int id, int categoryid)
        {
            Creature creature = await context.Creatures.SingleOrDefaultAsync(x => x.Id == id);
            if (creature == null)
                return NotFound();

            if (await context.Categories.AnyAsync(x => x.Id == categoryid) == false)
                return NotFound();

            creature.CategoryId = categoryid;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}/categories")]
        public async Task<IActionResult> DeleteCreatureCategory(int id)
        {
            Creature creature = await context.Creatures.SingleOrDefaultAsync(x => x.Id == id);
            if (creature == null)
                return NotFound();

            creature.CategoryId = null;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}/spawns")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCreatureSpawns(int id)
        {
            if (await context.Creatures.AnyAsync(x => x.Id == id) == false)
                return NotFound();

            return Ok(await context.Spawns.Where(x => x.CreatureId == id).ToListAsync());
        }

        [HttpGet("{id}/spawns/recent")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRecentSpawns(int id)
        {
            DateTimeOffset threeDaysAgo = new DateTimeOffset(DateTime.UtcNow).AddDays(-3);

            return Ok(await context.Spawns.Where(x => x.CreatureId == id).Where(x => x.TimeMaxUtc > threeDaysAgo).ToListAsync());
        }

        [HttpGet("{id}/spawns/latest")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLatestSpawns(int id)
        {
            var worlds = await context.Worlds.ToListAsync();
            var allSpawns = await context.Spawns.Where(x => x.CreatureId == id).OrderByDescending(x => x.TimeMaxUtc).ToListAsync();
            
            return Ok(worlds.Select(world => allSpawns.FirstOrDefault(x => x.WorldId == world.Id)).Where(spawn => spawn != null).ToList());
        }

        [HttpGet("{id}/locations")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCreatureLocations(int id)
        {
            if (await context.Creatures.AnyAsync(x => x.Id == id) == false)
                return NotFound();

            return Ok(await context.Locations.Where(x => x.CreatureId == id).ToListAsync());
        }
    }
}
