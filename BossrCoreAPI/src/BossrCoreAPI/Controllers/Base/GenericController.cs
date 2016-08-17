using System.Threading.Tasks;
using BossrCoreAPI.Models;
using BossrCoreAPI.Models.Identity;
using BossrCoreAPI.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BossrCoreAPI.Controllers.Base
{
    public abstract class GenericController<T> : Controller where T : class, IEntity
    {
        protected readonly ApplicationDbContext context;

        protected GenericController(ApplicationDbContext context)
        {
            this.context = context;
        }
        
        [HttpGet]
        public virtual async Task<IActionResult> GetEntities()
        {
            return Ok(await context.Set<T>().ToListAsync());
        }
        
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetEntity(int id)
        {
            T entity = await context.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> PutEntity(T entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await context.Categories.AnyAsync(x => x.Id == entity.Id) == false)
                return NotFound();

            context.Entry(entity).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public virtual async Task<IActionResult> PostEntity(T entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();

            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteEntity(int id)
        {
            T entity = await context.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                return NotFound();

            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();

            return Ok(entity);
        }
    }
}
