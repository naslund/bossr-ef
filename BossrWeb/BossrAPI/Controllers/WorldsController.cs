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
        public async Task<IHttpActionResult> GetWorlds()
        {
            return Ok(await db.Worlds.ToListAsync());
        }

        // GET: api/Worlds/5
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

        // GET: api/worlds/5/spawns
        [HttpGet]
        [Route("api/worlds/{worldid}/spawns")]
        public async Task<IHttpActionResult> GetWorldSpawns(int worldid)
        {
            if (WorldExists(worldid) == false)
                return NotFound();

            return Ok(await db.Spawns.Where(x => x.WorldId == worldid).ToListAsync());
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