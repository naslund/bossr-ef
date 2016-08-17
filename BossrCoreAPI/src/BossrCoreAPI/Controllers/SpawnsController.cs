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
            if (entity.DateTimeUtc.Ticks == 0)
                return BadRequest();

            if (entity.DateTimeUtc.Offset != TimeSpan.Zero)
                return BadRequest(new { message = "DateTimeUTC must be UTC (+00:00)" });

            return await base.PostEntity(entity);
        }
    }
}
