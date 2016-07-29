using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BossrAPI.Models;
using BossrLib;

namespace BossrAPI.Controllers
{
    public class WorldsController : ApiController
    {
        private readonly BossrDbContext db = new BossrDbContext();

        // GET: api/Worlds
        public IQueryable<World> GetWorlds()
        {
            return db.Worlds;
        }

        // GET: api/Worlds/5
        [ResponseType(typeof(World))]
        public async Task<IHttpActionResult> GetWorld(int id)
        {
            var world = await db.Worlds.FindAsync(id);
            if (world == null)
            {
                return NotFound();
            }

            return Ok(world);
        }

        // PUT: api/Worlds/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWorld(int id, World world)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != world.Id)
            {
                return BadRequest();
            }

            db.Entry(world).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorldExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Worlds
        [ResponseType(typeof(World))]
        public async Task<IHttpActionResult> PostWorld(World world)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Worlds.Add(world);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new {id = world.Id}, world);
        }

        // DELETE: api/Worlds/5
        [ResponseType(typeof(World))]
        public async Task<IHttpActionResult> DeleteWorld(int id)
        {
            var world = await db.Worlds.FindAsync(id);
            if (world == null)
            {
                return NotFound();
            }

            db.Worlds.Remove(world);
            await db.SaveChangesAsync();

            return Ok(world);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorldExists(int id)
        {
            return db.Worlds.Count(e => e.Id == id) > 0;
        }
    }
}